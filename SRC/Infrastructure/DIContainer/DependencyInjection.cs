using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Interfaces;
using TaskManager.Infrastructure.Data;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
           {
               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
           });

        services.AddScoped<IProjectRepository, ProjectRepository>();

        return services;
    }
}