﻿using Fonsion.be.Application.Common.Interfaces;
using Fonsion.be.Domain.Entities;
using Fonsion.be.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fonsion.be.Infrastructure.Common.Persistence;

public class FonsionDbContext: IdentityDbContext<User,IdentityRole,string>, IUnitOfWork
{

    public FonsionDbContext(DbContextOptions options): base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new IdentityRole
            {
                Name = "Guest",
                NormalizedName = "GUEST"
            },
        };

        builder.Entity<IdentityRole>().HasData(roles);
    }

    public DbSet<Room> Rooms { get; set; } = null!;
    
    
    public async Task CommitChangesAsync()
    {
       await base.SaveChangesAsync();
    }
}