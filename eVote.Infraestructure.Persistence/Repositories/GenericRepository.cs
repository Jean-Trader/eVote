using eVote.Core.Domain.Interfaces;
using eVote.Infraestructure.Persistence.Context;
using eVote.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<Entity> : IGenericRepository<Entity>
        where Entity : class
    {
        public readonly eVoteDbContext _context;

        public GenericRepository(eVoteDbContext context) 
        {
            _context = context;
        }

        public virtual async Task<Entity> AddAsync(Entity entity) 
        {
            CommonException.IsNull(entity,$"{entity.GetType}");

            await _context.Set<Entity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<IEnumerable<Entity>> GetAllAsync() 
        {
            var entity = _context.Set<Entity>().AsQueryable();

            var result = CommonException.Exists<Entity>(entity.ToList());

            return await entity.ToListAsync();
        }

        public virtual async Task<Entity?> GetByIdAsync(int id) 
        {
          var entity = await _context.Set<Entity>().FindAsync(id);

          CommonException.NotFound(entity!, $"not found");

          return entity;

        }
        public virtual async Task<Entity> UpdateAsync(int id, Entity entry) 
        {
            var entity = await GetByIdAsync(id);

            CommonException.IsNull(entry, $"{entry?.GetType()}");
            CommonException.NotFound(entity!, $"{typeof(Entity).Name} with id {id} not found");

            _context.Entry(entity!).CurrentValues.SetValues(entry!);
            await _context.SaveChangesAsync();
            return entity!;
        }

        public virtual async Task<bool> DeleteAsync(int id) 
        {
            var entity = await GetByIdAsync(id);

            CommonException.NotFound(entity!, $"{typeof(Entity).Name} with id {id} not found");

            _context.Set<Entity>().Remove(entity!);
            await _context.SaveChangesAsync();
            return true;
            
        }

        public virtual async Task<List<Entity>> AddRangeAsync(List<Entity> entities) 
        {

            var val = entities.FirstOrDefault();

            CommonException.IsNull(val, $"{entities.GetType}");

            await _context.Set<Entity>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities;

        }

        public virtual IQueryable<Entity> GetAllQuery() 
        {
          var entity = _context.Set<Entity>().AsQueryable();
          var result = CommonException.ExistsQuery<Entity>(entity);

          return result;
        }

        public virtual IQueryable<Entity>GetAllQueryWithInclude(List<string> includes) 
        {
            var query = _context.Set<Entity>().AsQueryable();


            foreach (var property in includes)
            {
                query = query.Include(property);
            }

            var result = CommonException.ExistsQuery<Entity>(query);


            return result; 
        }

        public virtual List<Entity> GetAllListWithInclude(List<string> includes) 
        { 
          var query = _context.Set<Entity>().AsQueryable();

          foreach(var property in includes) 
          { 
             query = query.Include(property);
          }

           var result = CommonException.Exists<Entity>(query.ToList());

            return result;
        }
    }
}
