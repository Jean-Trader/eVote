
using eVote.Core.Domain.CommonEntity;

namespace eVote.Core.Domain.Entities
{
    public class Candidate : BaseEntityPersonal<int>
    {
        public required string Photo { get; set; }
        public required bool Status { get; set; } = true;
        public int? PartyId { get; set; }
        public Party? Party { get; set; }
    }
}
