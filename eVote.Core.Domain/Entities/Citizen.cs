using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eVote.Core.Domain.CommonEntity;

namespace eVote.Core.Domain.Entities
{
    public class Citizen : BaseEntityPersonal<int>
    {
        public required string Email { get; set; }
        public required bool Status { get; set; } = true;
        public required string IdentificationNumber { get; set; }
    }
}
