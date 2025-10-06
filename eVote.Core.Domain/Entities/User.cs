using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eVote.Core.Domain.CommonEntity;

namespace eVote.Core.Domain.Entities
{
    public class User : BaseEntity<int>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string Role { get; set; } 
        public required bool Status { get; set; } = true;
    }
}
