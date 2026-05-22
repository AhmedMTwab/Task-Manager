using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Mappings;
using FluentValidation;

namespace TaskManager.Application.Interfaces;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(MappingProfile).Assembly);
        services.AddSingleton(config);
        services.AddSingleton<IMapper>(new Mapper(config));

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(MappingProfile).Assembly);
        });

        services.AddValidatorsFromAssembly(typeof(MappingProfile).Assembly);

        return services;
    }
}