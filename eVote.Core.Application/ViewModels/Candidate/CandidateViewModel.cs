using eVote.Core.Application.ViewModels.CommonEntity;
using eVote.Core.Application.ViewModels.Party;
using System.ComponentModel.DataAnnotations;


namespace eVote.Core.Application.ViewModels.Candidate
{
    public class CandidateViewModel : BaseEntityPersonalVM<int>
    {
        public required string Photo { get; set; }
        public required bool Status { get; set; } = true;
        public int? PartyId { get; set; } = null;
        public PartyViewModel? Party { get; set; }
        public required string Description { get; set; }
      
    }
}
