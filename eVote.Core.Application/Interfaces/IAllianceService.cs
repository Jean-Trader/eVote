using eVote.Core.Application.DTOs.Alliance;
using eVote.Core.Application.Interfaces;
namespace eVote.Core.Application.Interfaces
{
    public interface IAllianceService : IGenericsRepository<AllianceDto>
    {
        List<AllianceDto> GetAllWithDetails();
    }
}
