using eVote.Core.Application.ViewModels.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote.Core.Application.ViewModels.User
{
    public class UserViewModel : BaseEntityPersonalVM<int>
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public required bool Status { get; set; }
    }
}

