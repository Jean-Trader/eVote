using eVote.Core.Application.DTOs.PartyLeader;


namespace eVote.Core.Application.Interfaces
{
    public interface IPartyLeaderService : IGenericsRepository<PartyLeaderDto>
    {
        List<PartyLeaderDto> GetAllWithDetails();
    }
}
