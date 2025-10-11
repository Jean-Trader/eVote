using eVote.Core.Domain.Entities;

namespace eVote.Core.Domain.Interfaces
{
    public interface ICitizenRepository : IGenericRepository<Citizen>
    {
        List<Alliance> GetAllListWithInclude(List<string> includes);
    }
}
