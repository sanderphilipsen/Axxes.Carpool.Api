using Axxes.Carpool.Api.Exceptions;
using Axxes.Carpool.Api.Repositories.Abstractions;
using Axxes.Carpool.Api.Services.Abstractions;
using Axxes.Carpool.Api.Validators;
using Axxes.Carpool.Contracts;
using Axxes.Carpool.Contracts.Models;

namespace Axxes.Carpool.Api.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IPersonRepository _personRepository;

    public EventService(IEventRepository eventRepository, IPersonRepository personRepository)
    {
        _eventRepository = eventRepository;
        _personRepository = personRepository;
    }

    public void AddEvent(EventCommand eventCommand)
    {
    }


    public IEnumerable<Event> GetAllEvents()
        => _eventRepository.GetAllEvents();

    public void RegisterForEvent(RegisterCommand registerCommand)
    {
    }
}