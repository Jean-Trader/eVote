using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote.Core.Application.Interfaces
{
    public interface IGenericServiceWithStatus<TEntityDto> : IGenericsServices<TEntityDto>
        where TEntityDto : class
    {
        Task<TEntityDto> ChangeStatusAsync(int id);
    }
}
