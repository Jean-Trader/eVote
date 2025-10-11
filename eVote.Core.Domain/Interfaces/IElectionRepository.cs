using eVote.Core.Domain.Entities;

namespace eVote.Core.Domain.Interfaces
{
    public interface IElectionRepository : IGenericRepository<Election>
    {
        List<Alliance> GetAllListWithInclude(List<string> includes);
    }
}
