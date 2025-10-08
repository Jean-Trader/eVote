using eVote.Core.Domain.Interfaces;
using eVote.Core.Domain.Entities;
using eVote.Infraestructure.Persistence.Context;
namespace eVote.Infrastructure.Persistence.Repositories
{
    public class ElectivePositionRepository : GenericRepository<ElectivePosition>, IElectivePositionRepository
    {
        public ElectivePositionRepository(eVoteDbContext context) : base(context)
        {

        }
    }
    
    
}
