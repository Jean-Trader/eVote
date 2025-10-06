using eVote.Core.Domain.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote.Core.Domain.Entities
{
    public class AllianceRequest : BaseEntity<int>
    {
        public required int RequestingPartyId { get; set; }
        public Party? RequestingParty { get; set; }
        public required int ReceivingPartyId { get; set; }
        public Party? ReceivingParty { get; set; }
        public required DateTime RequestDate { get; set; }
        public required string Status { get; set; } // "Pending", "Accepted", "Rejected"
    }
}
