namespace TaskManager.Application.DTOs.Task;

public class CreateTaskDTO
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public PriorityEnum Priority { get; set; } = PriorityEnum.Medium;
    public StatusEnum Status { get; set; } = StatusEnum.Pending;
    public DateTime? DueDate { get; set; }
}
