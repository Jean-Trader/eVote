using AutoMapper;
using eVote.Core.Application.DTOs.Party;
using eVote.Core.Application.ViewModels.Party;

namespace eVote.Core.Application.Mappers.DtoToViewModel
{
    public class PartyViewModelMappingProfile : Profile
    {
        public PartyViewModelMappingProfile()
        {
            CreateMap<PartyDto, PartyViewModel>()
                .ForMember(dest => dest.Candidates, opt => opt.MapFrom(src => src.Candidates))
                .ReverseMap()
                .ForMember(dest => dest.Candidates, opt => opt.Ignore());

        }
    }
}
