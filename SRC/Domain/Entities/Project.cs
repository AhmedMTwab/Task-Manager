
using Microsoft.EntityFrameworkCore;

public class Project
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Guid OwnerId { get; set; }

    public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    public ApplicationUser Owner { get; set; }
}
public static class ProjectExtensions
{
    public static void ConfigureProject(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .HasMany(p => p.Tasks)
            .WithOne(t => t.Project)
            .HasForeignKey(t => t.ProjectId);

        modelBuilder.Entity<Project>()
            .HasOne(p => p.Owner)
            .WithMany()
            .HasForeignKey(p => p.OwnerId);
    }
}