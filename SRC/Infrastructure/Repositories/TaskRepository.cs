using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repositories;

public class TaskRepository(ApplicationDbContext dbContext) : ITaskRepository
{
    public async Task AddAsync(ProjectTask task, CancellationToken cancellationToken = default)
    {
        await dbContext.tasks.AddAsync(task, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
