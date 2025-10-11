using eVote.Core.Application.DTOs.CommonEntity;
using eVote.Core.Application.DTOs.Party;

namespace eVote.Core.Application.DTOs.AllianceRequest
{
    public class AllianceRequestDto : BaseEntityDto<int>
    {
        public required int RequestingPartyId { get; set; }
        public PartyDto? RequestingParty { get; set; }
        public required int ReceivingPartyId { get; set; }
        public PartyDto? ReceivingParty { get; set; }
        public required DateTime RequestDate { get; set; }
        public required string Status { get; set; }
    }
}
