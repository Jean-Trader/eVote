using eVote.Core.Application.DTOs.CommonEntity;
using eVote.Core.Application.DTOs.Party;

namespace eVote.Core.Application.DTOs.Alliance
{
    public class AllianceDto : BaseEntityDto<int>
    {
        public required int Party1Id { get; set; }
        public PartyDto? Party1 { get; set; }
        public required int Party2Id { get; set; }
        public PartyDto? Party2 { get; set; }
        public required DateTime AcceptedDate { get; set; }
        public required bool Status { get; set; } = true;
    }
}
