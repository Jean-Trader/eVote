using AutoMapper;
using eVote.Core.Application.DTOs.Candidacy;
using eVote.Core.Domain.Entities;

namespace eVote.Core.Application.Mappers.EntityToDto
{
    public class CandidacyDtoMappingProfile : Profile
    {
        public CandidacyDtoMappingProfile()
        {
            CreateMap<Candidacy, CandidacyDto>()
                .ForMember(dest => dest.Candidate, opt => opt.MapFrom(src => src.Candidate))
                .ForMember(dest => dest.ElectivePosition, opt => opt.MapFrom(src => src.ElectivePosition))
                .ForMember(dest => dest.Party, opt => opt.MapFrom(src => src.Party))
                .ReverseMap()
                .ForMember(dest => dest.ElectivePosition, opt => opt.Ignore())
                .ForMember(dest => dest.Candidate, opt => opt.Ignore())
                .ForMember(dest => dest.Party, opt => opt.Ignore());

        }

  
    }
}
