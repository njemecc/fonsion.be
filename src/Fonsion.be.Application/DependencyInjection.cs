using Fonsion.be.Application.Common.Interfaces;
using Fonsion.be.Application.Common.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fonsion.be.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));
        });

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IReservationService,ReservationService>();

        return services;
    }
}