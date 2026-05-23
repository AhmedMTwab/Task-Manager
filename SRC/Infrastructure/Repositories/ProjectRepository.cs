using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Data;

namespace TaskManager.Infrastructure.Repositories;

public class ProjectRepository(ApplicationDbContext dbContext) : IProjectRepository
{
    public async Task AddAsync(Project project, CancellationToken cancellationToken = default)
    {
        await dbContext.projects.AddAsync(project, cancellationToken);
    }

    public async Task<IEnumerable<Project>> GetAllByOwnerIdAsync(Guid ownerId, CancellationToken cancellationToken = default)
    {
        return await dbContext.projects
            .Where(p => p.OwnerId == ownerId && !p.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<Project?> GetByIdAsync(Guid id, Guid ownerId, CancellationToken cancellationToken = default)
    {
        return await dbContext.projects
            .FirstOrDefaultAsync(p => p.Id == id && p.OwnerId == ownerId && !p.IsDeleted, cancellationToken);
    }

    public async Task<Project?> GetByIdWithTasksAsync(Guid id, Guid ownerId, CancellationToken cancellationToken = default)
    {
        return await dbContext.projects
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(p => p.Id == id && p.OwnerId == ownerId && !p.IsDeleted, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
