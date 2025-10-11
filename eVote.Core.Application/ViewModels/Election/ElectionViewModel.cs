
using eVote.Core.Application.ViewModels.CommonEntity;
using eVote.Core.Application.ViewModels.ElectivePosition;
using eVote.Core.Application.ViewModels.Vote;
using System.ComponentModel.DataAnnotations;


namespace eVote.Core.Application.ViewModels.Election
{
    public class ElectionViewModel : BaseEntityVM<int>
    {
        [Required(ErrorMessage = "Name Required")]
        public required string Name { get; set; }
        public DateTime? ElectionDate { get; set; }
        public required string Status { get; set; }
        [Required(ErrorMessage = "CreatedBy Required")]
        public required DateTime CreatedDate { get; set; }
        public ICollection<VoteViewModel>? Votes { get; set; }
        public ICollection<ElectivePositionViewModel>? ElectivePositions { get; set; }
    }
}

