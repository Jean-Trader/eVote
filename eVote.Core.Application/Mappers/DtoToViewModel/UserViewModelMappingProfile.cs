using AutoMapper;
using eVote.Core.Application.DTOs.User;
using eVote.Core.Application.ViewModels.User;

namespace eVote.Core.Application.Mappers.DtoToViewModel
{
    public class UserViewModelMappingProfile : Profile
    {
        public UserViewModelMappingProfile()
        {
            CreateMap<UserDto, CreateUserViewModel>()
               .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
               .ForMember(dest => dest.Role, opt => opt.MapFrom(src => 0))
               .ReverseMap()
               .ForMember(dest => dest.Role, opt => opt.MapFrom(src =>
               src.Role == 1 ? "Admin" :
               src.Role == 2 ? "Political" :
               "Unknown"));
        }
    }
}
