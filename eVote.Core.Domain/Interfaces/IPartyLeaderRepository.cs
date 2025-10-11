using eVote.Core.Domain.Entities;

namespace eVote.Core.Domain.Interfaces
{
    public interface IPartyLeaderRepository : IGenericRepository<PartyLeader>
    {
        List<Alliance> GetAllListWithInclude(List<string> includes);
    }
}
