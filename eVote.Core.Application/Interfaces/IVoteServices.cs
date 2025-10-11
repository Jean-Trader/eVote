using eVote.Core.Application.DTOs.Votes;

namespace eVote.Core.Application.Interfaces
{
    public interface IVoteServices : IGenericsRepository<VoteDto>
    {
        List<VoteDto> GetAllWithDetails();
    }
}
