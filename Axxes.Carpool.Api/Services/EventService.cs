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
        if (eventCommand.StartDateTime > eventCommand.EnDateTime)
            throw new EndDateTimeBeforeStartDateTimeException("EndDateTime is before the StartDateTime");

        if (_eventRepository.GetAllEvents().Any(e => e.Name == eventCommand.Name))
            throw new EventAlreadyExistsException("Event already exists");


        var axxesEvent = new Event(eventCommand.Name, eventCommand.Description, eventCommand.StartDateTime,
                       eventCommand.EnDateTime, eventCommand.Location);

        _eventRepository.AddEvent(axxesEvent);

    }

    public IEnumerable<Event> GetAllEvents()
        => _eventRepository.GetAllEvents();

    public void RegisterForEvent(RegisterCommand registerCommand)
    {

    }
}