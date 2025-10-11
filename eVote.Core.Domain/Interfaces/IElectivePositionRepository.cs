using eVote.Core.Domain.Entities;

namespace eVote.Core.Domain.Interfaces
{
    public interface IElectivePositionRepository : IGenericRepository<ElectivePosition>
    {
        List<Alliance> GetAllListWithInclude(List<string> includes);
    }
}
