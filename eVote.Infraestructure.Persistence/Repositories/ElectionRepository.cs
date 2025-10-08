using eVote.Core.Domain.Interfaces;
using eVote.Core.Domain.Entities;
using eVote.Infraestructure.Persistence.Context;
namespace eVote.Infrastructure.Persistence.Repositories
{
    public class ElectionRepository : GenericRepository<Election>, IElectionRepository
    {
        public ElectionRepository(eVoteDbContext context) : base(context)
        {
        }
    }
}
