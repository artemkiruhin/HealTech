using HealTech.Application.HashServices;
using HealTech.Application.HashServices.Base;
using Microsoft.Extensions.DependencyInjection;

namespace HealTech.Application.Mappers.AutoMapperService;

public static class AutoMapperExtensions
{
    public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
    {
        services.AddScoped<IHashService, Sha256HashService>();
        services.AddAutoMapper(typeof(MappingProfile));
        return services;
    }
}