using AutoMapper;
using eVote.Core.Application.DTOs.Alliance;
using eVote.Core.Domain.Entities;

namespace eVote.Core.Application.Mappers.EntityToDto
{
    public class AllianceDtoMappingProfile : Profile
    {
        public AllianceDtoMappingProfile()
        {
            CreateMap<Alliance, AllianceDto>()
                .ForMember(dest => dest.Party1, opt => opt.MapFrom(src => src.Party1))
                .ForMember(dest => dest.Party2, opt => opt.MapFrom(src => src.Party2))
                .ReverseMap()
                .ForMember(dest => dest.Party1, opt => opt.Ignore())
                .ForMember(dest => dest.Party2, opt => opt.Ignore());
        
        }
    }
}
