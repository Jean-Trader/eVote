
using eVote.Core.Domain.CommonEntity;
namespace eVote.Core.Domain.Entities
{
    public class Party : BaseEntity<int>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Acronym { get; set; }
        public required string Logo { get; set; } 
        public required bool Status { get; set; } = true;
        public ICollection<Candidate>? Candidates { get; set; }
    }
}
