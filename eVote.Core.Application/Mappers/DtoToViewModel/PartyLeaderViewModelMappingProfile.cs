using AutoMapper;
using eVote.Core.Application.DTOs.PartyLeader;
using eVote.Core.Application.ViewModels.PartyLeader;
namespace eVote.Core.Application.Mappers.DtoToViewModel
{
    public class PartyLeaderViewModelMappingProfile : Profile
    {
        public PartyLeaderViewModelMappingProfile()
        {
            CreateMap<PartyLeaderDto, PartyLeaderViewModel>()
                .ForMember(dest => dest.Party, opt => opt.MapFrom(src => src.Party))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap()
                .ForMember(dest => dest.Party, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());
        }
    }
}
