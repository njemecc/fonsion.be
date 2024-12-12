
using Fonsion.be.Application.Common.Interfaces;
using Fonsion.be.Domain.Reservations;
using Fonsion.be.Infrastructure.Common.Persistence;
using Fonsion.be.Infrastructure.GuestCompanions.Persistance;
using Fonsion.be.Infrastructure.Images.Persistance;
using Fonsion.be.Infrastructure.PromoCodes.Persistance;
using Fonsion.be.Infrastructure.Reservations.Persistence;
using Fonsion.be.Infrastructure.Rooms.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fonsion.be.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,string connectionString)
    {
        
        services.AddDbContext<FonsionDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<FonsionDbContext>());
        
        services.AddScoped<IGuestCompanionRepository, GuestCompanionRepository>();
        services.AddScoped<IPromoCodeRepository, PromoCodeRepository>();
        services.AddScoped<IReservationsRepository, ReservationsRepository>();
        services.AddScoped<IRoomsRepository,RoomsRepository>();
        services.AddScoped<IImagesRepository, ImagesRepository>();
        

        return services;
    }
}