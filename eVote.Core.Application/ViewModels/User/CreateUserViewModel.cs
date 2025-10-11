using eVote.Core.Application.ViewModels.CommonEntity;

namespace eVote.Core.Application.ViewModels.User
{
    public class CreateUserViewModel : BaseEntityPersonalVM<int>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public required bool Status { get; set; }
    }
}
