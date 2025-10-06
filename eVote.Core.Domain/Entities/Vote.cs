using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace eVote.Core.Domain.Entities
{
    public class Vote
    {
        public int Id { get; set; }

        public int CitizenId { get; set; }
        public Citizen Citizen { get; set; }

        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }

        public int ElectivePositionId { get; set; }
        public ElectivePosition ElectivePosition { get; set; }
        public int ElectionId { get; set; }
        public Election Election { get; set; }

        public DateTime VoteDate { get; set; }
    }
}
