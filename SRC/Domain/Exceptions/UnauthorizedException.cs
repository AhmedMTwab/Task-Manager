namespace TaskManager.Domain.Exceptions;


public class UnauthorizedException : Exception
{
    public UnauthorizedException() : base("You are not authenticated.") { }
    public UnauthorizedException(string message) : base(message) { }
    public UnauthorizedException(string message, Exception innerException) : base(message, innerException) { }
}
