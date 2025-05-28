using Microsoft.EntityFrameworkCore;
using PipeLine.Backend.Assemblers.BaseAssemblers;
using PipeLine.Backend.Context;
using PipeLine.Backend.Repos.Base;
using PipeLine.Shared.Dtos.ChargingStationDtos.ChargingPortDto;
using PipeLine.Shared.Models.ChargingStationModels;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;

namespace PipeLine.Backend.Repos.ChargingStationRepos.ChargingPortRepos
{
    public class ChargingPortRepo<TDbContext> : BaseRepo<TDbContext, ChargingPort>, IChargingPortRepo where TDbContext : PipeLineMySqlContext
    {

        private PipeLineContext _pipeLineContext;
        public ChargingPortRepo(TDbContext? dbContext) : base(dbContext)
        {
            _pipeLineContext = dbContext;
        }

        public async Task<IEnumerable<ChargingPort>> GetFilteredPortsByChargingStationIdAsync(int? input, Guid id)
        {
            var query = _dbSet!.Where(p => p.ChargingStationId == id && !p.IsDisabled && !p.IsBeingUsed);

            if (input != null)
            {
                query = query.Where(p => p.PortNumber.ToString().Contains(input.ToString()));
            }

            return query.OrderBy(p => p.PortNumber);
        }

        public async Task<int> GetNumberOfPortsAsync()
        {
            return await _dbSet!.CountAsync();
        }

        public async Task<int> GetNumberOfInUsePortsAsync()
        {
            return await _dbSet!.CountAsync(p => p.IsBeingUsed);
        }

        public async Task<int> GetNumberOfEnabledPortsAsync()
        {
            return await _dbSet!.CountAsync(p => !p.IsDisabled);
        }

        public async Task SetPortToIsBeingUsedByIdAsync(Guid? id)
        {
            var port = await _dbSet!.FirstOrDefaultAsync(p => p.Id == id);
            if (port != null)
            {
                port.IsBeingUsed = true;
                _dbContext.Entry(port).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task SetPortToIsBeingUsedFalseByIdAsync(Guid? id)
        {
            var port = await _dbSet!.FirstOrDefaultAsync(p => p.Id == id);
            if (port != null)
            {
                port.IsBeingUsed = false;
                _dbContext.Entry(port).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<int> GetNumberOfPortsOfChargingStationAsync(Guid id)
        {
            return await _dbSet!.CountAsync(p => p.ChargingStationId == id);
        }

        public async Task SetPortToIsChargingByIdAsync(Guid? id)
        {
            var port = await _dbSet!.FirstOrDefaultAsync(p => p.Id == id);
            if (port != null)
            {
                port.IsCharging = true;
                _dbContext.Entry(port).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task SetPortToIsChargingFalseByIdAsync(Guid? id)
        {
            var port = await _dbSet!.FirstOrDefaultAsync(p => p.Id == id);
            if (port != null)
            {
                port.IsCharging = false;
                _dbContext.Entry(port).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
