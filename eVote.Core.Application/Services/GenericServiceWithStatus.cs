using AutoMapper;
using eVote.Core.Application.Interfaces;
using eVote.Core.Domain.Interfaces;

namespace eVote.Core.Application.Services
{
    public class GenericServiceWithStatus<TEntity, TEntityDto> : GenericService<TEntity, TEntityDto>, IGenericServiceWithStatus<TEntityDto>
        where TEntityDto : class
        where TEntity : class
    {
       
       
        public GenericServiceWithStatus(IGenericRepository<TEntity> repo, IMapper mapper) : base(repo, mapper)
        {
            
        }

        public async Task<TEntityDto> ChangeStatusAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            
            if (entity == null)
            {
                throw new Exception($"{typeof(TEntity).Name} with ID {id} not found.");
            }

            var property = entity.GetType().GetProperty("Status");
            if (property != null && property.PropertyType == typeof(bool))
            {
                bool currentValue = (bool)property.GetValue(entity)!;
                property.SetValue(entity, !currentValue);

                var updatedEntity = await _repo.UpdateAsync(id, entity);
                return _mapper.Map<TEntityDto>(updatedEntity);
            }

            throw new Exception($"{typeof(TEntity).Name}");
        }
    }
}
