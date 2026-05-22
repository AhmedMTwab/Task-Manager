namespace TaskManager.Domain.Exceptions;

public class BadRequestException : Exception
{
    public Dictionary<string, List<string>>? ValidationErrors { get; }

    public BadRequestException() : base("The request was invalid.") { }
    public BadRequestException(string message) : base(message) { }
    public BadRequestException(string message, Exception innerException) : base(message, innerException) { }

    public BadRequestException(string message, Dictionary<string, List<string>> validationErrors)
        : base(message)
    {
        ValidationErrors = validationErrors;
    }
}
