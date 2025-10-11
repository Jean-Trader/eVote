using AutoMapper;
using eVote.Core.Application.DTOs.Citizen;
using eVote.Core.Application.ViewModels.Citizen;

namespace eVote.Core.Application.Mappers.DtoToViewModel
{
    public class CitizenViewModelMappingProfile : Profile
    {
        public CitizenViewModelMappingProfile()
        {
            CreateMap<CitizenDto, CitizenViewModel>()

                .ReverseMap();
                
        }
    }
}
