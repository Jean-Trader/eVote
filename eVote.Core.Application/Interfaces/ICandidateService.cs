
using eVote.Core.Application.DTOs.Alliance;
using eVote.Core.Application.DTOs.Candidate;

namespace eVote.Core.Application.Interfaces
{
    public interface ICandidateService : IGenericsServices<CandidateDto>
    {
        List<CandidateDto> GetAllWithDetails();
    }
}
