using PipeLine.Backend.Services.Base;
using PipeLine.Shared.Dtos.UserDtos;
using PipeLine.Shared.Models.UserModels;

namespace PipeLine.Backend.Services.UserServices
{
    public interface IUserService : IBaseService<ApplicationUser,ApplicationUserDto>
    {
        Task<int> GetNumberOfUsersWithEmailAsync();
        Task<int> GetNumberOfUsersAsync();

        Task<ApplicationUserDto> GetByEmailAsync(string email);
        IQueryable<ApplicationUser> GetUsersWithSameLastName(string lastName);
        IQueryable<ApplicationUser> GetUsersWithSameFirstName(string firstName);
        Task<ApplicationUserDto?> GetUserByIdAsync(Guid id);
    }
}
