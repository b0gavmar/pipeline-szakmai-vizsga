using PipeLine.Backend.Repos.ErrorTicketRepos;
using PipeLine.Shared.Dtos.ErrorTicketDtos;
using PipeLine.Shared.Models.ErrorTicketModels;
using PipeLine.Backend.Services.Base;
using PipeLine.Backend.Assemblers.ErrorTicketAssemblers;
using Microsoft.EntityFrameworkCore;

namespace PipeLine.Backend.Services.ErrorTicketServices
{
    public class ErrorTicketService : BaseService<ErrorTicket, ErrorTicketDto>, IErrorTicketService
    {
        private readonly IErrorTicketRepo _errorTicketRepo;
        private readonly IErrorTicketAssembler _assembler;

        public ErrorTicketService(IErrorTicketRepo errorTicketRepo, IErrorTicketAssembler assembler)
            : base(errorTicketRepo, assembler)
        {
            _errorTicketRepo = errorTicketRepo;
            _assembler = assembler;
        }

        public async Task<int> GetNumberOfUnsolvedAsync()
        {
            return await _errorTicketRepo.GetNumberOfUnsolvedAsync();
        }

        public async Task<IEnumerable<ErrorTicketDto>> GetAllTicketsByChargingStationIdAsync(Guid? stationId)
        {
            if (stationId == null)
            {
                var tickets = _errorTicketRepo.GetAll().OrderBy(t => t.ChargingStation.Name);
                return _assembler.ToDtos(tickets.ToList());
            }
            else
            {
                var tickets = _errorTicketRepo.GetAllTicketsByChargingStationIdAsync((Guid)stationId).OrderBy(t => t.ChargingStation.Name);
                return _assembler.ToDtos(tickets.ToList());
            }
                
            
        }
    }
}
