using Microsoft.EntityFrameworkCore;
using PipeLine.Backend.Assemblers.BaseAssemblers;
using PipeLine.Backend.Context;
using PipeLine.Backend.Repos.Base;
using PipeLine.Shared.Models.ChargingInstanceModels;
using PipeLine.Shared.Responses;

namespace PipeLine.Backend.Repos.ChargingInstanceRepos
{
    public class ChargingInstanceRepo<TDbContext> : BaseRepo<TDbContext, ChargingInstance>, IChargingInstanceRepo where TDbContext : PipeLineMySqlContext
    {
        public ChargingInstanceRepo(TDbContext? dbContext) : base(dbContext)
        {
        }

        public IQueryable<ChargingInstance> GetAllWithNavigation()
        {
            return _dbSet!
                        .Include(ci => ci.ChargingPort)
                            .ThenInclude(cp => cp.ChargingStation)
                        .Include(ci => ci.Device);
        }

        public IQueryable<ChargingInstance> GetFilteredInstancesByUserIdAsync(DateTime? start, DateTime? end, Guid userId, bool? state)
        {
            var query = GetAllWithNavigation()
                        .Where(ci => ci.Device != null && ci.Device.ApplicationUserId == userId);

            if (start.HasValue)
            {
                query = query.Where(ci => ci.Start >= start); 
            }

            if (end.HasValue)
            {
                query = query.Where(ci => ci.End <= end);
            }

            if(state == true)
            {
                query = query.Where(ci => ci.End != null);
            }
            else if(state == false)
            {
                query = query.Where(ci => ci.End == null);
            }

            return query.OrderByDescending(ci => ci.Start);
        }


        public Task<ChargingInstance?> GetByIdAsync(Guid id)
        {
            return _dbSet!
                    .Include(ci => ci.ChargingPort)
                    .ThenInclude(cp => cp.ChargingStation)
                    .FirstOrDefaultAsync(ci => ci.Id == id); 
        }

        public IQueryable<ChargingInstance> GetInstancesByDeviceIdAsync(Guid id)
        {
            return GetAll().Where(i => i.DeviceId == id);
        }

        public IQueryable<ChargingInstance> GetInstancesByPortId(Guid id)
        {
            return GetAll().Where(i => i.ChargingPortId == id);
        }

        public async Task<int> GetNumberOfInstancesAsync()
        {
            return await Task.Run(() => _dbSet!.Count());
        }

        public async Task<int> GetNumberOfInstancesByUserIdAsync(Guid id)
        {
            return (await _dbSet!
                .Where(ci => ci.Device != null && ci.Device.ApplicationUserId == id)
                .ToListAsync()).Count;
        }

        public async Task<Response> FinishInstance(ChargingInstance instance)
        {
            try
            {
                _dbContext.Entry(instance).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return new Response(instance.Id);
            }
            catch (Exception ex)
            {
                return Response.Failure($"Error while updating: {ex.Message}");
            }
        }

        public async Task<ChargingInstance> GetInstanceByIdAsync(Guid id)
        {
            return await _dbSet!
                .AsNoTracking() 
                .Include(ci => ci.ChargingPort)
                .ThenInclude(cp => cp.ChargingStation)
                .FirstOrDefaultAsync(ci => ci.Id == id);
        }
    }
}
