using CommunityToolkit.Mvvm.ComponentModel;
using LiveCharts;
using LiveCharts.Wpf;
using PipeLine.AdminDesktop.ViewModels.Base;
using PipeLine.HttpService;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Shared.Models.ErrorTicketModels;
using System.Collections.ObjectModel;

namespace PipeLine.AdminDesktop.ViewModels.ControlPanel
{
    public partial class ControlPanelViewModel: BaseViewModel
    {
        private readonly IChargingPortHttpService _chargingPortHttpService;
        private readonly IDeviceHttpService _deviceHttpService;
        private readonly IApplicationUserHttpService _applicationUserHttpService;
        private readonly IErrorTicketHttpService _errorTicketHttpService;

        [ObservableProperty]
        private ObservableCollection<ErrorTicket> _errorTickets = new();

        public ControlPanelViewModel()
        {
            _chargingPortHttpService = new ChargingPortHttpService();
            _deviceHttpService = new DeviceHttpService();
            _applicationUserHttpService = new ApplicationUserHttpService();
            _errorTicketHttpService = new ErrorTicketHttpService();

            _chargingPortsEnabledChart = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Enabled ChargingPorts",
                    Values = new ChartValues<double> {NumberOfEnabledPorts},PushOut = 5
                },
                new PieSeries
                {
                    Title = "Disabled ChargingPorts",
                    Values = new ChartValues<double> {NumberOfPorts},
                    PushOut = 5
                }
            };
            _chargingPortsInUseChart = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "ChargingPorts In Use",
                    Values = new ChartValues<double> {NumberOfInUsePorts},
                        PushOut = 5
                },
                new PieSeries
                {  
                    Title = "ChargingPorts Not In Use",
                    Values = new ChartValues<double> {NumberOfPorts},
                    PushOut = 5

                }
            };


            _deviceModelsChart = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "EBikes",
                    Values = new ChartValues<double> { NumberOfEBikes }
                },
                new ColumnSeries
                {
                    Title = "EScooters",
                    Values = new ChartValues<double> { NumberOfEScooters }
                },
                new ColumnSeries
                {
                    Title = "ESkateBoards",
                    Values = new ChartValues<double> { NumberOfESkateBoards }
                },
            };

            _eBikesDetachableBatteryChart = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "EBikes with detachable battery",
                    Values = new ChartValues<double> { NumberOfEBikesWithDetachableBattery },
                    PushOut = 5
                },
                new PieSeries
                {
                    Title = "EBikes without detachable battery",
                    Values = new ChartValues<double> { NumberOfEBikes },
                    PushOut = 5
                },
            };
            _applicationUserChart = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Users with Email",
                    Values = new ChartValues<double> { NumberOfUsersWithEmail }
                },
                new ColumnSeries
                {
                    Title = "Users without Email",
                    Values = new ChartValues<double> { NumberOfUsersWithoutEmail }
                }
            };
            _foldableEScootersChart = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Foldable escooters",
                    Values = new ChartValues<double> { NumberOfFoldableEScooters },
                    PushOut = 3
                },
                new PieSeries
                {
                    Title = "Non-foldable escooters",
                    Values = new ChartValues<double> { NumberOfEScooters },
                    PushOut = 3
                },
            };

            _lockableESkateBoardsChart = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Lockable ESKateBoards",
                    Values = new ChartValues<double> { NumberOfLockableESkateBoards },
                        PushOut = 5
                },
                new PieSeries
                {
                    Title = "Non-lockable ESKateBoards",
                    Values = new ChartValues<double> { NumberOfESkateBoards },
                    PushOut = 2
                },
            };
        }

        public ControlPanelViewModel(IChargingPortHttpService? chargingPortHttpService, IDeviceHttpService? deviceHttpService,IApplicationUserHttpService? applicationUserHttpService, IErrorTicketHttpService errorTicketHttpService)
        {
            _chargingPortHttpService = chargingPortHttpService ?? throw new ArgumentNullException(nameof(chargingPortHttpService));
            _deviceHttpService = deviceHttpService ?? throw new ArgumentNullException(nameof(deviceHttpService));
            _applicationUserHttpService = applicationUserHttpService ?? throw new ArgumentNullException(nameof(applicationUserHttpService));
            _errorTicketHttpService = errorTicketHttpService ?? throw new ArgumentNullException(nameof(errorTicketHttpService));

            _chargingPortsEnabledChart = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Enabled ChargingPorts",
                    Values = new ChartValues<double> {NumberOfEnabledPorts},
                    PushOut = 2
                },
                new PieSeries
                {
                    Title = "Disabled ChargingPorts",
                    Values = new ChartValues<double> {NumberOfPorts},
                    PushOut = 2

                }
            };
            _chargingPortsInUseChart = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "ChargingPorts In Use",
                    Values = new ChartValues<double> {NumberOfInUsePorts},
                    PushOut = 2
                },
                new PieSeries
                {
                    Title = "ChargingPorts Not In Use",
                    Values = new ChartValues<double> {NumberOfPorts},
                    PushOut = 2

                }
            };
           _deviceModelsChart = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "EBikes",
                    Values = new ChartValues<double> { NumberOfEBikes }
                },
                new ColumnSeries
                {
                    Title = "EScooters",
                    Values = new ChartValues<double> { NumberOfEScooters }
                },
                new ColumnSeries
                {
                    Title = "ESkateBoards",
                    Values = new ChartValues<double> { NumberOfESkateBoards }
                },
            };

            _eBikesDetachableBatteryChart = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "EBikes with detachable battery",
                    Values = new ChartValues<double> { NumberOfEBikesWithDetachableBattery },
                    PushOut = 5
                },
                new PieSeries
                {
                    Title = "EBikes without detachable battery",
                    Values = new ChartValues<double> { NumberOfEBikes },
                    PushOut = 5
                },
            };

            _applicationUserChart = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Users with Email",
                    Values = new ChartValues<double> { NumberOfUsersWithEmail }
                },
                new ColumnSeries
                {
                    Title = "Users without Email",
                    Values = new ChartValues<double> { NumberOfUsersWithoutEmail }
                }
            };

            _foldableEScootersChart = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Foldable escooters",
                    Values = new ChartValues<double> { NumberOfFoldableEScooters },
                    PushOut = 3
                },
                new PieSeries
                {
                    Title = "Non-foldable escooters",
                    Values = new ChartValues<double> { NumberOfEScooters },
                    PushOut = 3
                },
            };

            _lockableESkateBoardsChart = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Lockable ESKateBoards",
                    Values = new ChartValues<double> { NumberOfLockableESkateBoards },
                    PushOut = 5
                },
                new PieSeries
                {
                    Title = "Non-lockable ESKateBoards",
                    Values = new ChartValues<double> { NumberOfESkateBoards },
                    PushOut = 2
                },
            };

        }

        [ObservableProperty]
        private int _numberOfPorts;
        [ObservableProperty]
        private int _numberOfInUsePorts;
        [ObservableProperty]
        private int _numberOfEnabledPorts;

        [ObservableProperty]
        private SeriesCollection _chargingPortsEnabledChart;
        [ObservableProperty]
        private SeriesCollection _chargingPortsInUseChart;


        [ObservableProperty]
        private int _numberOfEBikes;
        [ObservableProperty]
        private int _numberOfEScooters;
        [ObservableProperty]
        private int _numberOfESkateBoards;
        [ObservableProperty]
        private int _numberOfDevicesWithAName;
        [ObservableProperty]
        private int _numberOfEBikesWithDetachableBattery;
        [ObservableProperty]
        private int _numberOfFoldableEScooters;
        [ObservableProperty]
        private int _numberOfLockableESkateBoards;
        [ObservableProperty]
        private int _numberOfUsers;
        [ObservableProperty]
        private int _numberOfUsersWithEmail;
        [ObservableProperty]
        private int _numberOfUsersWithoutEmail;

        [ObservableProperty]
        private ObservableCollection<Device> _ebikesWithANameAndDetachableBattery = new ObservableCollection<Device>();

        [ObservableProperty]
        private SeriesCollection _deviceModelsChart;
        [ObservableProperty]
        private SeriesCollection _eBikesDetachableBatteryChart;
        [ObservableProperty]
        private SeriesCollection _applicationUserChart;
        [ObservableProperty]
        private SeriesCollection _foldableEScootersChart;
        [ObservableProperty]
        private SeriesCollection _lockableESkateBoardsChart;


        public async override Task InitializeAsync()
        { 
           await UpdateAsync();
           await base.InitializeAsync();
        }
        private async Task UpdateAsync()
        {
            NumberOfPorts = await _chargingPortHttpService.GetNumberOfPortsAsync();
            NumberOfInUsePorts = await _chargingPortHttpService.GetNumberOfInUsePortsAsync();
            NumberOfEnabledPorts = await _chargingPortHttpService.GetNumberOfEnabledPortsAsync();
            
            ChargingPortsEnabledChart[0].Values = new ChartValues<double> { NumberOfEnabledPorts };
            ChargingPortsEnabledChart[1].Values = new ChartValues<double> { NumberOfPorts - NumberOfEnabledPorts };
            ChargingPortsInUseChart[0].Values = new ChartValues<double> { NumberOfInUsePorts };
            ChargingPortsInUseChart[1].Values = new ChartValues<double> { NumberOfPorts - NumberOfInUsePorts };

            NumberOfEBikes = await _deviceHttpService.GetNumberOfEBikesAsync();
            NumberOfEScooters = await _deviceHttpService.GetNumberOfEScootersAsync();
            NumberOfESkateBoards = await _deviceHttpService.GetNumberOfESkateBoardsAsync();
            NumberOfDevicesWithAName = await _deviceHttpService.GetNumberOfDevicesWithANameAsync();
            NumberOfEBikesWithDetachableBattery = await _deviceHttpService.GetNumberOfEBikesWithDetachableBatteryAsync();
            NumberOfFoldableEScooters = await _deviceHttpService.GetNumberOfFoldableEScootersAsync();
            NumberOfLockableESkateBoards = await _deviceHttpService.GetNumberOfLocakbleESkateBoardsAsync();
            NumberOfUsers = await _applicationUserHttpService.GetNumberOfUsersAsync();
            NumberOfUsersWithEmail = await _applicationUserHttpService.GetNumberOfUsersWithEmailAsync();
            NumberOfUsersWithoutEmail = NumberOfUsers - NumberOfUsersWithEmail;

            DeviceModelsChart[0].Values = new ChartValues<double>
            {
                NumberOfEBikes
            };
            DeviceModelsChart[1].Values = new ChartValues<double>
            {
                NumberOfEScooters
            };
            DeviceModelsChart[2].Values = new ChartValues<double>
            {
                NumberOfESkateBoards
            };

            EBikesDetachableBatteryChart[0].Values = new ChartValues<double>
            {
                NumberOfEBikesWithDetachableBattery
            };
            EBikesDetachableBatteryChart[1].Values = new ChartValues<double>
            {
                NumberOfEBikes
            };


            ApplicationUserChart[0].Values = new ChartValues<double>
            { 
                NumberOfUsersWithEmail
            };
            ApplicationUserChart[1].Values = new ChartValues<double>
            {
                NumberOfUsersWithoutEmail
            };

            FoldableEScootersChart[0].Values = new ChartValues<double>
            {
                NumberOfFoldableEScooters
            };
            FoldableEScootersChart[1].Values = new ChartValues<double>
            {
                NumberOfEScooters
            };

            LockableESkateBoardsChart[0].Values = new ChartValues<double>
            {
                NumberOfLockableESkateBoards
            };
            LockableESkateBoardsChart[1].Values = new ChartValues<double>
            {
                NumberOfESkateBoards

            };

            var errorTickets = await _errorTicketHttpService.GetAllAsync();
            ErrorTickets = new ObservableCollection<ErrorTicket>(errorTickets);
        }
    }
}
