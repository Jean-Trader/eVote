using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote.Core.Application.Interfaces
{
    public interface IGenericsRepository<Entity> 
        where Entity : class
    {
        Task<List<Entity>> GetAllAsync();
        Task<Entity> GetByIdAsync(int id);
        Task<Entity>AddAsync(Entity entity);
        Task <Entity>UpdateAsync(int id, Entity entity);
        Task <bool>DeleteAsync(int id);
    }
}
