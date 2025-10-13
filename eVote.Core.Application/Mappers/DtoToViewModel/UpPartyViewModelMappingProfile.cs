using AutoMapper;
using eVote.Core.Application.DTOs.Party;
using eVote.Core.Application.ViewModels.Party;

namespace eVote.Core.Application.Mappers.DtoToViewModel
{
    public class UpPartyViewModelMappingProfile : Profile
    {
        public UpPartyViewModelMappingProfile() 
        { 
          CreateMap<UpPartyViewModel, PartyDto>()
         .ReverseMap();
        }

    }
}
