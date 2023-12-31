﻿using Axxes.Carpool.Contracts;
using Axxes.Carpool.Contracts.Models;

namespace Axxes.Carpool.Api.Repositories.Abstractions;

public interface IEventRepository
{

    public void AddEvent(Event axxesEvent);

    public IEnumerable<Event> GetAllUpcomingEvents();

    public IEnumerable<Event> GetAllEvents();
}