using Microsoft.EntityFrameworkCore;
using PipeLine.Backend.Assemblers.BaseAssemblers;
using PipeLine.Backend.Context;
using PipeLine.Backend.Repos.Base;
using PipeLine.Backend.Repos.ChargingStationRepos;
using PipeLine.Shared.Models.ChargingStationModels;
using PipeLine.Shared.Models.DeviceModels;
using PipeLine.Shared.Models.UserModels;
using System.Linq;
using PipeLine.Backend.Repos.DeviceRepos;
using PipeLine.Shared.Dtos.ChargingStationDto;
using PipeLine.Shared.Models.ChargingInstanceModels;

namespace PipeLine.Backend.Repos.ChargingStationRepos
{
    public class ChargingStationRepo<TDbContext> : BaseRepo<TDbContext, ChargingStation>, IChargingStationRepo where TDbContext : PipeLineMySqlContext
    {
        public ChargingStationRepo(TDbContext? dbContext) : base(dbContext)
        {
        }

        public IQueryable<ChargingStation> GetAllWithNavigation()
        {
            return _dbSet!
                        .Include(s => s.Ports);
        }

        public async Task<int> GetNumberOfChargingStationsAsync()
        {
            return await _dbSet!.CountAsync();
        }

        public IQueryable<ChargingStation> GetFilteredStationsAsync(string input)
        {
            return _dbSet!
                .Where(d => string.IsNullOrWhiteSpace(input) ||
                            EF.Functions.Like(d.Name, $"%{input}%") ||
                            EF.Functions.Like(d.Address, $"%{input}%"))
                .Select(s => s);
        }

    }
}
