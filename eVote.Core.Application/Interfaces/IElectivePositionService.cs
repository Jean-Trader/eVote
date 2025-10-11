
using eVote.Core.Application.DTOs.ElectivePosition;

namespace eVote.Core.Application.Interfaces
{
    public interface IElectivePositionService : IGenericsRepository<ElectivePositionDto>
    {
        List<ElectivePositionDto> GetAllWithDetails();
    }
}
