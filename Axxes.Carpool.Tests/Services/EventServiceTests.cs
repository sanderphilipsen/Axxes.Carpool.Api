using Axxes.Carpool.Api.Exceptions;
using Axxes.Carpool.Api.Repositories.Abstractions;
using Axxes.Carpool.Api.Services;
using Axxes.Carpool.Contracts;
using Axxes.Carpool.Contracts.Models;
using Moq;

namespace Axxes.Carpool.Tests.Services;

public class EventServiceTests
{
    public EventServiceTests()
    {

    }

    [Fact]
    public void AddEvent_With_Valid_Input_Data_Should_Not_Throw_Any_Exception()
    {
        //Arrange
        var eventRepository = new Mock<IEventRepository>();
        var personRepository = new Mock<IPersonRepository>();

        var eventCommand = new EventCommand("Haxx", "Internal conference", new DateTime(2023, 11, 10),
            new DateTime(2023, 11, 11), "Mallorca");

        var eventService = new EventService(eventRepository.Object, personRepository.Object);

        //Act
        var exception = Record.Exception(() => eventService.AddEvent(eventCommand));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public void AddEvent_With_A_Name_That_Already_Exists_Should_Throw_EventAlreadyExistsException()
    {
        //Arrange
        var eventRepository = new Mock<IEventRepository>();
        var events = new List<Event>()
        {
            new Event("Haxx", "Internal conference", new DateTime(2023, 11, 10), new DateTime(2023, 11, 11),
                "Mallorca")
        };
        eventRepository.Setup(e => e.GetAllEvents()).Returns(events);

        var personRepository = new Mock<IPersonRepository>();

        var eventCommand = new EventCommand("Haxx", "Internal conference", new DateTime(2023, 11, 10),
            new DateTime(2023, 11, 11), "Mallorca");

        var eventService = new EventService(eventRepository.Object, personRepository.Object);

        //Act & Assert
        Assert.Throws<EventAlreadyExistsException>(() => eventService.AddEvent(eventCommand));

    }

    [Fact]
    public void
        AddEvent_With_A_StartDateTime_After_The_EndDateTime_Should_Throw_EndDateTimeBeforeStartDateTimeException()
    {
        //Arrange
        var eventRepository = new Mock<IEventRepository>();

        var personRepository = new Mock<IPersonRepository>();

        var eventCommand = new EventCommand("Haxx", "Internal conference", new DateTime(2023, 11, 14),
            new DateTime(2023, 11, 11), "Mallorca");

        var eventService = new EventService(eventRepository.Object, personRepository.Object);

        //Act & Assert
        Assert.Throws<EndDateTimeBeforeStartDateTimeException>(() => eventService.AddEvent(eventCommand));
    }
}