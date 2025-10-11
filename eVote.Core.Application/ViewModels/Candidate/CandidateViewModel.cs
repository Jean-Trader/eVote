using eVote.Core.Application.ViewModels.CommonEntity;


namespace eVote.Core.Application.ViewModels.Candidate
{
    public class CandidateViewModel : BaseEntityPersonalVM<int>
    {
        public required string Photo { get; set; }
        public required bool Status { get; set; } = true;
        public required int PartyId { get; set; }
        public PartyViewModel? Party { get; set; }
    }
}
