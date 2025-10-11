using eVote.Core.Domain.Entities;
using eVote.Infraestructure.Persistence.Context;
using eVote.Core.Application.Helpers;

namespace eVote.Infrastructure.Persistence.Seeders
{
    public static class UserAdminSeeder
    {
        public static async Task SeedAsync(eVoteDbContext context)
        {
            
            if (!context.Users.Any(u => u.Role == "Admin"))
            {

                var adminUser = new User
                {
                    Id = 1,
                    FirstName = "Admin",
                    LastName = "Sistema",
                    UserName = "Jean",
                    Email = "20241487@itla.edu.do",
                    PasswordHash = EncryptionPassword.Sha256Hash("admin123"),
                    Role = "Admin",
                    Status = true
                };

                await context.Users.AddAsync(adminUser);
                await context.SaveChangesAsync();
            }
        }
    }
}
