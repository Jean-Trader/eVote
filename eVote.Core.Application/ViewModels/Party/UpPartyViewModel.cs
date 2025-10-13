using eVote.Core.Application.DTOs.Candidate;
using eVote.Core.Application.ViewModels.CommonEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote.Core.Application.ViewModels.Party
{
    public class UpPartyViewModel : BaseEntityVM<int>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Acronym { get; set; }
        public required string Logo { get; set; } 
        public required bool Status { get; set; } 
        
    }
}
