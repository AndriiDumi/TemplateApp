using TemplateApp.Application.Services.Implementations;
using TemplateApp.Domain.Interfaces;

namespace Template.API.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITemplateService, TemplateService>();

        return services;
    }
}
