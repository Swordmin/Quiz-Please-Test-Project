using EnergyRegen.Core;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace EnergyRegen.UI
{
    public class EnergyBarUIView : UIView<EnergyBarUIViewModel>
    {
        [SerializeField] private TextMeshProUGUI _energyLabel;
        [SerializeField] private Image _fillImage;
        [SerializeField] private Button _spendButton;

        private CompositeDisposable _subscriptions;

        public override void Initialize()
        {
            _subscriptions = new CompositeDisposable();

            ViewModel.Current
                .Subscribe(value => _energyLabel.text = $"{value} / {ViewModel.Max}")
                .AddTo(_subscriptions);

            ViewModel.SecondsToNext
                .Subscribe(progress => _fillImage.fillAmount = progress)
                .AddTo(_subscriptions);

            _spendButton.OnClickAsObservable()
                .Subscribe(_ => ViewModel.TrySpend())
                .AddTo(_subscriptions);
        }

        public override void Release()
        {
            base.Release();
            _subscriptions?.Dispose();
            _subscriptions = null;
        }
    }
}
