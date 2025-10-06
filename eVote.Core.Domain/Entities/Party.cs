using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eVote.Core.Domain.CommonEntity;
namespace eVote.Core.Domain.Entities
{
    public class Party : BaseEntity<int>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required bool Status { get; set; } = true;
    }
}
