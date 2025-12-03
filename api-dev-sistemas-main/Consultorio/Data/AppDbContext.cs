using Microsoft.EntityFrameworkCore;
using Consultorio.Models;

namespace Consultorio.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Patient> Patients { get; set; } = null!;
    public DbSet<Appointment> Appointments { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=consultorio.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Name).IsRequired().HasMaxLength(100);
            e.Property(p => p.Email).IsRequired().HasMaxLength(150);
            e.HasIndex(p => p.Email).IsUnique();
        });

        base.OnModelCreating(modelBuilder);

        // Configurar relacionamento
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
