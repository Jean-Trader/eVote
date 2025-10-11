using AutoMapper;
using eVote.Core.Application.DTOs.AllianceRequest;
using eVote.Core.Application.ViewModels.AllianceRequest;

namespace eVote.Core.Application.Mappers.DtoToViewModel
{
    public class AllianceRequestViewModelMappingProfile : Profile
    {
        public AllianceRequestViewModelMappingProfile()
        {
            CreateMap<AllianceRequestDto, AllianceRequestViewModel>()
                .ForMember(dest => dest.RequestingParty, opt => opt.MapFrom(src => src.RequestingParty))
                .ForMember(dest => dest.ReceivingParty, opt => opt.MapFrom(src => src.ReceivingParty))
                .ReverseMap()
                .ForMember(dest => dest.RequestingParty, opt => opt.Ignore())
                .ForMember(dest => dest.ReceivingParty, opt => opt.Ignore());
        }
    }
}
