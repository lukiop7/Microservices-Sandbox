using AutoMapper;
using CommandService.Dtos;
using CommandService.Models;
using PlatformService;

namespace CommandService.Profiles;

public class PlatformProfile : Profile
{
    public PlatformProfile()
    {
        CreateMap<Platform, PlatformReadDto>()
            .ForCtorParam(nameof(PlatformReadDto.Id), opt => opt.MapFrom(src => src.Id))
            .ForCtorParam(nameof(PlatformReadDto.Name), opt => opt.MapFrom(src => src.Name));

        CreateMap<PlatformPublishedDto, Platform>()
            .ForMember(dst => dst.ExternalId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<GrpcPlatformModel, Platform>()
            .ForMember(dest => dest.ExternalId, opt => opt.MapFrom(src => src.PlatformId))
            .ForMember(dest => dest.Commands, opt => opt.Ignore());
    }
}