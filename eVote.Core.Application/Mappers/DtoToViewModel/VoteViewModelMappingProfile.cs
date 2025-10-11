using AutoMapper;
using eVote.Core.Application.DTOs.Votes;
using eVote.Core.Application.ViewModels.Vote;


namespace eVote.Core.Application.Mappers.DtoToViewModel
{
    public class VoteViewModelMappingProfile : Profile
    {
        public VoteViewModelMappingProfile()
        {
            CreateMap<VoteDto, VoteViewModel>()
                .ForMember(dest => dest.Citizen, opt => opt.MapFrom(src => src.Citizen))
                .ForMember(dest => dest.Election, opt => opt.MapFrom(src => src.Election))
                .ForMember(dest => dest.ElectivePosition, opt => opt.MapFrom(src => src.ElectivePosition))
                .ForMember(dest => dest.Candidate, opt => opt.MapFrom(src => src.Candidate))
                .ReverseMap()
                .ForMember(dest => dest.Citizen, opt => opt.Ignore())
                .ForMember(dest => dest.Election, opt => opt.Ignore())
                .ForMember(dest => dest.ElectivePosition, opt => opt.Ignore());
        }

    }
}
