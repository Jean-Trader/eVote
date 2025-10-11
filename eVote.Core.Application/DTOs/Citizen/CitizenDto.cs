using eVote.Core.Application.DTOs.CommonEntity;

namespace eVote.Core.Application.DTOs.Citizen
{
    public class CitizenDto : BaseEntityPersonalDto<int>
    {
        public required string Email { get; set; }
        public required bool Status { get; set; } = true;
        public required string IdentificationNumber { get; set; }
    }
}
