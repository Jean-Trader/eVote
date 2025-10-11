using AutoMapper;
using eVote.Core.Application.DTOs.ElectivePosition;
using eVote.Core.Application.ViewModels.ElectivePosition;

namespace eVote.Core.Application.Mappers.DtoToViewModel
{
    public class ElectivePositionViewModelMappingProfile : Profile
    {
        public ElectivePositionViewModelMappingProfile()
        {
            CreateMap<ElectivePositionDto, ElectivePositionViewModel>()
                .ReverseMap();
        }
    }
}
