using AutoMapper;
using eVote.Core.Application.DTOs.Candidate;
using eVote.Core.Application.Interfaces;
using eVote.Core.Domain.Entities;
using eVote.Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote.Core.Application.Services
{
    public class CandidateService : GenericServiceWithStatus<Candidate,CandidateDto>, ICandidateService
    {
        private readonly ICandidateRepository _candidateRepo;
     
        public CandidateService(IGenericRepository<Candidate> genericRepo,IMapper mapper ,ICandidateRepository repo) : base(genericRepo ,mapper)
        {
            _candidateRepo = repo;
      

        }
        public List<CandidateDto> GetAllWithDetails()
        {
            try
            {
                var candidates = _candidateRepo.GetAllListWithInclude(new List<string> { "Party", "Election" });
                return _mapper.Map<List<CandidateDto>>(candidates);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<CandidateDto>();
            }

        }
    }
}
