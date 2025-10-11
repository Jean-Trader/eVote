
namespace eVote.Core.Application.ViewModels.CommonEntity
{
    public class BaseEntityVM<TKey>
    {
        public required TKey Id { get; set; }
    }
}
