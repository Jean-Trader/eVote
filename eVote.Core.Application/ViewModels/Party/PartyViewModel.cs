using eVote.Core.Application.ViewModels.Candidate;
using eVote.Core.Application.ViewModels.CommonEntity;
using Microsoft.AspNetCore.Http;
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
        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "Logo Required")]
        public IFormFile? LogoFile { get; set; }
        public required bool Status { get; set; } = true;
        public ICollection<CandidateViewModel>? Candidates { get; set; }
    }
}
