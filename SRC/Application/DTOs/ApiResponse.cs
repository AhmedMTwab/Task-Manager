namespace TaskManager.Application.DTOs;

public class ApiResponse
{
    public int StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    public Dictionary<string, List<string>>? Errors { get; set; }

    public static ApiResponse Success(int statusCode = 200, string message = "Request completed successfully.")
        => new()
        {
            StatusCode = statusCode,
            IsSuccess = true,
            Message = message
        };

    public static ApiResponse Fail(int statusCode, string message, Dictionary<string, List<string>>? errors = null)
        => new()
        {
            StatusCode = statusCode,
            IsSuccess = false,
            Message = message,
            Errors = errors
        };
}

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; set; }

    public static ApiResponse<T> Success(T data, int statusCode = 200, string message = "Request completed successfully.")
        => new()
        {
            StatusCode = statusCode,
            IsSuccess = true,
            Message = message,
            Data = data
        };

    public new static ApiResponse<T> Fail(int statusCode, string message, Dictionary<string, List<string>>? errors = null)
        => new()
        {
            StatusCode = statusCode,
            IsSuccess = false,
            Message = message,
            Errors = errors
        };
}
