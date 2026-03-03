using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaVales.Models;

namespace SistemaVales.DAL;

public class ValesDbContext : IdentityDbContext
{
    public ValesDbContext(DbContextOptions<ValesDbContext> options) : base(options)
    {
    }

    public DbSet<Hospital> Hospitales { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Expediente> Expedientes { get; set; }
    public DbSet<Medicamento> Medicamentos { get; set; }
    public DbSet<Receta> Recetas { get; set; }
    public DbSet<Vale> Vales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Hospital>()
            .HasMany(h => h.Pacientes)
            .WithOne(p => p.Hospital)
            .HasForeignKey(p => p.HospitalId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Receta>()
            .HasOne(r => r.Paciente)
            .WithMany()
            .HasForeignKey(r => r.PacienteId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Receta>()
            .HasOne(r => r.Hospital)
            .WithMany()
            .HasForeignKey(r => r.HospitalId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Expediente>()
            .HasOne(e => e.Vale)
            .WithOne(v => v.Expediente)
            .HasForeignKey<Vale>(v => v.ExpedienteId);
    }
}
