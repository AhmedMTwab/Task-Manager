namespace TaskManager.Domain.Interfaces;

public interface ITaskRepository
{
    Task AddAsync(ProjectTask task, CancellationToken cancellationToken = default);
    Task<IEnumerable<ProjectTask>> GetAllByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
