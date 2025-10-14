using eVote.Core.Application.ViewModels.CommonEntity;
using eVote.Core.Application.ViewModels.Party;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote.Core.Application.ViewModels.Candidate
{
    public class CreateCandidateViewModel : BaseEntityPersonalVM<int>
    {
        [Required(ErrorMessage = "Photo Required")]
        public IFormFile? Photo { get; set; }
        public required bool Status { get; set; } = true;
        public required int PartyId { get; set; }
        public PartyViewModel? Party { get; set; }
        [Required(ErrorMessage = "Description Required")]
        public required string Description { get; set; }
    }
}
