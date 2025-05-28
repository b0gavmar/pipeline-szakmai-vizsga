using Microsoft.EntityFrameworkCore;
using PipeLine.Backend.Context;
using PipeLine.Backend.Repos.Base;
using PipeLine.Shared.Models.ChargingStationModels.ChargingPortModel;
using PipeLine.Shared.Models.ErrorTicketModels;
using System;

namespace PipeLine.Backend.Repos.ErrorTicketRepos
{
    public class ErrorTicketRepo<TDbContext> : BaseRepo<TDbContext, ErrorTicket>, IErrorTicketRepo where TDbContext : PipeLineMySqlContext
    {
        public ErrorTicketRepo(TDbContext? dbContext) : base(dbContext)
        {
        }

        public IQueryable<ErrorTicket> GetAllTicketsByChargingStationIdAsync(Guid id)
        {
            return  _dbSet!
                .Include(t => t.ChargingStation)
                .Where(t => t.ChargingStationId == id);
        }

        public IQueryable<ErrorTicket> GetByDescription(string description)
        {
            return GetAll().Where(errorTicket => errorTicket.Description == description);
        }

        public async Task<int> GetNumberOfUnsolvedAsync()
        {
            return await Task.Run(() => _dbSet!.Count(errorTicket => errorTicket.IsSolved == false));
        }
    }
}
