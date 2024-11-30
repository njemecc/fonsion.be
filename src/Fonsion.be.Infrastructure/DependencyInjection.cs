using System.Collections.Immutable;
using Fonsion.be.Application.Common.Interfaces;
using Fonsion.be.Infrastructure.Common.Persistence;
using Fonsion.be.Infrastructure.Rooms.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fonsion.be.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,string connectionString)
    {
        
        services.AddDbContext<FonsionDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        services.AddScoped<IRoomsRepository,RoomsRepository>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<FonsionDbContext>());
        

        return services;
    }
}