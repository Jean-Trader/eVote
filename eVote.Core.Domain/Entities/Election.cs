using eVote.Core.Domain.CommonEntity;

namespace eVote.Core.Domain.Entities
{
    public class Election : BaseEntity<int>
    {
        public required string Name { get; set; }
        public DateTime? ElectionDate { get; set; }
        public required string Status { get; set; } 
        public required DateTime CreatedDate { get; set; }

        public ICollection<Vote>? Votes { get; set; }
        public ICollection<ElectivePosition>? ElectivePositions { get; set; }

    }
}
