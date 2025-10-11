using AutoMapper;
using eVote.Core.Application.DTOs.AllianceRequest;
using eVote.Core.Application.Interfaces;
using eVote.Core.Domain.Entities;
using eVote.Core.Domain.Interfaces;

namespace eVote.Core.Application.Services
{
    public class AllianceRequestService : GenericService<AllianceRequest,AllianceRequestDto>, IAllianceRequestService
    {
        private IAllianceRequestRepository _allianceRequestRepository;
        private IMapper _mapper;
        public AllianceRequestService(IGenericRepository<AllianceRequest> repository, IMapper mapper, IAllianceRequestRepository repo) : base(repository, mapper)
        {
            _allianceRequestRepository = repo;
            _mapper = mapper;
        }
        public List<AllianceRequestDto> GetAllWithDetails()
        {
            try {           
                var allianceRequests = _allianceRequestRepository.GetAllListWithInclude(new List<string> { "RequestingParty", "RequestedParty" });
                return _mapper.Map<List<AllianceRequestDto>>(allianceRequests);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<AllianceRequestDto>();
            }
            
        }
    }
}
