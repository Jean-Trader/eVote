using eVote.Core.Application.DTOs.Party;

namespace eVote.Core.Application.Interfaces
{
    public interface IPartyServices : IGenericsRepository<PartyDto>
    {
        List<PartyDto> GetAllWithDetails();
    }
}
