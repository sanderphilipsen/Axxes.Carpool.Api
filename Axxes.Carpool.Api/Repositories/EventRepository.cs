using Axxes.Carpool.Api.Repositories.Abstractions;
using Axxes.Carpool.Contracts.Models;

namespace Axxes.Carpool.Api.Repositories;

public class EventRepository : IEventRepository
{
    private List<Event> Events = new List<Event>()
    {
        new Event("Techorama","International .NET Conference",DateTime.Now.AddDays(10).AddHours(8),DateTime.Now.AddDays(11).AddHours(20),"Kinepolis" ),
        new Event("Haxx","Internal Axxes conference",DateTime.Now.AddDays(50).AddHours(8),DateTime.Now.AddDays(50).AddHours(23),"Grimbergen"),
    };

    public EventRepository()
    {
    }

    public void AddEvent(Event axxesEvent)
        => Events.Add(axxesEvent);

    public IEnumerable<Event> GetAllEvents()
        => Events;

    public IEnumerable<Event> GetAllUpcomingEvents()
        => Events.Where(e => e.StartDateTime > DateTime.Now);

}