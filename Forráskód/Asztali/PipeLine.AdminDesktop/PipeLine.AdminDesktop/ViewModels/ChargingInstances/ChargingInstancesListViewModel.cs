using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PipeLine.AdminDesktop.ViewModels.Base;
using PipeLine.HttpService;
using PipeLine.Shared.Models.ChargingInstanceModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PipeLine.AdminDesktop.ViewModels.ChargingInstances
{
    public partial class ChargingInstancesListViewModel : BaseViewModel
    {
        private readonly IChargingInstanceHttpService _chargingInstanceHttpService;

        [ObservableProperty]
        private ObservableCollection<ChargingInstance> _chargingInstancesList = new();

        [ObservableProperty]
        private ChargingInstance _selectedChargingInstance = new();

        public ChargingInstancesListViewModel()
        {
            _chargingInstanceHttpService = new ChargingInstanceHttpService();
            _selectedChargingInstance = new ChargingInstance();
        }

        public ChargingInstancesListViewModel(IChargingInstanceHttpService? chargingInstanceHttpService)
        {
            _chargingInstanceHttpService = chargingInstanceHttpService ?? throw new ArgumentNullException(nameof(chargingInstanceHttpService));
            _selectedChargingInstance = new ChargingInstance();
        }

        [RelayCommand]
        public async Task DoRemoveInstance(ChargingInstance chargingInstance)
        {
            await _chargingInstanceHttpService.DeleteAsync(chargingInstance.Id);
            await UpdateAsync();
        }

        public override async Task InitializeAsync()
        {
            await UpdateAsync();
            await base.InitializeAsync();
        }

        private async Task UpdateAsync()
        {
            List<ChargingInstance> chargingInstances = await _chargingInstanceHttpService.GetAllAsync();
            if (chargingInstances.Count > 0)
            {
                ChargingInstancesList = new ObservableCollection<ChargingInstance>(chargingInstances);
            }
        }
    }
    
}
