using eVote.Core.Application.DTOs.CommonEntity;
using eVote.Core.Application.DTOs.Party;
using eVote.Core.Application.DTOs.User;

namespace eVote.Core.Application.DTOs.PartyLeader
{
    public class PartyLeaderDto : BaseEntityDto<int>
    {
        public required int UserId { get; set; }
        public UserDto? User { get; set; }
        public required int PartyId { get; set; }
        public PartyDto? Party { get; set; }
        public required DateTime AssignmentDate { get; set; }
    }
}
