
namespace eVote.Core.Domain.Interfaces
{
    public interface IGenericRepository<Entity> 
        where Entity : class
    {
        Task<Entity>AddAsync(Entity entity);
        Task<Entity>UpdateAsync(int id,Entity entity);
        Task<IEnumerable<Entity>> GetAllAsync();
        Task<Entity?> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
        IQueryable<Entity>GetAllQuery();


    }
}
