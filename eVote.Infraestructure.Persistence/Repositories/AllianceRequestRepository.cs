using eVote.Core.Domain.Interfaces;
using eVote.Core.Domain.Entities;
using eVote.Infraestructure.Persistence.Context;

namespace eVote.Infrastructure.Persistence.Repositories
{
    internal class AllianceRequestRepository : GenericRepository<AllianceRequest>, IAllianceRequestRepository
    {
        public AllianceRequestRepository(eVoteDbContext context) : base(context)
        {
        }
    }
    
    
}
