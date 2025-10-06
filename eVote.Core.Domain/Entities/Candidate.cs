using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eVote.Core.Domain.CommonEntity;

namespace eVote.Core.Domain.Entities
{
    public class Candidate : BaseEntity<int>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Photo { get; set; }
        public required bool Status { get; set; } = true;
        public required int PartyId { get; set; }
        public Party? Party { get; set; }
    }
}
