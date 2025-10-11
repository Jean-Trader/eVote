using eVote.Core.Application.ViewModels.CommonEntity;
using eVote.Core.Application.ViewModels.Party;
using eVote.Core.Application.ViewModels.User;
using System.ComponentModel.DataAnnotations;

namespace eVote.Core.Application.ViewModels.PartyLeader
{
    public class PartyLeaderViewModel : BaseEntityVM<int>
    {
        [Required(ErrorMessage = "User Required")]
        public required int UserId { get; set; }
        public UserViewModel? User { get; set; }
        [Required(ErrorMessage = "Party Required")]
        public required int PartyId { get; set; }
        public PartyViewModel? Party { get; set; }
        [Required(ErrorMessage = "Assignment Date Required")]
        public required DateTime AssignmentDate { get; set; }
    }
}
