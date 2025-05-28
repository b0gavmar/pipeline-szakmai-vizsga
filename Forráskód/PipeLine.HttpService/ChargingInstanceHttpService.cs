using PipeLine.Backend.Assemblers.ChargingInstanceAssemblers;
using PipeLine.Shared.Dtos.ChargingInstance;
using PipeLine.Shared.Models.ChargingInstanceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.HttpService
{
    public class ChargingInstanceHttpService : BaseHttpService<ChargingInstance, ChargingInstanceDto>, IChargingInstanceHttpService
    {
        public ChargingInstanceHttpService() : base() { }
        public ChargingInstanceHttpService(IHttpClientFactory? httpClientFactory, IChargingInstanceAssembler chargingInstanceAssembler) : base(httpClientFactory, chargingInstanceAssembler)
        {

        }

        public async Task<int> GetNumberOfInstancesAsync()
        {
            try
            {
                int instances =  await _httpClient.GetFromJsonAsync<int>(path + "/NumberOfInstances");
                return instances;
            }
            catch (Exception ex)
            {

                return -1;
            }
        }
    }
}
