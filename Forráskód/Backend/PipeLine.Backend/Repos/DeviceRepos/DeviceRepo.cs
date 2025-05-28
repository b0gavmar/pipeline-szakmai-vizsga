using Microsoft.EntityFrameworkCore;
using PipeLine.Backend.Assemblers.BaseAssemblers;
using PipeLine.Backend.Context;
using PipeLine.Backend.Repos.Base;
using PipeLine.Shared.Models.ChargingInstanceModels;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Shared.Models.Enums;

namespace PipeLine.Backend.Repos.DeviceRepos
{
    public class DeviceRepo<TDbContext> : BaseRepo<TDbContext, Device>, IDeviceRepo where TDbContext : PipeLineMySqlContext
    {

        public DeviceRepo(TDbContext? dbContext) : base(dbContext)
        {

        }

        public IQueryable<Device> GetAllWithNavigation()
        {
            return _dbSet!
                    .Include(d => d.ChargingInstances);
        }
        public async Task<int> GetNumberOfDevicesByUserIdAsync(Guid Id)
        {
            return (await _dbSet!.Where(d => d.ApplicationUserId == Id).ToListAsync()).Count;
        }

        public async Task<int> GetNumberOfEbikesAsync()
        {
            return (await _dbSet!.Where(d => d.DeviceType == DeviceType.EBike).ToListAsync()).Count;
        }

        public async Task<int> GetNumberOfEScootersAsync()
        {
            return (await _dbSet!.Where(d => d.DeviceType == DeviceType.EScooter).ToListAsync()).Count;
        }

        public async Task<int> GetNumberOfESkateBoardsAsync()
        {
            return (await _dbSet!.Where(d => d.DeviceType == DeviceType.ESkateBoard).ToListAsync()).Count;
        }

        public async Task<int> GetNumberOfEBikesWithDetachableBatteryAsync()
        {
            return (await _dbSet!.Where(d => d.DeviceType == DeviceType.EBike && ((EBike)d).DetachableBattery).ToListAsync()).Count;
        }

        public async Task<int> GetNumberOfFoldableEScootersAsync()
        {
            return (await _dbSet!.Where(d => d.DeviceType == DeviceType.EScooter && ((EScooter)d).IsFoldable).ToListAsync()).Count;
        }

        public async Task<int> GetNumberOfLocakbleESkateBoardsAsync()
        {
            return (await _dbSet!.Where(d => d.DeviceType == DeviceType.ESkateBoard && ((ESkateBoard)d).CanBeLocked).ToListAsync()).Count;
        }

        public async Task<int> GetNumberOfDevicesWithANameAsync()
        {
            return (await _dbSet!.Where(d => d.Name != null).ToListAsync()).Count;
        }

        public IQueryable<Device> GetEBikesWithANameAndDetachableBattery()
        {
            return _dbSet!
                .Where(d => d.Name != null && d.DeviceType == DeviceType.EBike && ((EBike)d).DetachableBattery);
        }

        public IQueryable<Device> GetDevicesByUserId(Guid Id)
        {
            return _dbSet!.Where(d=> d.ApplicationUserId == Id);
        }

        public IQueryable<Device> GetFilteredDevicesByUserId(string? input, Guid id)
        {
            var query = _dbSet!.Where(d => d.ApplicationUserId == id);

            if (!string.IsNullOrWhiteSpace(input))
            {
                query = query.Where(d => EF.Functions.Like(d.Name, $"%{input}%") ||
                                         EF.Functions.Like(d.Model, $"%{input}%") ||
                                         EF.Functions.Like(d.Manufacturer, $"%{input}%"));
            }

            return query;
        }

        public IQueryable<Device> GetFilteredDevices(string? input)
        {
            var query = GetAll();

            if (!string.IsNullOrWhiteSpace(input))
            {
                query = query.Where(d => EF.Functions.Like(d.Name, $"%{input}%") ||
                                         EF.Functions.Like(d.Model, $"%{input}%") ||
                                         EF.Functions.Like(d.Manufacturer, $"%{input}%"));
            }

            return query;
        }

        public IQueryable<Device> GetNonChargingDevicesByUserId(Guid id)
        {
            return GetAllWithNavigation()
                .Where(d => d.ApplicationUserId == id &&
                            d.ChargingInstances.All(ci => ci.End != null));
        }

    }
}
