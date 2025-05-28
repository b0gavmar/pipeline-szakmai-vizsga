using PipeLine.Backend.Repos.Base;
using PipeLine.Shared.Models.ErrorTicketModels;

namespace PipeLine.Backend.Repos.ErrorTicketRepos
{
    public interface IErrorTicketRepo : IBaseRepo<ErrorTicket>
    {
        Task<int> GetNumberOfUnsolvedAsync();
        IQueryable<ErrorTicket> GetAllTicketsByChargingStationIdAsync(Guid id);
        IQueryable<ErrorTicket> GetByDescription(string description);
    }
}
