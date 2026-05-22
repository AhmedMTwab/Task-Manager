using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Behaviors;
using TaskManager.Application.Mappings;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

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
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(MappingProfile).Assembly);

        return services;
    }
}