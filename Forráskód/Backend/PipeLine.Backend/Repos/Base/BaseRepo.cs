using Microsoft.EntityFrameworkCore;
using PipeLine.Shared.Models;
using PipeLine.Shared.Responses;
using System.Linq.Expressions;

namespace PipeLine.Backend.Repos.Base
{
    public class BaseRepo<TDbContext, TEntity> : IBaseRepo<TEntity>
    where TDbContext : DbContext
    where TEntity : class, IDbEntity<TEntity>, new()
    {
        protected readonly TDbContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepo(TDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll() => _dbSet;

        public async Task<TEntity?> GetByIdAsync(Guid id){
            return await _dbSet.FindAsync(id);
        }

        public async Task<Response> CreateAsync(TEntity entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return new Response(entity.Id);
            }
            catch (Exception ex)
            {
                return Response.Failure($"Error while creation: {ex.Message}");
            }
        }

        public async Task<Response> UpdateAsync(TEntity entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _dbContext.SaveChangesAsync();
                return new Response(entity.Id);
            }
            catch (Exception ex)
            {
                return Response.Failure($"Error while updating: {ex.Message}");
            }
        }

        public async Task<Response> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity == null)
                    return Response.Failure($"Entity with ID {id} not found.");

                _dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
                return new Response(id);
            }
            catch (Exception ex)
            {
                return Response.Failure($"Error while deleting: {ex.Message}");
            }
        }

        public virtual async Task<bool> CanDelete(Guid id)
        {
            return true;
        }
    }
}
