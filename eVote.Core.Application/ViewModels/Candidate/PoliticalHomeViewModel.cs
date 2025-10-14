using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote.Core.Application.ViewModels.Candidate
{
    public class PoliticalHomeViewModel
    {
        public string PartyName { get; set; } = string.Empty;
        public string PartyAcronym { get; set; } = string.Empty;
        public string PartyLogoPath { get; set; } = "/images/default_logo.png";
        public int ActiveCandidates { get; set; }
        public int InactiveCandidates { get; set; }
        public int PoliticalAlliances { get; set; }
        public int PendingAllianceRequests { get; set; }
        public int CandidatesAssigned { get; set; }
    }
}
