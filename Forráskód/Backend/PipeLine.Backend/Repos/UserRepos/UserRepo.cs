using Microsoft.EntityFrameworkCore;
using PipeLine.Backend.Context;
using PipeLine.Backend.Repos.Base;
using PipeLine.Shared.Models.UserModels;
using PipeLine.Shared.Responses;

namespace PipeLine.Backend.Repos.UserRepos
{
    public class UserRepo<TDbContext> : BaseRepo<TDbContext, ApplicationUser>, IUserRepo where TDbContext : PipeLineMySqlContext
    {
        public UserRepo(TDbContext? dbContext) : base(dbContext)
        {
        }

        public IQueryable<ApplicationUser> GetUsersWithSameLastName(string lastName)
        {
            return GetAll().Where(applicationUser => applicationUser.LastName == lastName);
        }

        public IQueryable<ApplicationUser> GetUsersWithSameFirstName(string firstName)
        {
            return GetAll().Where(applicationUser => applicationUser.FirstName == firstName);
        }

        public async Task<int> GetNumberOfUsersWithEmailAsync()
        {
            return await Task.Run(() => _dbSet!.Count(applicationUser => applicationUser.Email != null));
        }
        public async Task<int> GetNumberOfUsersAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<Response> UpdateAsync(ApplicationUser entity)
        {
            Response response = new();
            try
            {
                if (_dbContext is not null)
                {
                    var existingEntity = await _dbSet.FindAsync(entity.Id);
                    if (existingEntity is null)
                    {
                        response.AppendNewError($"Entity with ID {entity.Id} not found!");
                        return response;
                    }

                    _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);

                    _dbContext.Entry(existingEntity).Property(e => e.PasswordHash).IsModified = false;
                    _dbContext.Entry(existingEntity).Property(e => e.SecurityStamp).IsModified = false;

                    await _dbContext.SaveChangesAsync();
                    return response;
                }
            }
            catch (Exception e)
            {
                response.AppendNewError(e.Message);
            }
            response.AppendNewError($"An error occurred in {nameof(UpdateAsync)} of class {nameof(BaseRepo<TDbContext, ApplicationUser>)}.");
            response.AppendNewError($"{entity} couldn't be updated!");
            return response;
        }

        public async Task<ApplicationUser> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.NormalizedEmail == email.ToUpper());
        }
    }
}
