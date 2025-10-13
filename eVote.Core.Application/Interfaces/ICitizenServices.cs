using eVote.Core.Application.DTOs.Citizen;

namespace eVote.Core.Application.Interfaces
{
    public interface ICitizenServices : IGenericServiceWithStatus<CitizenDto>
    {
        List<CitizenDto> GetAllWithDetails();
    }
}
