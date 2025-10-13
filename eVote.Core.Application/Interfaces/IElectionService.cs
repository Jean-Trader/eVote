using eVote.Core.Application.DTOs.Election;
using eVote.Core.Application.ViewModels.Election;

namespace eVote.Core.Application.Interfaces
{
    public interface IElectionService : IGenericsServices<ElectionDto>
    {
        List<ElectionDto> GetAllWithDetails();
        List<ElectoralResumeDto> GetElectoralResumeByYear(int Year);
    }
}
