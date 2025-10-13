using AutoMapper;
using eVote.Core.Application.DTOs.Party;
using eVote.Core.Application.Interfaces;
using eVote.Core.Domain.Entities;
using eVote.Core.Domain.Interfaces;
namespace eVote.Core.Application.Services
{
    public class PartyService : GenericServiceWithStatus<Party, PartyDto>, IPartyServices
    {
        IPartyRepository _partyRepository;

        public PartyService(IGenericRepository<Party> repository, IMapper mapper, IPartyRepository repo) : base(repository, mapper)
        {
            _partyRepository = repo;
        }
        public List<PartyDto> GetAllWithDetails()
        {
            try
            {
                var includes = new List<string> { "Candidates" };
                var parties = _partyRepository.GetAllListWithInclude(includes);
                return _mapper.Map<List<PartyDto>>(parties);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<PartyDto>();
            }
           
        }
    }
}
