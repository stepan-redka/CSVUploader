using Microsoft.EntityFrameworkCore;
using TestApp.Core.Entities;

namespace TestApp.Infrastructure.Data;

public class DbAppContext : DbContext
{
    public DbAppContext(DbContextOptions<DbAppContext> options) : base(options)
    {
    }

    public DbSet<Contact> Contacts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.ToTable("Contacts");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Salary).HasColumnType("decimal(18,2)");
        });
    }
}