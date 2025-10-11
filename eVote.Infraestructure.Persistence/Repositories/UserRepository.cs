using eVote.Core.Domain.Entities;
using eVote.Core.Domain.Interfaces;
using eVote.Infraestructure.Persistence.Context;
using eVote.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;


namespace eVote.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public eVoteDbContext _contextU;
        public UserRepository(eVoteDbContext context) : base(context)
        {
            _contextU = context;
        }

        public async Task<User?> LoginAsync(string userName, string Password)
        {
            userName = userName.Trim().ToLower();
            var user = await _contextU.Set<User>().FirstOrDefaultAsync(u => u.UserName == userName && Password == u.PasswordHash);
            if (user == null)
            {
                return null;
            }
            return user;
        }
    }
}
