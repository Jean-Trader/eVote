using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote.Core.Domain.Entities
{
    public class PartyLeader
    {
        public required int UserId { get; set; }
        public User? User { get; set; }

        public required int PartyId { get; set; }
        public Party? Party { get; set; }

        public required DateTime AssignmentDate { get; set; }
    }
}
