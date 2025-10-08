using eVote.Core.Domain.Interfaces;
using eVote.Core.Domain.Entities;
using eVote.Infraestructure.Persistence.Context;

namespace eVote.Infrastructure.Persistence.Repositories
{
    public class PartyRepository : GenericRepository<Party>, IPartyRepository
    {
        public PartyRepository(eVoteDbContext context) : base(context)
        {
        }
    }
    

    
}
