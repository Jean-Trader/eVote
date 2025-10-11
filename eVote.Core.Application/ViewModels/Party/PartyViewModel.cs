using eVote.Core.Application.ViewModels.Candidate;
using eVote.Core.Application.ViewModels.CommonEntity;
using System.ComponentModel.DataAnnotations;

namespace eVote.Core.Application.ViewModels.Party
{
    public class PartyViewModel : BaseEntityVM<int>
    {
        [Required(ErrorMessage = "Name Required")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "Description Required")]
        public required string Description { get; set; }
        [Required(ErrorMessage = "Acronym Required")]
        public required string Acronym { get; set; }
        [Required(ErrorMessage = "Logo Required")]
        public required string Logo { get; set; }
        public required bool Status { get; set; } = true;
        public ICollection<CandidateViewModel>? Candidates { get; set; }
    }
}
