using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; 
using server.Models;

namespace server.Data;

public class AppDbContext : IdentityDbContext<User>
{
    public DbSet<Category> Categories { get; set; } 
    public DbSet<Transaction> Transactions { get; set; }
    // public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("public"); 
    }
}
