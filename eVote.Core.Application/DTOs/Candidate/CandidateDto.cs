using eVote.Core.Application.DTOs.CommonEntity;
using eVote.Core.Application.DTOs.Party;

namespace eVote.Core.Application.DTOs.Candidate
{
    public class CandidateDto : BaseEntityPersonalDto<int>
    {
        public required string Photo { get; set; } = "Candidate.png";
        public required bool Status { get; set; } = true;
        public required int PartyId { get; set; }
        public PartyDto? Party { get; set; }
    }
}
