using eVote.Core.Application.DTOs.CommonEntity;
using eVote.Core.Application.DTOs.Candidate;
using eVote.Core.Application.DTOs.Citizen;
using eVote.Core.Application.DTOs.Election;
using eVote.Core.Application.DTOs.ElectivePosition;

namespace eVote.Core.Application.DTOs.Votes
{
    public class VoteDto : BaseEntityDto<int>
    {
        public required int CitizenId { get; set; }
        public CitizenDto? Citizen { get; set; }

        public required int CandidateId { get; set; }
        public CandidateDto? Candidate { get; set; }

        public required int ElectivePositionId { get; set; }
        public ElectivePositionDto? ElectivePosition { get; set; }
        public required int ElectionId { get; set; }
        public ElectionDto? Election { get; set; }

        public DateTime VoteDate { get; set; }
    }
}
