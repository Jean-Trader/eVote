using AutoMapper;
using eVote.Core.Application.DTOs.Election;
using eVote.Core.Application.Interfaces;
using eVote.Core.Domain.Entities;
using eVote.Core.Domain.Interfaces;

namespace eVote.Core.Application.Services
{
    public class ElectionService : GenericService<Election, ElectionDto>, IElectionService
    {
        IElectionRepository _electionRepository;
        IMapper _mapper;
        public ElectionService(IGenericRepository<Election> repository, IMapper mapper, IElectionRepository repo) : base(repository, mapper)
        {
            _electionRepository = repo;
            _mapper = mapper;
        }
        public List<ElectionDto> GetAllWithDetails()
        {
            try
            {
                var includes = new List<string> { "Candidates", "Votes" };
                var elections = _electionRepository.GetAllListWithInclude(includes);
                return _mapper.Map<List<ElectionDto>>(elections);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<ElectionDto>();
            }
           
        }
    }
}
