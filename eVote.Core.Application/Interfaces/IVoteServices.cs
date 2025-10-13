using eVote.Core.Application.DTOs.Votes;

namespace eVote.Core.Application.Interfaces
{
    public interface IVoteServices : IGenericsServices<VoteDto>
    {
        List<VoteDto> GetAllWithDetails();
    }
}
