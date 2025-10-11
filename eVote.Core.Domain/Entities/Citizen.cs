
using eVote.Core.Domain.CommonEntity;

namespace eVote.Core.Domain.Entities
{
    public class Citizen : BaseEntityPersonal<int>
    {
        public required string Email { get; set; }
        public required bool Status { get; set; } 
        public required string IdentificationNumber { get; set; }
    }
}
