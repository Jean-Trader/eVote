using AutoMapper;
using eVote.Core.Application.DTOs.User;
using eVote.Core.Application.Helpers;
using eVote.Core.Domain.Entities;

namespace eVote.Core.Application.Mappers.EntityToDto
{
    public class CreateUserDtoMappingProfile : Profile
    {
        public CreateUserDtoMappingProfile()
        {
            CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => EncryptionPassword.Sha256Hash(src.Password)))
            .ReverseMap();
        }
    }
}
