using eVote.Core.Application.DTOs.PartyLeader;


namespace eVote.Core.Application.Interfaces
{
    public interface IPartyLeaderService : IGenericsServices<PartyLeaderDto>
    {
        List<PartyLeaderDto> GetAllWithDetails();
    }
}
