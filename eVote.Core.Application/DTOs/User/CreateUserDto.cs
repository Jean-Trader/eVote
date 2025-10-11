using eVote.Core.Application.DTOs.CommonEntity;

namespace eVote.Core.Application.DTOs.User
{
    public class CreateUserDto : BaseEntityPersonalDto<int>
    {
      
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public required bool Status { get; set; }
    }
}
