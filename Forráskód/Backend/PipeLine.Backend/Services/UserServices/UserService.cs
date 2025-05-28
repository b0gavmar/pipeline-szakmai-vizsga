using Microsoft.EntityFrameworkCore;
using PipeLine.Backend.Assemblers.UserAssemblers;
using PipeLine.Backend.Repos.UserRepos;
using PipeLine.Backend.Services.Base;
using PipeLine.Shared.Dtos.UserDtos;
using PipeLine.Shared.Models.UserModels;

namespace PipeLine.Backend.Services.UserServices
{
    public class UserService : BaseService<ApplicationUser, ApplicationUserDto>, IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IApplicationUserAssembler _assembler;

        public UserService(IUserRepo userRepo, IApplicationUserAssembler assembler)
            : base(userRepo, assembler)
        {
            _userRepo = userRepo;
            _assembler = assembler;
        }

        public IQueryable<ApplicationUser> GetUsersWithSameLastName(string lastName)
        {
            return _userRepo.GetUsersWithSameLastName(lastName);
        }

        public IQueryable<ApplicationUser> GetUsersWithSameFirstName(string firstName)
        {
            return _userRepo.GetUsersWithSameFirstName(firstName);
        }

        public async Task<int> GetNumberOfUsersWithEmailAsync()
        {
            return await _userRepo.GetNumberOfUsersWithEmailAsync();
        }

        public async Task<int> GetNumberOfUsersAsync()
        {
            return await _userRepo.GetNumberOfUsersAsync();
        }

        public async Task<ApplicationUserDto?> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            return user is null ? null : _assembler.ToDto(user);
        }

        public async Task<ApplicationUserDto> GetByEmailAsync(string email)
        {
            return _assembler.ToDto(await _userRepo.GetByEmailAsync(email));
        }
    }
}
