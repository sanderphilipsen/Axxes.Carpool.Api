using Axxes.Carpool.Api.Exceptions;
using Axxes.Carpool.Api.Repositories.Abstractions;
using Axxes.Carpool.Api.Services;
using Axxes.Carpool.Contracts;
using Axxes.Carpool.Contracts.Models;
using Moq;

namespace Axxes.Carpool.UnitTests.ServiceTests;

public sealed class EventServiceTests
{
    private readonly Event _haxx = new("Haxx", "Internal conference", new DateTime(2023, 11, 4),
        new DateTime(2023, 11, 5), "Mallorca");

    private readonly Event _haxxInPassed = new("Haxx", "Internal conference", new DateTime(2022, 11, 4),
        new DateTime(2022, 11, 5), "Mallorca");

    [Fact]
    public void AddEvent_Should_Succeed_And_Not_Throw_An_Exception()
    {
        // Arrange
        var eventRepository = new Mock<IEventRepository>();
        eventRepository.Setup(p => p.GetAllUpcomingEvents()).Returns(new List<Event>());

        var personRepository = new Mock<IPersonRepository>();
        personRepository.Setup(p => p.GetAllPersons()).Returns(new List<Person>());

        var eventService = new EventService(eventRepository.Object, personRepository.Object);

        var eventCommand = new EventCommand(_haxx.Name, _haxx.Description, _haxx.StartDateTime, _haxx.EnDateTime, _haxx.Location);

        //Act
        var exception = Record.Exception(() => eventService.AddEvent(eventCommand));

        //Assert
        Assert.Null(exception);

    }

    [Fact]
    public void Add_Event_Should_Throw_EventAlreadyExistsException_If_There_Is_Already_A_Event_With_The_Same_Name()
    {
        // Arrange
        var eventCommand = new EventCommand("Haxx", "Internal conference", DateTime.Now.AddDays(5).AddHours(2),
            DateTime.Now.AddDays(2).AddHours(5), "Mallorca");

        var eventRepository = new Mock<IEventRepository>();


        var personRepository = new Mock<IPersonRepository>();
        personRepository.Setup(p => p.GetAllPersons()).Returns(new List<Person>());

        var eventService = new EventService(eventRepository.Object, personRepository.Object);

        //Assert
        Assert.Throws<StartDateTimeBeforeEndDateTimeException>(() => eventService.AddEvent(eventCommand));

    }

    [Fact]
    public void Add_Event_Should_Throw_EventInPassedException_If_The_StartDateTime_Is_After_The_EndDateTime()
    {
        // Arrange
        var eventCommand = new EventCommand("Haxx", "Internal conference", DateTime.Now.AddDays(5),
            DateTime.Now.AddDays(7), "Mallorce");


        var eventRepository = new Mock<IEventRepository>();
        eventRepository.Setup(p => p.GetAllUpcomingEvents()).Returns(new List<Event>() { _haxx });

        var personRepository = new Mock<IPersonRepository>();
        personRepository.Setup(p => p.GetAllPersons()).Returns(new List<Person>());

        var eventService = new EventService(eventRepository.Object, personRepository.Object);

        //Assert
        Assert.Throws<EventAlreadyExistsException>(() => eventService.AddEvent(eventCommand));

    }
    [Fact]
    public void Register_For_Event_Should_Throw_Exception_If_License_Plate_Is_Invalid()
    {
        // Arrange
        var startTime = new DateTime(2023, 10, 5, 10, 0, 0);

        var person = new Person("Sander");

        var eventRepository = new Mock<IEventRepository>();
        eventRepository.Setup(p => p.GetAllEvents()).Returns(new List<Event>() { _haxx });

        var personRepository = new Mock<IPersonRepository>();
        personRepository.Setup(p => p.GetAllPersons()).Returns(new List<Person> { person });

        var eventService = new EventService(eventRepository.Object, personRepository.Object);
        var registerCommand = new RegisterCommand(_haxx.Id, "Sander", true, true, "15-ABC-123");

        //Assert
        Assert.Throws<LicensePlateNumberException>(() => eventService.RegisterForEvent(registerCommand));
    }

