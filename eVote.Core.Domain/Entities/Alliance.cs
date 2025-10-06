using eVote.Core.Domain.CommonEntity;
namespace eVote.Core.Domain.Entities
{
    public class Alliance : BaseEntity<int>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
