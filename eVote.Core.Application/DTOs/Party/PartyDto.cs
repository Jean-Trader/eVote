using eVote.Core.Application.DTOs.Candidate;
using eVote.Core.Application.DTOs.CommonEntity;
namespace eVote.Core.Application.DTOs.Party
{
    public class PartyDto : BaseEntityDto<int>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Acronym { get; set; } 
        public required string Logo { get; set; } = null ?? "Logo";
        public required bool Status { get; set; } = true;
        public ICollection<CandidateDto>? Candidates { get; set; }
    }
}
