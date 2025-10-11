using eVote.Core.Application.DTOs.Candidacy;
using eVote.Core.Application.Interfaces;
using eVote.Core.Domain.Entities;
using AutoMapper;
using eVote.Core.Domain.Interfaces;

namespace eVote.Core.Application.Services
{
    public class CandidacyService : GenericService<Candidacy, CandidacyDto>, ICandidacyServices
    {
        private readonly IMapper _mappers;
        private readonly ICandidacyRepository _repository;
        public CandidacyService(IGenericRepository<Candidacy> repository, IMapper mapper, ICandidacyRepository repo) : base(repository, mapper)
        {
            _mappers = mapper;
            _repository = repo;
        }

        public List<CandidacyDto> GetAllWithDetails()
        {
            try
            {
                var candidacies = _repository.GetAllListWithInclude(new List<string> { "Candidate", "Election" });
                return _mappers.Map<List<CandidacyDto>>(candidacies);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<CandidacyDto>();
            }
        }

    }
}
