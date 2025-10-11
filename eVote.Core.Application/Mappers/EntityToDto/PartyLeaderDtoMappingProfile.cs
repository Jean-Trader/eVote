using AutoMapper;
using eVote.Core.Application.DTOs.PartyLeader;
using eVote.Core.Domain.Entities;

namespace eVote.Core.Application.Mappers.EntityToDto
{
    public class PartyLeaderDtoMappingProfile : Profile
    {
        public PartyLeaderDtoMappingProfile()
        {
            CreateMap<PartyLeader, PartyLeaderDto>()
                .ForMember(dest => dest.Party, opt => opt.MapFrom(src => src.Party))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap()
                .ForMember(dest => dest.Party, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

        }
    }
}
