using eVote.Core.Domain.CommonEntity;

namespace eVote.Core.Domain.Entities
{
    public class PartyLeader : BaseEntity<int>
    {
        public required int UserId { get; set; }
        public User? User { get; set; }

        public required int PartyId { get; set; }
        public Party? Party { get; set; }

        public required DateTime AssignmentDate { get; set; }
    }
}
