using eVote.Core.Domain.Entities;

namespace eVote.Core.Domain.Interfaces
{
    public interface IPartyRepository : IGenericRepository<Party>
    {
        List<Alliance> GetAllListWithInclude(List<string> includes);
    }
}
