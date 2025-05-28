using PipeLine.Backend.Assemblers.ErrorTicketAssemblers;
using PipeLine.Shared.Dtos.ChargingStationDto;
using PipeLine.Shared.Dtos.ErrorTicketDtos;
using PipeLine.Shared.Models.ErrorTicketModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.HttpService
{
    public class ErrorTicketHttpService : BaseHttpService<ErrorTicket, ErrorTicketDto>, IErrorTicketHttpService
    {
        public ErrorTicketHttpService(IHttpClientFactory? httpClientFactory, IErrorTicketAssembler assembler) : base(httpClientFactory, assembler)
        {
        }

        public ErrorTicketHttpService() : base()
        {
        }

        public async Task<int> GetNumberOfUnsolvedAsync()
        {
            try
            {
                int numberOfUnsolved = await _httpClient.GetFromJsonAsync<int>(path + "/NumberOfUnsolved");
                return numberOfUnsolved;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ErrorTicketDto>> GetAllTicketsByChargingStationIdAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ErrorTicketDto>>(path + $"/TicketsOfChargingStation/{id}");
                return response ?? new List<ErrorTicketDto>();
            }
            catch (Exception ex)
            {
                return new List<ErrorTicketDto>();
            }
        }
    }
}
