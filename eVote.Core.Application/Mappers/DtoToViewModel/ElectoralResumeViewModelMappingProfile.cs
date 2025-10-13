using AutoMapper;
using eVote.Core.Application.DTOs.Election;
using eVote.Core.Application.ViewModels.Election;

namespace eVote.Core.Application.Mappers.DtoToViewModel
{
    public class ElectoralResumeViewModelMappingProfile : Profile
    {
        public ElectoralResumeViewModelMappingProfile()
        {
            CreateMap<ElectoralResumeDto, ElectoralResumeViewModel>()
                .ReverseMap();
        }
    }
}
