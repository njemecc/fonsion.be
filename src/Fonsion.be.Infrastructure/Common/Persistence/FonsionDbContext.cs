using Fonsion.be.Application.Common.Interfaces;
using Fonsion.be.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fonsion.be.Infrastructure.Common.Persistence;

public class FonsionDbContext: DbContext, IUnitOfWork
{

    public FonsionDbContext(DbContextOptions options): base(options)
    {
        
    }
    
    public DbSet<Room> Rooms { get; set; } = null!;
    
    
    public async Task CommitChangesAsync()
    {
       await base.SaveChangesAsync();
    }
}