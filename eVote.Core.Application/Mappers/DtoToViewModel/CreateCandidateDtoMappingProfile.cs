using AutoMapper;
using eVote.Core.Application.DTOs.Candidate;
using eVote.Core.Application.ViewModels.Candidate;

namespace eVote.Core.Application.Mappers.DtoToViewModel
{
    public class CreateCandidateDtoMappingProfile : Profile
    {
        public CreateCandidateDtoMappingProfile() 
        {
           CreateMap<CandidateDto, CreateCandidateViewModel>()
            .ForMember(dest => dest.Photo, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Photo, opt => opt.Ignore());
        }
    }
}
