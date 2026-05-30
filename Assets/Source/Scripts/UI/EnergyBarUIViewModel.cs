using EnergyRegen.Core;
using EnergyRegen.Energy;
using UniRx;

namespace EnergyRegen.UI
{
    public class EnergyBarUIViewModel : IUIViewModel
    {
        private readonly IEnergyService _energyService;

        public IReadOnlyReactiveProperty<int> Current => _energyService.Current;
        public IReadOnlyReactiveProperty<float> SecondsToNext => _energyService.SecondsToNext;
        public int Max { get; }

        public EnergyBarUIViewModel(IEnergyService energyService, EnergySettings settings)
        {
            _energyService = energyService;
            Max = settings.MaxEnergy;
        }

        public bool TrySpend() => _energyService.TrySpend(10);
    }
}
