using eVote.Core.Application.DTOs.Alliance;
using eVote.Core.Application.DTOs.Candidacy;


namespace eVote.Core.Application.Interfaces
{
    public interface ICandidacyServices : IGenericsRepository<CandidacyDto>
    {
        List<CandidacyDto> GetAllWithDetails();
    }
}
