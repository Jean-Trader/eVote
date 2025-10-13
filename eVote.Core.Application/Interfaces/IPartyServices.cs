using eVote.Core.Application.DTOs.Party;

namespace eVote.Core.Application.Interfaces
{
    public interface IPartyServices : IGenericServiceWithStatus<PartyDto>
    {
        List<PartyDto> GetAllWithDetails();
    }
}
