namespace Axxes.Carpool.Api.Exceptions;

public class EndDateTimeBeforeStartDateTimeException : Exception
{
    public EndDateTimeBeforeStartDateTimeException(string message) : base(message)
    {
    }
}