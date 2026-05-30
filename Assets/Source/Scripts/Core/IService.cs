using System.Threading;
using Cysharp.Threading.Tasks;

namespace EnergyRegen.Core
{
    public interface IService
    {
        UniTask InitializeAsync(CancellationToken ct);
        UniTask ReleaseAsync(CancellationToken ct);
    }
}