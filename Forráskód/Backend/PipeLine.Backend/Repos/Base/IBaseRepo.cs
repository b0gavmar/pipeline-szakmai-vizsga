using PipeLine.Shared.Models;
using PipeLine.Shared.Responses;
using System.Linq.Expressions;

namespace PipeLine.Backend.Repos.Base
{
    public interface IBaseRepo<TModel> where TModel : class
    {
        IQueryable<TModel> GetAll();
        Task<TModel?> GetByIdAsync(Guid id);
        Task<Response> CreateAsync(TModel entity);
        Task<Response> UpdateAsync(TModel entity);
        Task<Response> DeleteAsync(Guid id);
    }

}
