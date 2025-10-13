using eVote.Core.Application.ViewModels.CommonEntity;
using System.ComponentModel.DataAnnotations;

namespace eVote.Core.Application.ViewModels.User
{
    public class CreateUserViewModel : BaseEntityPersonalVM<int>
    {
        [Required(ErrorMessage="User Name Required")]
        public required string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password Required")]
        public required string Password { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password Required")]
        public required string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Email Required")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Role Required")]
        public required int Role { get; set; }
        public required bool Status { get; set; }
    }
}
