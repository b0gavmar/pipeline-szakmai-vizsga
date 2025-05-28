using PipeLine.Shared.Models;
using PipeLine.Shared.Responses;

namespace PipeLine.Backend.Services.Base
{
    public interface IBaseService<TModel, TDto>
    where TModel : class, IDbEntity<TModel>, new()
    where TDto : class
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(Guid id);
        Task<Response> CreateAsync(TDto dto);
        Task<Response> UpdateAsync(TDto dto);
        Task<Response> DeleteAsync(Guid id);
        Task<bool> CanDelete(Guid id);
    }
}
