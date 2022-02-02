using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Model> Models { get; set; }

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //if (!optionsBuilder.IsConfigured)
        //    base.OnConfiguring(
        //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("RentACarConnectionString")));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(
            b =>
            {
                b.ToTable("Brands").HasKey(k => k.Id);
                b.Property(p => p.Id).HasColumnName("Id");
                b.Property(p => p.Name).HasColumnName("Name");
                b.HasMany(p => p.Models);
            });
        modelBuilder.Entity<Model>(b =>
        {
            b.ToTable("Models").HasKey(k => k.Id);
            b.Property(p => p.Id).HasColumnName("Id");
            b.Property(p => p.Name).HasColumnName("Name");
            b.Property(p => p.DailyPrice).HasColumnName("DailyPrice");
            b.Property(p => p.BrandId).HasColumnName("BrandId");
            b.Property(p => p.TransmissionId).HasColumnName("TransmissionId");
            b.Property(p => p.FuelId).HasColumnName("FuelId");
            b.Property(p => p.ImageUrl).HasColumnName("ImageUrl");
            b.HasOne(p => p.Brand);
            b.HasOne(p => p.Transmission);
            b.HasOne(p => p.Fuel);
            b.HasMany(p => p.Cars);
        });
    }
}