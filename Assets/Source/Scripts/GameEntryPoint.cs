using System.Threading;
using EnergyRegen.Energy;
using EnergyRegen.UI;
using UnityEngine;
using VContainer.Unity;

namespace EnergyRegen
{
    public class GameEntryPoint : IAsyncStartable
    {
        private readonly IEnergyService _energyService;
        private readonly EnergyBarUIViewModel _viewModel;
        private readonly EnergyBarUIView _view;

        public GameEntryPoint(
            IEnergyService energyService,
            EnergyBarUIViewModel viewModel,
            EnergyBarUIView view)
        {
            _energyService = energyService;
            _viewModel = viewModel;
            _view = view;
        }

        public async Awaitable StartAsync(CancellationToken ct)
        {
            await _energyService.InitializeAsync(ct);
            _view.Initialize(_viewModel);
        }
    }
}
