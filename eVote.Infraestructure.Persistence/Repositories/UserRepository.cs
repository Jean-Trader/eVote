using eVote.Core.Domain.Entities;
using eVote.Core.Domain.Interfaces;
using eVote.Infraestructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(eVoteDbContext context) : base(context)
        {

        }
    }
}
