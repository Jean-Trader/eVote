
using eVote.Core.Application.DTOs.Party;
using eVote.Core.Application.ViewModels.CommonEntity;
using System.ComponentModel.DataAnnotations;

namespace eVote.Core.Application.ViewModels.Alliance
{
    public class AllianceViewModel : BaseEntityVM<int>
    {
        [Required(ErrorMessage = "Party Required")]
        public required int Party1Id { get; set; }
        public PartyDto? Party1 { get; set; }
        [Required(ErrorMessage = "Party Required")]
        public required int Party2Id { get; set; }
        public PartyDto? Party2 { get; set; }
        [Required(ErrorMessage = "Date Required")]
        public required DateTime AcceptedDate { get; set; }
        public required bool Status { get; set; } 
    }
}
