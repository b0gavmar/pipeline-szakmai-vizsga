using Microsoft.AspNetCore.Identity;
using PipeLine.Shared.Models.DeviceModels;

namespace PipeLine.Shared.Models.UserModels
{
    public class ApplicationUser : IdentityUser<Guid>, IDbEntity<ApplicationUser>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public List<Device>? Devices { get; set; }
    }
}
