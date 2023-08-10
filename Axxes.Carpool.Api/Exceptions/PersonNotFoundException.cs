namespace Axxes.Carpool.Api.Exceptions;

public class PersonNotFoundException : Exception
{
    public PersonNotFoundException(string message) : base(message)
    {
    }
}