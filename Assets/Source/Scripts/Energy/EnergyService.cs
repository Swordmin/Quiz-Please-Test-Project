using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using EnergyRegen.Core;
using UniRx;
using UnityEngine;

namespace EnergyRegen.Energy
{
    public class EnergyService : Service, IEnergyService
    {
        private readonly EnergySettings _settings;

        private readonly ReactiveProperty<int> _current = new();
        private readonly ReactiveProperty<float> _secondsToNext = new(0f);

        public IReadOnlyReactiveProperty<int> Current => _current;
        public IReadOnlyReactiveProperty<float> SecondsToNext => _secondsToNext;

        public EnergyService(EnergySettings settings)
        {
            _settings = settings;
        }

        protected override UniTask OnInitializeAsync(CancellationToken ct)
        {
            _current.Value = _settings.MaxEnergy;
            _secondsToNext.Value = 0f;

            RegenLoopAsync(LoopCancellationTokenSource.Token).Forget();

            return UniTask.CompletedTask;
        }

        protected override UniTask OnReleaseAsync(CancellationToken ct)
        {
            _current.Dispose();
            _secondsToNext.Dispose();
            return UniTask.CompletedTask;
        }

        public bool TrySpend(int amount)
        {
            if (amount <= 0 || _current.Value < amount) return false;

            _current.Value -= amount;
            return true;
        }

        private async UniTaskVoid RegenLoopAsync(CancellationToken ct)
        {
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    if (_current.Value >= _settings.MaxEnergy)
                    {
                        _secondsToNext.Value = 0f;
                        await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: ct);
                        continue;
                    }

                    float elapsed = 0f;
                    float target = _settings.RegenSeconds;

                    while (elapsed < target)
                    {
                        await UniTask.Yield(PlayerLoopTiming.Update, ct);
                        elapsed += Time.deltaTime;
                        _secondsToNext.Value = Mathf.Clamp01(elapsed / target);
                    }

                    _current.Value = Mathf.Min(_current.Value + 1, _settings.MaxEnergy);
                    _secondsToNext.Value = 0f;
                }
            }
            catch (OperationCanceledException) { }
        }
    }
}
