using AutoMapper;
using eVote.Core.Application.DTOs.ElectivePosition;
using eVote.Core.Application.Interfaces;
using eVote.Core.Domain.Entities;
using eVote.Core.Domain.Interfaces;

namespace eVote.Core.Application.Services
{
    public class ElectivePositionService : GenericServiceWithStatus<ElectivePosition, ElectivePositionDto>, IElectivePositionService
    {
        protected IElectivePositionRepository _electivePositionRepository;

        public ElectivePositionService(IGenericRepository<ElectivePosition> repository, IMapper mapper, IElectivePositionRepository repo) : base(repository, mapper)
        {
            _electivePositionRepository = repo;
           
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
