using EnergyRegen.Energy;
using EnergyRegen.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace EnergyRegen
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private EnergySettings _energySettings;
        [SerializeField] private EnergyBarUIView _energyBarView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_energySettings);

            builder.Register<EnergyService>(Lifetime.Singleton).As<IEnergyService>();

            builder.Register<EnergyBarUIViewModel>(Lifetime.Singleton);

            builder.RegisterInstance(_energyBarView);

            builder.RegisterEntryPoint<GameEntryPoint>();
        }
    }
}