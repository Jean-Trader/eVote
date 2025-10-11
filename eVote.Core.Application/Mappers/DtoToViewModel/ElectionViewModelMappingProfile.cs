using AutoMapper;
using eVote.Core.Application.DTOs.Election;
using eVote.Core.Application.ViewModels.Election;

namespace eVote.Core.Application.Mappers.DtoToViewModel
{
    public class ElectionViewModelMappingProfile : Profile
    {
        public ElectionViewModelMappingProfile()
        {
            CreateMap<ElectionDto, ElectionViewModel>()
                .ForMember(dest => dest.Votes, opt => opt.MapFrom(src => src.Votes))
                .ForMember(dest => dest.ElectivePositions, opt => opt.MapFrom(src => src.ElectivePositions))
                .ReverseMap()
                .ForMember(dest => dest.Votes, opt => opt.Ignore())
                .ForMember(dest => dest.ElectivePositions, opt => opt.Ignore());
        }
    }
}
