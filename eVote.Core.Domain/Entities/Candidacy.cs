using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote.Core.Domain.Entities
{
    public class Candidacy
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public Candidate? Candidate { get; set; }
        public int ElectivePositionId { get; set; }
        public ElectivePosition? ElectivePosition { get; set; }
        public bool IsInAlliance { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
