using eVote.Core.Domain.Entities;

namespace eVote.Core.Domain.Interfaces
{
    public interface IAllianceRepository : IGenericRepository<Alliance>
    {
       List<Alliance>GetAllListWithInclude(List<string> includes);
    }
}
