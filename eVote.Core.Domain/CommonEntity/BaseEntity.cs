using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote.Core.Domain.CommonEntity
{
    public class BaseEntity<TKey>
    {
        public required TKey Id { get; set; }
    }
}
