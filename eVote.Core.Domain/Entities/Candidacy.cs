using eVote.Core.Domain.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote.Core.Domain.Entities
{
    public class Candidacy : BaseEntity<int>
    {
        public int CandidateId { get; set; }
        public Candidate? Candidate { get; set; }
        public int ElectivePositionId { get; set; }
        public ElectivePosition? ElectivePosition { get; set; }
        public DateTime RegistrationDate { get; set; }
        public required int PartyId { get; set; }
        public Party? Party { get; set; }
    }
}
