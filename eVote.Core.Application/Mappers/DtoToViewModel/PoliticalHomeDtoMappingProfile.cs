using AutoMapper;
using eVote.Core.Application.DTOs.Candidate;
using eVote.Core.Application.ViewModels.Candidate;

namespace eVote.Core.Application.Mappers.DtoToViewModel
{
    public class PoliticalHomeDtoMappingProfile : Profile
    {
        public PoliticalHomeDtoMappingProfile() 
        {
            CreateMap<PoliticalHomeDto, PoliticalHomeViewModel>()
                   .ReverseMap();
        }
    }
}
