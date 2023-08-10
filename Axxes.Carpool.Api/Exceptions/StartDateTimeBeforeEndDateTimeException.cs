namespace Axxes.Carpool.Api.Exceptions;

public class StartDateTimeBeforeEndDateTimeException : Exception
{
    public StartDateTimeBeforeEndDateTimeException(string message) : base(message)
    {
    }
}