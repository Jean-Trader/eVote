using AutoMapper;
using eVote.Core.Application.DTOs.ElectivePosition;
using eVote.Core.Domain.Entities;

namespace eVote.Core.Application.Mappers.EntityToDto
{
    public class ElectivePositionDtoMappingProfile : Profile
    {
        public ElectivePositionDtoMappingProfile()
        {
            CreateMap<ElectivePosition, ElectivePositionDto>()
                .ReverseMap();
        }
    }
}
