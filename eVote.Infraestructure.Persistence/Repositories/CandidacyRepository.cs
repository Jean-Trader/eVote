using eVote.Core.Domain.Interfaces;
using eVote.Core.Domain.Entities;
using eVote.Infraestructure.Persistence.Context;

namespace eVote.Infrastructure.Persistence.Repositories
{
    public class CandidacyRepository : GenericRepository<Candidacy>, ICandidacyRepository
    {
        public CandidacyRepository(eVoteDbContext context) : base(context)
        {
        }
    }
}
