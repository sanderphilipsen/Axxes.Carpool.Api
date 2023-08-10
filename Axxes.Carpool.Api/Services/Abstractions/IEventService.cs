using Axxes.Carpool.Contracts;
using Axxes.Carpool.Contracts.Models;

namespace Axxes.Carpool.Api.Services.Abstractions;

public interface IEventService
{
    public void AddEvent(EventCommand eventCommand);

    public void RegisterForEvent(RegisterCommand registerCommand);

    public IEnumerable<Event> GetAllEvents();
}