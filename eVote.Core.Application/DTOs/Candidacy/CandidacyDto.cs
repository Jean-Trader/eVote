using eVote.Core.Application.DTOs.CommonEntity;
using eVote.Core.Application.DTOs.Candidate;
using eVote.Core.Application.DTOs.ElectivePosition;
using eVote.Core.Application.DTOs.Party;
namespace eVote.Core.Application.DTOs.Candidacy
{
    public class CandidacyDto : BaseEntityDto<int>
    {
        public int CandidateId { get; set; }
        public CandidateDto? Candidate { get; set; }
        public int ElectivePositionId { get; set; }
        public ElectivePositionDto? ElectivePosition { get; set; }
        public DateTime RegistrationDate { get; set; }
        public required int PartyId { get; set; }
        public PartyDto? Party { get; set; }
    }
}
