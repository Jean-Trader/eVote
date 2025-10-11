using AutoMapper;
using eVote.Core.Application.DTOs.User;
using eVote.Core.Domain.Entities;

namespace eVote.Core.Application.Mappers.EntityToDto
{
    public class UserDtoMappingProfile : Profile
    {
        public UserDtoMappingProfile()
        {
            CreateMap<User, UserDto>()
          
            .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.UserName))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.Role, opt => opt.MapFrom(s => s.Role))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
            
            .ReverseMap();
        }
    }
}
