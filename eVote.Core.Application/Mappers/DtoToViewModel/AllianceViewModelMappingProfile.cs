using AutoMapper;
using eVote.Core.Application.DTOs.Alliance;
using eVote.Core.Application.ViewModels.Alliance;

namespace eVote.Core.Application.Mappers.DtoToViewModel
{
    public class AllianceViewModelMappingProfile : Profile
    {
        public AllianceViewModelMappingProfile()
        {
            CreateMap<AllianceDto, AllianceViewModel>()
                .ForMember(dest => dest.Party1, opt => opt.MapFrom(src => src.Party1))
                .ForMember(dest => dest.Party2, opt => opt.MapFrom(src => src.Party2))
                .ReverseMap()
                .ForMember(dest => dest.Party1, opt => opt.Ignore())
                .ForMember(dest => dest.Party2, opt => opt.Ignore());

        }

    }
}
