using PipeLine.Backend.Services.Base;
using PipeLine.Shared.Dtos.ErrorTicketDtos;
using PipeLine.Shared.Models.ErrorTicketModels;

namespace PipeLine.Backend.Services.ErrorTicketServices
{
    public interface IErrorTicketService : IBaseService<ErrorTicket,ErrorTicketDto>
    {
        Task<int> GetNumberOfUnsolvedAsync();
        Task<IEnumerable<ErrorTicketDto>> GetAllTicketsByChargingStationIdAsync(Guid? stationId);
    }
}
