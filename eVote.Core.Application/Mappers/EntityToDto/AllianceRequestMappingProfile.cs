using AutoMapper;
using eVote.Core.Application.DTOs.AllianceRequest;
using eVote.Core.Domain.Entities;

namespace eVote.Core.Application.Mappers.EntityToDto
{
    internal class AllianceRequestMappingProfile : Profile
    {
        public AllianceRequestMappingProfile()
        {
            CreateMap<AllianceRequest, AllianceRequestDto>()
                .ForMember(dest => dest.RequestingParty, opt => opt.MapFrom(src => src.RequestingParty))
                .ForMember(dest => dest.ReceivingParty, opt => opt.MapFrom(src => src.ReceivingParty))
                .ReverseMap()
                .ForMember(dest => dest.RequestingParty, opt => opt.Ignore())
                .ForMember(dest => dest.ReceivingParty, opt => opt.Ignore());
        }
    }
}
