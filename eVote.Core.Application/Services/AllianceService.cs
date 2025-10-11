using eVote.Core.Application.DTOs.Alliance;
using eVote.Core.Application.Interfaces;
using eVote.Core.Domain.Entities;
using AutoMapper;

using eVote.Core.Domain.Interfaces;

namespace eVote.Core.Application.Services
{
    public class AllianceService : GenericService<Alliance, AllianceDto>, IAllianceService
    {
        private readonly IMapper _mappers;
        private readonly IAllianceRepository _repository;
        public AllianceService(IGenericRepository<Alliance> repository,IAllianceRepository repo ,IMapper mapper) : base(repository, mapper)
        {
            _repository = repo;
            _mappers = mapper;
        }

        public List<AllianceDto> GetAllWithDetails()
        {
            try
            {
                var alliances =  _repository.GetAllListWithInclude(new List<string> { "Party1", "Party2" });
                return _mappers.Map<List<AllianceDto>>(alliances);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }
    }


}
