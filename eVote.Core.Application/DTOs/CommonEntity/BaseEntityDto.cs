
namespace eVote.Core.Application.DTOs.CommonEntity
{
    public class BaseEntityDto<TKey>
    {
        public required TKey Id { get; set; }
    }
}
