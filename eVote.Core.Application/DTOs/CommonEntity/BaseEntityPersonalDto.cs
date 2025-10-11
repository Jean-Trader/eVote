
namespace eVote.Core.Application.DTOs.CommonEntity
{
    public class BaseEntityPersonalDto<TKey> : BaseEntityDto<TKey>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
