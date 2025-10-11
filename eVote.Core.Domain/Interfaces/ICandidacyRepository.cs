using eVote.Core.Domain.Entities;

namespace eVote.Core.Domain.Interfaces
{
    public interface ICandidacyRepository : IGenericRepository<Candidacy>
    {
        List<Alliance> GetAllListWithInclude(List<string> includes);
    }
}
