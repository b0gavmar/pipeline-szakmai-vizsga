using Microsoft.EntityFrameworkCore;
using PipeLine.Backend.Assemblers.BaseAssemblers;
using PipeLine.Backend.Repos.Base;
using PipeLine.Shared.Models;
using PipeLine.Shared.Responses;

namespace PipeLine.Backend.Services.Base
{
    public class BaseService<TModel, TDto> : IBaseService<TModel, TDto>
    where TModel : class, IDbEntity<TModel>, new()
    where TDto : class
    {
        protected readonly IBaseRepo<TModel> _repo;
        protected readonly IAssembler<TModel, TDto> _assembler;

        public BaseService(IBaseRepo<TModel> repo, IAssembler<TModel, TDto> assembler)
        {
            _repo = repo;
            _assembler = assembler;
        }

        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await _repo.GetAll().ToListAsync();
            return _assembler.ToDtos(entities);
        }

        public virtual async Task<TDto?> GetByIdAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : _assembler.ToDto(entity);
        }

        public virtual async Task<Response> CreateAsync(TDto dto)
        {
            var model = _assembler.ToModel(dto);
            return await _repo.CreateAsync(model);
        }

        public virtual async Task<Response> UpdateAsync(TDto dto)
        {
            var model = _assembler.ToModel(dto);
            return await _repo.UpdateAsync(model);
        }

        public virtual async Task<Response> DeleteAsync(Guid id)
        {
            if(!await CanDelete(id))
            {
                return Response.Failure("Cannot delete entity.");
            }
            return await _repo.DeleteAsync(id);
        }

        public virtual async Task<bool> CanDelete(Guid id)
        {
            return true;
        }
    }
}
