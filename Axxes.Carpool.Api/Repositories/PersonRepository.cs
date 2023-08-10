using Axxes.Carpool.Api.Repositories.Abstractions;
using Axxes.Carpool.Contracts.Models;

namespace Axxes.Carpool.Api.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly List<Person> _persons = new()
    {
       new Person("John"),
       new Person("Lisa"),
       new Person("Jef"),
       new Person("Margot"),
       new Person("Lien"),
       new Person("Joris"),
    };

    public PersonRepository()
    {
    }

    public IEnumerable<Person> GetAllPersons()
        => _persons;
}