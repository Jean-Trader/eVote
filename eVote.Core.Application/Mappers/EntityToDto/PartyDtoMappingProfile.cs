using AutoMapper;
using eVote.Core.Application.DTOs.Party;
using eVote.Core.Domain.Entities;
namespace eVote.Core.Application.Mappers.EntityToDto
{
    public class PartyDtoMappingProfile : Profile
    {
        public PartyDtoMappingProfile()
        {
            CreateMap<Party, PartyDto>()
                .ForMember(dest => dest.Candidates, opt => opt.MapFrom(src => src.Candidates))
                .ReverseMap()
                .ForMember(dest => dest.Candidates, opt => opt.Ignore());
        }
    }
}
