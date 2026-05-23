namespace TaskManager.Domain.Interfaces;

public interface IProjectRepository
{
    Task AddAsync(Project project, CancellationToken cancellationToken = default);
    Task<IEnumerable<Project>> GetAllByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default);
    Task<Project?> GetByIdAsync(Guid id, Guid ownerId, CancellationToken cancellationToken = default);
    Task<Project?> GetByIdWithTasksAsync(Guid id, Guid ownerId, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
