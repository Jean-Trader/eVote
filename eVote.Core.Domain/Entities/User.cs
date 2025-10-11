
using eVote.Core.Domain.CommonEntity;

namespace eVote.Core.Domain.Entities
{
    public class User : BaseEntityPersonal<int>
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string Role { get; set; } 
        public required bool Status { get; set; } = true;
    }
}
