using PipeLine.Backend.Repos.Base;
using PipeLine.Shared.Models.UserModels;

namespace PipeLine.Backend.Repos.UserRepos
{
    public interface IUserRepo : IBaseRepo<ApplicationUser>
    {
        Task<int> GetNumberOfUsersWithEmailAsync();
        Task<int> GetNumberOfUsersAsync();
        Task<ApplicationUser> GetByEmailAsync(string email);

        IQueryable<ApplicationUser> GetUsersWithSameLastName(string lastName);
        IQueryable<ApplicationUser> GetUsersWithSameFirstName(string firstName);
    }
}
