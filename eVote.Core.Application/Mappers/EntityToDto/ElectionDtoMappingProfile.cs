using AutoMapper;
using eVote.Core.Application.DTOs.Election;
using eVote.Core.Domain.Entities;

namespace eVote.Core.Application.Mappers.EntityToDto
{
    public class ElectionDtoMappingProfile : Profile
    {
        public ElectionDtoMappingProfile()
        {
            CreateMap<Election, ElectionDto>()
                .ForMember(dest => dest.Votes, opt => opt.MapFrom(src => src.Votes))
                .ForMember(dest => dest.ElectivePositions, opt => opt.MapFrom(src => src.ElectivePositions))
                .ReverseMap()
                .ForMember(dest => dest.Votes, opt => opt.Ignore())
                .ForMember(dest => dest.ElectivePositions, opt => opt.Ignore());
        }
    }
}
