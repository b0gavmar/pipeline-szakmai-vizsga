using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1;
using PipeLine.Backend.Assemblers.DeviceAssemblers;
using PipeLine.Backend.Context;
using PipeLine.Backend.Repos.ChargingInstanceRepos;
using PipeLine.Backend.Repos.DeviceRepos;
using PipeLine.Backend.Services.Base;
using PipeLine.Shared.Dtos.DeviceDtos;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Shared.Responses;

namespace PipeLine.Backend.Services.DeviceServices
{
    public class DeviceService : BaseService<Device, DeviceDto>, IDeviceService
    {
        private readonly IDeviceRepo _deviceRepo;
        private readonly IDeviceAssembler _assembler;
        private readonly IChargingInstanceRepo _chargingInstanceRepo;

        public DeviceService(IDeviceRepo deviceRepo, IDeviceAssembler assembler, IChargingInstanceRepo chargingInstanceRepo) : base(deviceRepo, assembler)
        {
            _deviceRepo = deviceRepo;
            _assembler = assembler;
            _chargingInstanceRepo = chargingInstanceRepo;
        }

        public async Task<IEnumerable<DeviceDto>> GetDevicesByUserIdAsync(Guid Id)
        {
            var devices = await _deviceRepo.GetDevicesByUserId(Id).ToListAsync();
            return _assembler.ToDtos(devices);
        }

        public async Task<IEnumerable<DeviceDto>> GetFilteredDevicesByUserIdAsync(string? input, Guid Id)
        {

            var devices = await _deviceRepo.GetFilteredDevicesByUserId(input, Id).ToListAsync();
            return _assembler.ToDtos(devices);
        }
        public async Task<IEnumerable<DeviceDto>> GetFilteredDevicesAsync(string? input)
        {
            var devices = await _deviceRepo.GetFilteredDevices(input).ToListAsync();
            return _assembler.ToDtos(devices);
        }

        public async Task<IEnumerable<DeviceDto>> GetNonChargingDevicesByUserId(Guid id)
        {
            var devices = await _deviceRepo.GetNonChargingDevicesByUserId(id).ToListAsync();
            return _assembler.ToDtos(devices);
        }

        public async Task<int> GetNumberOfDevicesByUserIdAsync(Guid id)
        {
            return await _deviceRepo.GetNumberOfDevicesByUserIdAsync(id);
        }

        public async Task<int> GetNumberOfDevicesWithANameAsync()
        {
            return await _deviceRepo.GetNumberOfDevicesWithANameAsync();
        }

        public async Task<int> GetNumberOfEbikesAsync()
        {
            return await _deviceRepo.GetNumberOfEbikesAsync();
        }

        public async Task<int> GetNumberOfEBikesWithDetachableBatteryAsync()
        {
            return await _deviceRepo.GetNumberOfEBikesWithDetachableBatteryAsync();
        }

        public async Task<int> GetNumberOfEScootersAsync()
        {
            return await _deviceRepo.GetNumberOfEScootersAsync();
        }

        public async Task<int> GetNumberOfESkateBoardsAsync()
        {
            return await _deviceRepo.GetNumberOfESkateBoardsAsync();
        }

        public async Task<int> GetNumberOfFoldableEScootersAsync()
        {
            return await _deviceRepo.GetNumberOfFoldableEScootersAsync();
        }

        public async Task<int> GetNumberOfLocakbleESkateBoardsAsync()
        {
            return await _deviceRepo.GetNumberOfLocakbleESkateBoardsAsync();
        }

        public override async Task<bool> CanDelete(Guid id)
        {
            var instances = await _chargingInstanceRepo.GetAll()
                .Where(ci => ci.DeviceId == id)
                .ToListAsync();

            return instances.All(ci => ci.End != null);
        }
    }
}
