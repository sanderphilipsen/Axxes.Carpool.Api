using Axxes.Carpool.Contracts.Models;

namespace Axxes.Carpool.Api.Repositories.Abstractions;

public interface IPersonRepository
{
    public IEnumerable<Person> GetAllPersons();
}