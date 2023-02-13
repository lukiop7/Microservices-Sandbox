namespace CommandService.Dtos;

public record CommandReadDto(int Id, string HowTo, string CommandLine, int PlatformId);

public record CommandCreateDto(string HowTo, string CommandLine, int PlatformId);