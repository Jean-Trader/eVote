using eVote.Core.Application.DTOs.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote.Core.Application.DTOs.Election
{
    public class ElectoralResumeDto : BaseEntityDto<int>
    {
        public string? Name { get; set; }
        public DateTime? ElectionDate { get; set; }
        public int PartiesCount { get; set; }
        public int CandidatesCount { get; set; }
        public int TotalVotes { get; set; }
    }
}
