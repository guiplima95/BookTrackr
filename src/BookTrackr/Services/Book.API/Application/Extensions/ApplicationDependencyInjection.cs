using FluentValidation;

namespace Book.API.Application.Extensions;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(ApplicationDependencyInjection).Assembly);
        });

        services.AddValidatorsFromAssembly(
           typeof(ApplicationDependencyInjection).Assembly,
           includeInternalTypes: true);

        return services;
    }
}