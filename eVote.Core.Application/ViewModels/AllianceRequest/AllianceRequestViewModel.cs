using eVote.Core.Application.ViewModels.CommonEntity;
using System.ComponentModel.DataAnnotations;


namespace eVote.Core.Application.ViewModels.AllianceRequest
{
    public class AllianceRequestViewModel : BaseEntityVM<int>
    {
        [Required(ErrorMessage = "Requesting Party Required")]
        public required int RequestingPartyId { get; set; }
        public PartyViewModel? RequestingParty { get; set; }
        [Required(ErrorMessage = "Receiving Party Required")]
        public required int ReceivingPartyId { get; set; }
        public PartyViewModel? ReceivingParty { get; set; }
        [Required(ErrorMessage = "Request Date Required")]
        public required DateTime RequestDate { get; set; }
        public required string Status { get; set; }
    }
}
