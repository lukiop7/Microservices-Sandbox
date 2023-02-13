using AutoMapper;
using CommandService.Dtos;
using CommandService.Models;

namespace CommandService.Profiles;

public class CommandProfile : Profile
{
    public CommandProfile()
    {
        CreateMap<Command, CommandReadDto>()
            .ForCtorParam(nameof(CommandReadDto.Id), opt => opt.MapFrom(src => src.Id))
            .ForCtorParam(nameof(CommandReadDto.HowTo), opt => opt.MapFrom(src => src.HowTo))
            .ForCtorParam(nameof(CommandReadDto.CommandLine), opt => opt.MapFrom(src => src.CommandLine))
            .ForCtorParam(nameof(CommandReadDto.PlatformId), opt => opt.MapFrom(src => src.PlatformId));

        CreateMap<CommandCreateDto, Command>();
    }
}