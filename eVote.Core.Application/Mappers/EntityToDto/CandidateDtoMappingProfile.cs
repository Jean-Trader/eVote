using AutoMapper;
using eVote.Core.Application.DTOs.Candidate;
using eVote.Core.Domain.Entities;

namespace eVote.Core.Application.Mappers.EntityToDto
{
    public class CandidateDtoMappingProfile : Profile
    {
        public CandidateDtoMappingProfile()
        {
            CreateMap<Candidate, CandidateDto>()
                .ForMember(dest => dest.Party, opt => opt.MapFrom(src => src.Party))
                .ForMember(dest => dest.PartyId, opt => opt.MapFrom(src => src.PartyId == 0 ? null : src.PartyId ))
                .ReverseMap()
                .ForMember(dest => dest.Party, opt => opt.Ignore())
                .ForMember(dest => dest.PartyId, opt => opt.MapFrom(src => src.PartyId == 0 ? null : src.PartyId));
        }
    }
}
