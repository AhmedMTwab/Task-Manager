namespace TaskManager.Application.DTOs.Authentication;

public class TokenDTO
{
    public string Token { get; set; } = string.Empty;
    public DateTime Expire_Date { get; set; }
}
