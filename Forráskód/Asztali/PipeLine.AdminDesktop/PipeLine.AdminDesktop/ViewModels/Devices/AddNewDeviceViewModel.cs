using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PipeLine.AdminDesktop.ViewModels.Base;
using PipeLine.Backend.Assemblers.DeviceAssemblers;
using PipeLine.HttpService;
using PipeLine.Shared.Dtos.DeviceDtos;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Shared.Models.Enums;
using PipeLine.Shared.Models.UserModels;

namespace PipeLine.AdminDesktop.ViewModels.Devices
{
    public partial class AddNewDeviceViewModel:BaseViewModel
    {
        private readonly IDeviceHttpService _deviceHttpService;
        private readonly IDeviceAssembler _deviceAssembler;
        private readonly IApplicationUserHttpService _applicationUserHttpService;
        public IEnumerable<DeviceType> DeviceTypes => Enum.GetValues<DeviceType>();

        public AddNewDeviceViewModel()
        {
            _deviceHttpService = new DeviceHttpService();
            _deviceAssembler = new DeviceAssembler();
            _applicationUserHttpService = new ApplicationUserHttpService();
        }

        public AddNewDeviceViewModel(IDeviceHttpService? deviceHttpService, IApplicationUserHttpService applicationUserHttpService, IDeviceAssembler deviceAssembler)
        {
            _deviceHttpService = deviceHttpService ?? throw new ArgumentNullException(nameof(deviceHttpService));
            _deviceAssembler = deviceAssembler ?? throw new ArgumentNullException(nameof(deviceAssembler));
            _applicationUserHttpService = applicationUserHttpService ?? throw new ArgumentNullException(nameof(applicationUserHttpService));
        }

        [ObservableProperty]
        private Device _newDevice = new();
        [ObservableProperty]
        private DeviceType _selectedDeviceType = DeviceType.Empty;
        [ObservableProperty]
        private bool _additionalProperty = false;
        [ObservableProperty]
        private string _additionalPropText = ":";
        [ObservableProperty]
        private List<ApplicationUser> _userList = new List<ApplicationUser>();
        [ObservableProperty]
        private ApplicationUser _selectedUser = new ApplicationUser();

        partial void OnSelectedDeviceTypeChanged(DeviceType value)
        {
            NewDevice.DeviceType = value;
            Update();
        }

        partial void OnSelectedUserChanged(ApplicationUser value)
        {
            NewDevice.ApplicationUserId = value.Id;
        }

        [RelayCommand]
        public void AddNewDevice()
        {
            DeviceDto deviceDto = _deviceAssembler.ToDto(NewDevice);
            if (NewDevice.DeviceType == DeviceType.EBike)
            {
                EBike ebike = (EBike)_deviceAssembler.ToModel(deviceDto);
                ebike.DetachableBattery = AdditionalProperty;
                _deviceHttpService.CreateAsync(ebike);
            }
            else if(NewDevice.DeviceType == DeviceType.EScooter)
            {
                EScooter escooter = (EScooter)_deviceAssembler.ToModel(deviceDto);
                escooter.IsFoldable = AdditionalProperty;
                _deviceHttpService.CreateAsync(escooter);
            }
            else if (NewDevice.DeviceType == DeviceType.ESkateBoard)
            {
                ESkateBoard eskateboard = (ESkateBoard)_deviceAssembler.ToModel(deviceDto);
                eskateboard.CanBeLocked = AdditionalProperty;
                _deviceHttpService.CreateAsync(eskateboard);
            }
            else
            {

            }

        }

        public async override Task InitializeAsync()
        {
            Update();
            await base.InitializeAsync();
            UserList = await _applicationUserHttpService.GetAllAsync();
            SelectedUser = UserList[0];
        }

        private void Update()
        {
            if (NewDevice.DeviceType == DeviceType.EBike)
            {
                AdditionalPropText = "Detachable Battery:";
            }
            else if (NewDevice.DeviceType == DeviceType.EScooter)
            {
                AdditionalPropText = "Is Foldable:";
            }
            else if (NewDevice.DeviceType == DeviceType.ESkateBoard)
            {
                AdditionalPropText = "Can Be Locked:";
            }
            else
            {

            }
        }
    }
}
