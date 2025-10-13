using AutoMapper;
using eVote.Core.Application.DTOs.Citizen;
using eVote.Core.Application.Interfaces;
using eVote.Core.Domain.Entities;
using eVote.Core.Domain.Interfaces;

namespace eVote.Core.Application.Services
{
    public class CitizenService : GenericServiceWithStatus<Citizen, CitizenDto>, ICitizenServices
    {
        protected ICitizenRepository _citizenRepository;
       
        public CitizenService(IGenericRepository<Citizen> repository, IMapper mapper, ICitizenRepository repo) : base(repository, mapper)
        {
            _citizenRepository = repo;
        }
        public List<CitizenDto> GetAllWithDetails()
        {
            try
            {
                var includes = new List<string> { "Votes" };
                var citizens = _citizenRepository.GetAllListWithInclude(includes);
                return _mapper.Map<List<CitizenDto>>(citizens);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<CitizenDto>();
            }
        }
    }
}
