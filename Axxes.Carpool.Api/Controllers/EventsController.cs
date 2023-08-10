using Axxes.Carpool.Api.Services.Abstractions;
using Axxes.Carpool.Contracts;
using Microsoft.AspNetCore.Mvc;
using EventCommand = Axxes.Carpool.Contracts.EventCommand;

namespace Axxes.Carpool.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
        => _eventService = eventService;

    [HttpPost]
    public IActionResult AddEvent(EventCommand eventCommand)
    {
        try
        {
            _eventService.AddEvent(eventCommand);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);

        }
    }

    [HttpPost("register")]
    public IActionResult RegisterForEvent(RegisterCommand registerCommand)
    {
        try
        {
            _eventService.RegisterForEvent(registerCommand);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    [HttpGet]
    public IActionResult GetAllEvents()
    {
        return Ok(_eventService.GetAllEvents());
    }
}
