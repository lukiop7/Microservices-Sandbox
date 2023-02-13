namespace CommandService.Dtos;

public record PlatformReadDto(int Id, string Name);

public record PlatformPublishedDto(int Id, string Name, string Event);