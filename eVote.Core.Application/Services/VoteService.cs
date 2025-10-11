using AutoMapper;
using eVote.Core.Application.DTOs.Votes;
using eVote.Core.Application.Interfaces;
using eVote.Core.Domain.Entities;
using eVote.Core.Domain.Interfaces;
namespace eVote.Core.Application.Services
{
    public class VoteService : GenericService<Vote, VoteDto> , IVoteServices
    {
        private readonly IVoteRepository _repository;
        private readonly IMapper _mapper;
        public VoteService(IGenericRepository<Vote> repository, IMapper mapper, IVoteRepository repo) : base(repository, mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        public List<VoteDto> GetAllWithDetails()
        {
            try
            {
                var votes = _repository.GetAllListWithInclude(new List<string> { "Candidacy", "Candidacy.Candidate", "Candidacy.Election", "Voter" });
                return _mapper.Map<List<VoteDto>>(votes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<VoteDto>();
            }
        }

    }
   
}
