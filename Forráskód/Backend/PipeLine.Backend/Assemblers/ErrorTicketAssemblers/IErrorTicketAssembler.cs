using PipeLine.Backend.Assemblers.BaseAssemblers;
using PipeLine.Shared.Dtos.ErrorTicketDtos;
using PipeLine.Shared.Models.ErrorTicketModels;

namespace PipeLine.Backend.Assemblers.ErrorTicketAssemblers
{
    public interface IErrorTicketAssembler : IAssembler<ErrorTicket, ErrorTicketDto>
    {
    }
}
