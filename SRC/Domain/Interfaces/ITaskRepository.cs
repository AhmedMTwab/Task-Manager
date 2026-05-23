namespace TaskManager.Domain.Interfaces;

public interface ITaskRepository
{
    Task AddAsync(ProjectTask task, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
