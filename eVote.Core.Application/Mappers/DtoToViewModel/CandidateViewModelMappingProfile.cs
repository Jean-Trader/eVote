using AutoMapper;
using eVote.Core.Application.DTOs.Candidate;
using eVote.Core.Application.ViewModels.Candidate;

namespace eVote.Core.Application.Mappers.DtoToViewModel
{
    public class CandidateViewModelMappingProfile : Profile
    {
        public CandidateViewModelMappingProfile()
        {
            CreateMap<CandidateDto, CandidateViewModel>()
                .ForMember(dest => dest.Party, opt => opt.MapFrom(src => src.Party))
                .ReverseMap()
                .ForMember(dest => dest.Party, opt => opt.Ignore());
        }
    }
}
