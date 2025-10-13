using AutoMapper;
using eVote.Core.Application.DTOs.Election;
using eVote.Core.Application.DTOs.Party;
using eVote.Core.Application.Interfaces;
using eVote.Core.Application.ViewModels.Election;
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

        public List<ElectoralResumeDto> GetElectoralResumeByYear(int year)
        {
            try
            {
                var elections = GetAllWithDetails();

                var filteredElections = elections
                    .Where(e => e.ElectionDate.Year == year)
                    .ToList();

                var resumes = new List<ElectoralResumeDto>();

                foreach (var election in filteredElections)
                {
                    
                    var votedCandidateIds = election.Votes?
                        .Select(v => v.CandidateId)
                        .Distinct().ToList() ?? new List<int>();


                    var parties = election.Votes?
                    .Select(v => v.Candidate?.Party)
                    .Where(p => p != null)
                    .Distinct().ToList() ?? new List<PartyDto>();


                    var resume = new ElectoralResumeDto 
                    {
                        Id = election.Id,
                        Name = election.Name,
                        ElectionDate = election.ElectionDate,
                        CandidatesCount = votedCandidateIds.Count,
                        PartiesCount = parties.Count,
                        TotalVotes = election.Votes?.Count ?? 0
                    };

                    resumes.Add(resume);
                }

                return resumes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error: {ex.Message}");
                return new List<ElectoralResumeDto>();
            }
        }
    }
}
