using ClaseEF.Models;
using Microsoft.EntityFrameworkCore;

namespace ClaseEF.Data;

public partial class ClaseEfContext : DbContext
{
    public ClaseEfContext(DbContextOptions<ClaseEfContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CITA> CITAs { get; set; }

    public virtual DbSet<Clinica> Clinicas { get; set; }

    public virtual DbSet<Especialidade> Especialidades { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CITA>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.CITAS");

            entity.ToTable("CITAS");


            entity.Property(e => e.FechaDeLaCita).HasColumnType("datetime");
            entity.Property(e => e.FechaDeRegistro).HasColumnType("datetime");
            entity.Property(e => e.MontoTotal).HasColumnType("decimal(18, 2)");

        });

        modelBuilder.Entity<Clinica>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clinicas__3214EC078883FBD7");

            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Especialidade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Especial__3214EC0716766937");

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Servicios");

            entity.Property(e => e.Clinica).HasMaxLength(200);
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.Especialista).HasMaxLength(200);
            entity.Property(e => e.FechaDeModificacion).HasColumnType("datetime");
            entity.Property(e => e.FechaDeRegistro).HasColumnType("datetime");
            entity.Property(e => e.IVA).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Monto).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });
        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Pacientes");

            entity.Property(e => e.NombreDeLaPersona).HasMaxLength(150);
            entity.Property(e => e.Identificacion).HasMaxLength(30);
            entity.Property(e => e.Telefono).HasMaxLength(10);
            entity.Property(e => e.Correo).HasMaxLength(50);
            entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");
            entity.Property(e => e.Direccion).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
