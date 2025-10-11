
using System.ComponentModel.DataAnnotations;

namespace eVote.Core.Application.ViewModels.CommonEntity
{
    public class BaseEntityPersonalVM<TKey> : BaseEntityVM<TKey>
    {
        [Required(ErrorMessage = "First Name Required")]
        public required string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name Required")]
        public required string LastName { get; set; }
    }
}
