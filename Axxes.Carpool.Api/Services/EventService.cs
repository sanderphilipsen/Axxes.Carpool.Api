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
        if (eventCommand.StartDateTime >= eventCommand.EnDateTime)
            throw new StartDateTimeBeforeEndDateTimeException("The start date of the event should be before the end date");

        if (_eventRepository.GetAllUpcomingEvents().Any(e => e.Name == eventCommand.Name))
            throw new EventAlreadyExistsException($"The event {eventCommand.Name} already exists");

        var newEvent = new Event(eventCommand.Name, eventCommand.Description, eventCommand.StartDateTime, eventCommand.EnDateTime,
            eventCommand.Location);

        _eventRepository.AddEvent(newEvent);
    }


    public IEnumerable<Event> GetAllEvents()
        => _eventRepository.GetAllEvents();

    public void RegisterForEvent(RegisterCommand registerCommand)
    {
        var person = _personRepository.GetAllPersons().FirstOrDefault(p => p.Name == registerCommand.PersonName);

        if (person is null)
            throw new PersonNotFoundException($"Person with name {registerCommand.PersonName} not found");

        var axxesEvent = _eventRepository.GetAllEvents()
            .FirstOrDefault(e => e.Id == registerCommand.EventId);

        if (axxesEvent is null)
            throw new EventNotFoundException($"Event with id {registerCommand.EventId} not found");

        if (axxesEvent.EnDateTime < DateTime.Now)
            throw new PassedEventException("Event is in the passed");

        if (registerCommand.CanDrive && registerCommand.OpenToCarpool)
        {
            if (registerCommand.LicensePlateNumber is null)
                throw new LicensePlateNumberException("License plate number is required when you want to carpool and can drive.");

            if (!BelgianLicensePlateNumberValidator.Validate(registerCommand.LicensePlateNumber))
                throw new LicensePlateNumberException("License plate number is not a valid belgian license plate");
        }

        var registration = new EventRegistration(person!, registerCommand.CanDrive, registerCommand.OpenToCarpool);
        axxesEvent!.Register(registration);
    }
}