using System.Threading;
using Cysharp.Threading.Tasks;

namespace EnergyRegen.Core
{
    public abstract class Service : IService
    {
        protected CancellationTokenSource LoopCancellationTokenSource { get; private set; }

        public virtual async UniTask InitializeAsync(CancellationToken ct)
        {
            LoopCancellationTokenSource = new CancellationTokenSource();
            await OnInitializeAsync(ct);
        }

        public virtual async UniTask ReleaseAsync(CancellationToken ct)
        {
            LoopCancellationTokenSource?.Cancel();
            LoopCancellationTokenSource?.Dispose();
            LoopCancellationTokenSource = null;
            await OnReleaseAsync(ct);
        }

        protected virtual UniTask OnInitializeAsync(CancellationToken ct) => UniTask.CompletedTask;
        protected virtual UniTask OnReleaseAsync(CancellationToken ct) => UniTask.CompletedTask;
    }
}
