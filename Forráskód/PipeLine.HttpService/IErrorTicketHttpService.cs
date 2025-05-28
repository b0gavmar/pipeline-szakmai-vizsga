using PipeLine.Shared.Dtos.ErrorTicketDtos;
using PipeLine.Shared.Models.ErrorTicketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.HttpService
{
    public interface IErrorTicketHttpService : IBaseHttpService<ErrorTicket>
    {
        Task<int> GetNumberOfUnsolvedAsync();
        Task<List<ErrorTicketDto>> GetAllTicketsByChargingStationIdAsync(Guid id);
    }
}
