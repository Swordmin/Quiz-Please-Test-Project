using EnergyRegen.Core;
using UniRx;

namespace EnergyRegen.Energy
{
    public interface IEnergyService : IService
    {
        IReadOnlyReactiveProperty<int> Current { get; }
        IReadOnlyReactiveProperty<float> SecondsToNext { get; }

        bool TrySpend(int amount);
    }
}
