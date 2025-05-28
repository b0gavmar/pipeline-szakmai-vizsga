using PipeLine.Shared.Models.ChargingInstanceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.HttpService
{
    public interface IChargingInstanceHttpService : IBaseHttpService<ChargingInstance>
    {
        Task<int> GetNumberOfInstancesAsync();
    }
}
