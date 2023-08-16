using Axxes.Carpool.Api.Exceptions;
using Axxes.Carpool.Api.Repositories.Abstractions;
using Axxes.Carpool.Api.Services;
using Axxes.Carpool.Contracts;
using Axxes.Carpool.Contracts.Models;
using Moq;

namespace Axxes.Carpool.UnitTests.ServiceTests;

public sealed class EventServiceTests
{
    [Fact]
    public void Add_Event_Should_Succeed_If_There_Are_0_Upcoming_Events_And_EndTime_Is_After_StartTime()
    {
        // Arrange
        var eventRepository = new Mock<IEventRepository>();
        eventRepository.Setup(p => p.GetAllUpcomingEvents()).Returns(new List<Event>());

        var personRepository = new Mock<IPersonRepository>();
        personRepository.Setup(p => p.GetAllPersons()).Returns(new List<Person>());

        var eventService = new EventService(eventRepository.Object, personRepository.Object);

        var eventCommand = new EventCommand("Haxx", "Internal conference", DateTime.Now.AddDays(5).AddHours(2),
            DateTime.Now.AddDays(5).AddHours(5), "Grimbergen");

        //Act
        var exception = Record.Exception(() => eventService.AddEvent(eventCommand));

        //Assert
        Assert.Null(exception);


    }

    [Fact]
    public void Add_Event_Should_Return_1_Error_If_There_Are_Is_Already_A_Event_With_The_Same_Name()
    {
        // Arrange
        var eventCommand = new EventCommand("Haxx", "Internal conference", DateTime.Now.AddDays(5).AddHours(2),
            DateTime.Now.AddDays(5).AddHours(5), "Grimbergen");
        var axxesEvent = new Event("Haxx", "Internal conference", DateTime.Now.AddDays(5).AddHours(2),
            DateTime.Now.AddDays(5).AddHours(5), "Grimbergen");

        var eventRepository = new Mock<IEventRepository>();
        eventRepository.Setup(p => p.GetAllUpcomingEvents()).Returns(new List<Event>() { axxesEvent });

        var personRepository = new Mock<IPersonRepository>();
        personRepository.Setup(p => p.GetAllPersons()).Returns(new List<Person>());

        var eventService = new EventService(eventRepository.Object, personRepository.Object);

        //Act
        eventService.AddEvent(eventCommand);

        //Assert
        Assert.Throws<EventAlreadyExistsException>(() => eventService.AddEvent(eventCommand));

    }

}