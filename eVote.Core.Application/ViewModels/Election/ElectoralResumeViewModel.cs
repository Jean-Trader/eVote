using eVote.Core.Application.ViewModels.CommonEntity;


namespace eVote.Core.Application.ViewModels.Election
{
    public class ElectoralResumeViewModel : BaseEntityVM<int>
    {
        public string? Name { get; set; }
        public DateTime? ElectionDate { get; set; }
        public int PartiesCount { get; set; }
        public int CandidatesCount { get; set; }
        public int TotalVotes { get; set; }
    }
}
