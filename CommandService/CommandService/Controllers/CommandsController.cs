using AutoMapper;
using CommandService.Dtos;
using CommandService.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using CommandService.Models;

namespace CommandService.Controllers;

[Route("api/c/platforms/{platformId}/[controller]")]
[ApiController]
public class CommandsController : ControllerBase
{
    private readonly ICommandRepo _repo;
    private readonly IMapper _mapper;

    public CommandsController(ICommandRepo repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
    {
        Console.WriteLine($"--> Hit GetCommandsForPlatform: {platformId}");

        if (!_repo.PlatformExist(platformId))
            return NotFound();

        var commands = _repo.GetCommandsForPlatform(platformId);

        return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
    }


    [HttpGet("{commandId}")]
    public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, [FromRoute] int commandId)
    {
        Console.WriteLine($"--> Hit GetCommandForPlatform: {platformId} / {commandId}");

        if (!_repo.PlatformExist(platformId))
            return NotFound();

        var command = _repo.GetCommand(platformId, commandId);

        if (command is null)
            return NotFound();

        return Ok(_mapper.Map<CommandReadDto>(command));
    }

    [HttpPost]
    public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandDto)
    {
        Console.WriteLine($"--> Hit CreateCommandForPlatform: {platformId}");

        if (!_repo.PlatformExist(platformId))
            return NotFound();

        var command = _mapper.Map<Command>(commandDto);

        _repo.CreateCommand(platformId, command);
        _repo.SaveChanges();

        var readDto = _mapper.Map<CommandReadDto>(command);

        return CreatedAtAction(nameof(GetCommandForPlatform),
            new { commandId = readDto.Id, platformId = readDto.PlatformId }, readDto);
    }
}