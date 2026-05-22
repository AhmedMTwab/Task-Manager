using Microsoft.EntityFrameworkCore;

public class Task
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public PriorityEnum Priority { get; set; } = PriorityEnum.Medium;
    public StatusEnum Status { get; set; } = StatusEnum.Pending;
    public DateTime? DueDate { get; set; }
    public Guid ProjectId { get; set; }

    public Project Project { get; set; } = new Project();

}
public static class TaskExtensions
{
    public static void ConfigureTask(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Task>()
            .HasOne(t => t.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId);
    }
}