using eVote.Core.Application.DTOs.Election;

namespace eVote.Core.Application.Interfaces
{
    public interface IElectionService : IGenericsRepository<ElectionDto>
    {
        List<ElectionDto> GetAllWithDetails();
    }
}
