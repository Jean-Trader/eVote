
using eVote.Core.Application.ViewModels.Candidate;
using eVote.Core.Application.ViewModels.CommonEntity;
using eVote.Core.Application.ViewModels.ElectivePosition;
using eVote.Core.Application.ViewModels.Party;
using System.ComponentModel.DataAnnotations;

namespace eVote.Core.Application.ViewModels.Candidacy
{
    public class CandidacyViewModel : BaseEntityVM<int>
    {
        [Required(ErrorMessage = "Candidate required")]
        public int CandidateId { get; set; }
        public CandidateViewModel? Candidate { get; set; }
        [Required(ErrorMessage = "Elective Position required")]
        public int ElectivePositionId { get; set; }
        public ElectivePositionViewModel? ElectivePosition { get; set; }
        [Required(ErrorMessage = "Registration Date required")]
        public DateTime RegistrationDate { get; set; }
        [Required(ErrorMessage = "Party required")]
        public required int PartyId { get; set; }
        public PartyViewModel? Party { get; set; }
    }
}
