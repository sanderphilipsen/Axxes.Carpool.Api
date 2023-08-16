using Axxes.Carpool.Contracts.Models;

namespace Axxes.Carpool.Contracts.Models;

public class Event
{
    public Guid Id;
    public string Name;
    public string Description;
    public DateTime StartDateTime;
    public DateTime EnDateTime;
    public string Location;
    public List<EventRegistration> Registrations;

    public Event(string name, string description, DateTime startDateTime, DateTime enDateTime, string location)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        StartDateTime = startDateTime;
        EnDateTime = enDateTime;
        Location = location;
        Registrations = new List<EventRegistration>();
    }

    public void Register(EventRegistration registration)
        => Registrations.Add(registration);

}