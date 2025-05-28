using PipeLine.Backend.Assemblers.BaseAssemblers;
using PipeLine.Shared.Converters;
using PipeLine.Shared.Dtos.ErrorTicketDtos;
using PipeLine.Shared.Models.ErrorTicketModels;

namespace PipeLine.Backend.Assemblers.ErrorTicketAssemblers
{
    public class ErrorTicketAssembler : Assembler<ErrorTicket, ErrorTicketDto>, IErrorTicketAssembler
    {
        public override ErrorTicketDto ToDto(ErrorTicket errorTicket)
        {
            return errorTicket.ToDto();
        }
        public override ErrorTicket ToModel(ErrorTicketDto dto)
        {
            return dto.ToModel();
        }
    }
}
