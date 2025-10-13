using System.Linq;
using System.Threading.Tasks;
using eVote.Core.Application.Helpers;
using eVote.Core.Application.Interfaces;
using eVote.Core.Domain.Entities;
using eVote.Core.Domain.Interfaces;
namespace eVote.Core.Application.Services
{
    public class DefaultUser : IDefaultUser
    {
        private readonly IUserRepository _userService;

        public DefaultUser(IUserRepository userService)
        {
            _userService = userService;
        }

        public async Task DefaultEntity()
        {
            var users = await _userService.GetAllAsync();

            if (!users.Any(u => u.Role == "Admin"))
            {
                var adminUser = new User
                {
                    Id = 0,
                    FirstName = "Admin",
                    LastName = "Sistema",
                    UserName = "Jean",
                    Email = "20241487@itla.edu.do",
                    PasswordHash = EncryptionPassword.Sha256Hash("admin123"),
                    Role = "Admin",
                    Status = true
                };
                if (adminUser != null)
                await _userService.AddAsync(adminUser);
            }
        }
    }
}
