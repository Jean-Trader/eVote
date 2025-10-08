using eVote.Core.Domain.Interfaces;
using eVote.Core.Domain.Entities;
using eVote.Infraestructure.Persistence.Context;
namespace eVote.Infrastructure.Persistence.Repositories
{
     public class AllianceRepository : GenericRepository<Alliance>, IAllianceRepository
     {
        public AllianceRepository(eVoteDbContext context) : base(context)
        {

        }
    }
}
