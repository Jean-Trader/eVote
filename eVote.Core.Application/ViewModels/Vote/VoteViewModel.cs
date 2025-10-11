using eVote.Core.Application.ViewModels.Candidate;
using eVote.Core.Application.ViewModels.Citizen;
using eVote.Core.Application.ViewModels.CommonEntity;
using eVote.Core.Application.ViewModels.Election;
using eVote.Core.Application.ViewModels.ElectivePosition;
using System.ComponentModel.DataAnnotations;

namespace eVote.Core.Application.ViewModels.Vote
{
    public class VoteViewModel : BaseEntityVM<int>
    {
        [Required(ErrorMessage = "Citizen Required")]
        public required int CitizenId { get; set; }
        public CitizenViewModel? Citizen { get; set; }
        [Required(ErrorMessage = "Candidate Required")]
        public required int CandidateId { get; set; }
        public CandidateViewModel? Candidate { get; set; }
        [Required(ErrorMessage = "Elective Position Required")]
        public required int ElectivePositionId { get; set; }
        public ElectivePositionViewModel? ElectivePosition { get; set; }
        [Required(ErrorMessage = "Election Required")]
        public required int ElectionId { get; set; }
        public ElectionViewModel? Election { get; set; }
        [Required(ErrorMessage = "Vote Date Required")]

        public DateTime VoteDate { get; set; }
    }
}
