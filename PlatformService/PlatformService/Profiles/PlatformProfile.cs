using AutoMapper;
using PlatformService.DTOs;
using PlatformService.Models;

namespace PlatformService.Profiles;

public class PlatformProfile : Profile
{
    public PlatformProfile()
    {
        CreateMap<Platform, PlatformReadDto>()
            .ForCtorParam(nameof(PlatformReadDto.Id), opt => opt.MapFrom(src => src.Id))
            .ForCtorParam(nameof(PlatformReadDto.Cost), opt => opt.MapFrom(src => src.Cost))
            .ForCtorParam(nameof(PlatformReadDto.Name), opt => opt.MapFrom(src => src.Name))
            .ForCtorParam(nameof(PlatformReadDto.Publisher), opt => opt.MapFrom(src => src.Publisher));

        CreateMap<PlatformCreateDto, Platform>();

        CreateMap<PlatformReadDto, PlatformPublishedDto>()
            .ForCtorParam(nameof(PlatformPublishedDto.Id), opt => opt.MapFrom(src => src.Id))
            .ForCtorParam(nameof(PlatformPublishedDto.Name), opt => opt.MapFrom(src => src.Name))
            .ForCtorParam(nameof(PlatformPublishedDto.Event), opt => opt.MapFrom(src => string.Empty));

        CreateMap<Platform, GrpcPlatformModel>()
            .ForMember(dest => dest.PlatformId, opt => opt.MapFrom(src => src.Id));
    }
}