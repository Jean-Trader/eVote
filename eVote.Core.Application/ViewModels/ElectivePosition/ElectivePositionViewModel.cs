using eVote.Core.Application.ViewModels.CommonEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote.Core.Application.ViewModels.ElectivePosition
{
    public class ElectivePositionViewModel : BaseEntityVM<int>
    {
        [Required(ErrorMessage = "Name Required")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "Description Required")]
        public required string Description { get; set; }
        public bool? Status { get; set; } 
    }
}
