using eVote.Core.Domain.Interfaces;
using eVote.Core.Domain.Entities;
using eVote.Infraestructure.Persistence.Context;

namespace eVote.Infrastructure.Persistence.Repositories
{
    public class PartyLeaderRepository : GenericRepository<PartyLeader>, IPartyLeaderRepository
    {
        public PartyLeaderRepository(eVoteDbContext context) : base(context)
        {
        }
    }
}
