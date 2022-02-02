using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Fuel> Fuel { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Transmission> Transmissions { get; set; }

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
        modelBuilder.Entity<Brand>(b =>
        {
            b.ToTable("Brands").HasKey(k => k.Id);
            b.Property(p => p.Id).HasColumnName("Id");
            b.Property(p => p.Name).HasColumnName("Name");
            b.HasMany(p => p.Models);
        });

        modelBuilder.Entity<Car>(c =>
        {
            c.ToTable("Cars").HasKey(k => k.Id);
            c.Property(p => p.Id).HasColumnName("Id");
            c.Property(p => p.ColorId).HasColumnName("ColorId");
            c.Property(p => p.ModelId).HasColumnName("ModelId");
            c.Property(p => p.CarState).HasColumnName("State");
            c.Property(p => p.ModelYear).HasColumnName("ModelYear");
            c.Property(p => p.Plate).HasColumnName("Plate");
            c.HasOne(p => p.Color);
            c.HasOne(p => p.Model);
        });

        modelBuilder.Entity<Color>(c =>
        {
            c.ToTable("Colors").HasKey(k => k.Id);
            c.Property(p => p.Id).HasColumnName("Id");
            c.Property(p => p.Name).HasColumnName("Name");
            c.HasMany(p => p.Cars);
        });
        modelBuilder.Entity<Fuel>(f =>
        {
            f.ToTable("Fuels").HasKey(k => k.Id);
            f.Property(p => p.Id).HasColumnName("Id");
            f.Property(p => p.Name).HasColumnName("Name");
            f.HasMany(p => p.Models);
        });
        modelBuilder.Entity<Model>(m =>
        {
            m.ToTable("Models").HasKey(k => k.Id);
            m.Property(p => p.Id).HasColumnName("Id");
            m.Property(p => p.BrandId).HasColumnName("BrandId");
            m.Property(p => p.FuelId).HasColumnName("FuelId");
            m.Property(p => p.TransmissionId).HasColumnName("TransmissionId");
            m.Property(p => p.Name).HasColumnName("Name");
            m.Property(p => p.DailyPrice).HasColumnName("DailyPrice");
            m.Property(p => p.ImageUrl).HasColumnName("ImageUrl");
            m.HasOne(p => p.Brand);
            m.HasMany(p => p.Cars);
            m.HasOne(p => p.Fuel);
            m.HasOne(p => p.Transmission);
        });
        modelBuilder.Entity<Transmission>(t =>
        {
            t.ToTable("Transmissions").HasKey(k => k.Id);
            t.Property(p => p.Id).HasColumnName("Id");
            t.Property(p => p.Name).HasColumnName("Name");
            t.HasMany(p => p.Models);
        });
    }
}