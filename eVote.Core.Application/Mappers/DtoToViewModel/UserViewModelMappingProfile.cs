using AutoMapper;
using eVote.Core.Application.DTOs.User;
using eVote.Core.Application.ViewModels.User;

namespace eVote.Core.Application.Mappers.DtoToViewModel
{
    public class UserViewModelMappingProfile : Profile
    {
        public UserViewModelMappingProfile()
        {
            CreateMap<UserDto, UserViewModel>().
            ReverseMap();
        }
    }
}
