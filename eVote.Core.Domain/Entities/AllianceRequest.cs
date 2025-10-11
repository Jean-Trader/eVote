using eVote.Core.Domain.CommonEntity;


namespace eVote.Core.Domain.Entities
{
    public class AllianceRequest : BaseEntity<int>
    {
        public required int RequestingPartyId { get; set; }
        public Party? RequestingParty { get; set; }
        public required int ReceivingPartyId { get; set; }
        public Party? ReceivingParty { get; set; }
        public required DateTime RequestDate { get; set; }
        public required string Status { get; set; } 
    }
}
