namespace PlatformService.DTOs;

public record PlatformReadDto(int Id, string Name, string Publisher, string Cost);

// todo: fluent validation
public record PlatformCreateDto(string Name, string Publisher, string Cost);

public record PlatformPublishedDto(int Id, string Name, string Event);