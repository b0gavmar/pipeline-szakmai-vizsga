using PipeLine.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.HttpService
{
    public interface IBaseHttpService<Tmodel>
    {
        Task<List<Tmodel>> GetAllAsync();
        Task<Tmodel> GetByIdAsync(Guid id);
        Task<Response> CreateAsync(Tmodel model);
        Task<Response> UpdateAsync(Tmodel model);
        Task<Response> DeleteAsync(Guid id);
    }
}
