using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using MongoDB_EF_Driver_Sample.Models;

namespace MongoDB_EF_Driver_Sample.Data;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>(b => 
        {
            b.ToCollection("customers");
            b.Property(c=>c.Name).HasElementName("primaryName");
        });

    }

    

    public DbSet<Customer> Customers { get; init; }
}