    [Fact]
    public void Register_For_Event_Should_Throw_Person_Not_Found_Exception_If_Person_Does_Not_Exists()
    {
        // Arrange

        var eventRepository = new Mock<IEventRepository>();
        eventRepository.Setup(p => p.GetAllEvents()).Returns(new List<Event>() { _haxx });

        var personRepository = new Mock<IPersonRepository>();
        personRepository.Setup(p => p.GetAllPersons()).Returns(new List<Person>());

        var eventService = new EventService(eventRepository.Object, personRepository.Object);
        var registerCommand = new RegisterCommand(Guid.NewGuid(), "Haxx", true, true, "1-ABC-123");

        //Assert
        Assert.Throws<PersonNotFoundException>(() => eventService.RegisterForEvent(registerCommand));
    }

    [Fact]
    public void Register_For_Event_Should_Throw_EventNotFoundException_If_Event_Does_Not_Exists()
    {
        // Arrange
        var haxxEvent = new Event("Haxx", "Internal conference", DateTime.Now.AddDays(2).AddHours(10),
            DateTime.Now.AddDays(2).AddHours(22), "Hasselt");

        var eventRepository = new Mock<IEventRepository>();
        eventRepository.Setup(p => p.GetAllEvents()).Returns(new List<Event>());

        var personRepository = new Mock<IPersonRepository>();
        personRepository.Setup(p => p.GetAllPersons()).Returns(new List<Person>() { new("Sander") });

        var eventService = new EventService(eventRepository.Object, personRepository.Object);
        var registerCommand = new RegisterCommand(haxxEvent.Id, "Sander", true, true, "1-ABC-123");

        //Assert
        Assert.Throws<EventNotFoundException>(() => eventService.RegisterForEvent(registerCommand));
    }

    [Fact]
    public void Register_For_Event_With_Person_Who_IsOpenToCarpool_And_Candrive_Should_Throw_LicensePlateNumberException_If_LicensePlate_Is_Not_Filled_In_Does_Not_Exists()
    {
        // Arrange

        var eventRepository = new Mock<IEventRepository>();
        eventRepository.Setup(p => p.GetAllEvents()).Returns(new List<Event>() { _haxx });

        var personRepository = new Mock<IPersonRepository>();
        personRepository.Setup(p => p.GetAllPersons()).Returns(new List<Person>() { new("Sander") });

        var eventService = new EventService(eventRepository.Object, personRepository.Object);
        var registerCommand = new RegisterCommand(_haxx.Id, "Sander", true, true, null);

        //Assert

        var ex = Record.Exception(() => eventService.RegisterForEvent(registerCommand));

        Assert.IsType(typeof(LicensePlateNumberException), ex);
        Assert.Equal(ex.Message, "License plate number is required when you want to carpool and can drive.");
    }


    [Fact]
    public void Register_For_Event_That_Is_In_The_Passed_Should_Throw_PassedEventException()
    {
        // Arrange

        var eventRepository = new Mock<IEventRepository>();
        eventRepository.Setup(p => p.GetAllEvents()).Returns(new List<Event>() { _haxxInPassed });

        var personRepository = new Mock<IPersonRepository>();
        personRepository.Setup(p => p.GetAllPersons()).Returns(new List<Person>() { new("Sander") });

        var eventService = new EventService(eventRepository.Object, personRepository.Object);
        var registerCommand = new RegisterCommand(_haxxInPassed.Id, "Sander", true, true, "ABC-123");

        //Assert
        Assert.Throws<PassedEventException>(() => eventService.RegisterForEvent(registerCommand));
    }

    [Fact]
    public void Register_For_Event_That_Is_Valid_Should_Not_Throw_Any_Exception()
    {
        // Arrange

        var eventRepository = new Mock<IEventRepository>();
        eventRepository.Setup(p => p.GetAllEvents()).Returns(new List<Event>() { _haxx });

        var personRepository = new Mock<IPersonRepository>();
        personRepository.Setup(p => p.GetAllPersons()).Returns(new List<Person>() { new("Sander") });

        var eventService = new EventService(eventRepository.Object, personRepository.Object);

        var registerCommand = new RegisterCommand(_haxx.Id, "Sander", true, true, "ABC-123");

        //Act
        var exception = Record.Exception(() => eventService.RegisterForEvent(registerCommand));

        //Assert
        Assert.Null(exception);
    }

}