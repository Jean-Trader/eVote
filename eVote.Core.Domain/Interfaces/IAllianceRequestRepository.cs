using eVote.Core.Domain.Entities;

namespace eVote.Core.Domain.Interfaces
{
    public interface IAllianceRequestRepository : IGenericRepository<AllianceRequest>
    {
        List<Alliance> GetAllListWithInclude(List<string> includes);
    }
}
