
using eVote.Core.Application.DTOs.ElectivePosition;

namespace eVote.Core.Application.Interfaces
{
    public interface IElectivePositionService : IGenericServiceWithStatus<ElectivePositionDto>
    {
        List<ElectivePositionDto> GetAllWithDetails();
    }
}
