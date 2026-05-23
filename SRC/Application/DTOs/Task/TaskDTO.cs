namespace TaskManager.Application.DTOs.Task;

public class TaskDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public PriorityEnum Priority { get; set; }
    public StatusEnum Status { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
}
