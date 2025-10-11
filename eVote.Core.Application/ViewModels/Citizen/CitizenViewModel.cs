using eVote.Core.Application.ViewModels.CommonEntity;
using System.ComponentModel.DataAnnotations;

namespace eVote.Core.Application.ViewModels.Citizen
{
    public class CitizenViewModel : BaseEntityPersonalVM<int>
    {
        [Required(ErrorMessage = "Email Required")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Status Required")]
        public required bool Status { get; set; }
        [Required(ErrorMessage = "Identification Number Required")]
        public required string IdentificationNumber { get; set; }
    }
}
