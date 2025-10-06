using eVote.Core.Domain.CommonEntity;
namespace eVote.Core.Domain.Entities
{
    public class Alliance : BaseEntity<int>
    {
        public required int Party1Id { get; set; }
        public Party? Party1 { get; set; }
        public required int Party2Id { get; set; }
        public Party? Party2 { get; set; }
        public required DateTime AcceptedDate { get; set; }
        public required bool Status { get; set; } = true;
    }
}
