
using eVote.Core.Application.DTOs.Votes;
using eVote.Core.Application.DTOs.ElectivePosition;
using eVote.Core.Domain.CommonEntity;

namespace eVote.Core.Application.DTOs.Election
{
    public class ElectionDto : BaseEntity<int>
    {
        public required string Name { get; set; }
        public required DateTime ElectionDate { get; set; }
        public required string Status { get; set; } 
        public required DateTime CreatedDate { get; set; }
        public ICollection<VoteDto>? Votes { get; set; }
        public ICollection<ElectivePositionDto>? ElectivePositions { get; set; }
    }
}
