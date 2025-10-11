using AutoMapper;
using eVote.Core.Application.DTOs.Citizen;
using eVote.Core.Domain.Entities;
namespace eVote.Core.Application.Mappers.EntityToDto
{
    public class CitizenDtoMappingProfile : Profile
    {
        public CitizenDtoMappingProfile()
        {
            CreateMap<Citizen, CitizenDto>()
            .ReverseMap();
        }

    }
}
