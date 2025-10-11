using AutoMapper;
using eVote.Core.Application.DTOs.ElectivePosition;
using eVote.Core.Application.Interfaces;
using eVote.Core.Domain.Entities;
using eVote.Core.Domain.Interfaces;

namespace eVote.Core.Application.Services
{
    public class ElectivePositionService : GenericService<ElectivePosition, ElectivePositionDto>, IElectivePositionService
    {
        IElectivePositionRepository _electivePositionRepository;
        IMapper _mapper;
        public ElectivePositionService(IGenericRepository<ElectivePosition> repository, IMapper mapper, IElectivePositionRepository repo) : base(repository, mapper)
        {
            _electivePositionRepository = repo;
            _mapper = mapper;
        }
        public List<ElectivePositionDto> GetAllWithDetails()
        {
            try
            {
                var includes = new List<string> { "Candidates" };
                var electivePositions = _electivePositionRepository.GetAllListWithInclude(includes);
                return _mapper.Map<List<ElectivePositionDto>>(electivePositions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<ElectivePositionDto>();
            }
        
        }

    }
}
