using AutoMapper;
using eVote.Core.Application.DTOs.PartyLeader;
using eVote.Core.Application.Interfaces;
using eVote.Core.Domain.Entities;
using eVote.Core.Domain.Interfaces;

namespace eVote.Core.Application.Services
{
    public class PartyLeaderService : GenericService<PartyLeader, PartyLeaderDto>, IPartyLeaderService
    {
        IPartyLeaderRepository _partyLeaderRepository;
        IMapper _mapper;
        public PartyLeaderService(IGenericRepository<PartyLeader> repository, IMapper mapper, IPartyLeaderRepository repo) : base(repository, mapper)
        {
            _partyLeaderRepository = repo;
            _mapper = mapper;
        }

        public List<PartyLeaderDto> GetAllWithDetails()
        {
            try
            {
                var includes = new List<string> { "Party" };
                var partyLeaders = _partyLeaderRepository.GetAllListWithInclude(includes);
                return _mapper.Map<List<PartyLeaderDto>>(partyLeaders);
            }
            catch (Exception ex)
            {
               Console.WriteLine($"An error occurred: {ex.Message}");
               return new List<PartyLeaderDto>();
            }
          
        }
    }
    

    
}
