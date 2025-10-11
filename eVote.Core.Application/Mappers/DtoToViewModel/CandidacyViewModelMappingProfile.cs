using AutoMapper;
using eVote.Core.Application.DTOs.Candidacy;
using eVote.Core.Application.ViewModels.Candidacy;

namespace eVote.Core.Application.Mappers.DtoToViewModel
{
    public class CandidacyViewModelMappingProfile : Profile
    {
        public CandidacyViewModelMappingProfile()
        {
            CreateMap<CandidacyDto, CandidacyViewModel>()
                .ForMember(dest => dest.Candidate, opt => opt.MapFrom(src => src.Candidate))
                .ForMember(dest => dest.ElectivePosition, opt => opt.MapFrom(src => src.ElectivePosition))
                .ForMember(dest => dest.Party, opt => opt.MapFrom(src => src.Party))
                .ReverseMap()
                .ForMember(dest => dest.Candidate, opt => opt.Ignore())
                .ForMember(dest => dest.Party, opt => opt.Ignore())
                .ForMember(dest => dest.ElectivePosition, opt => opt.Ignore());

        }
    }
}
