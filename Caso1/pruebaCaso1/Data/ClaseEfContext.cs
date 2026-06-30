using pruebaCaso1.Models;
using Microsoft.EntityFrameworkCore;

namespace pruebaCaso1.Data
{
    public partial class ClaseEfContext : DbContext
    {
        public ClaseEfContext(DbContextOptions<ClaseEfContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Habitacion> Habitaciones { get; set; }
        public virtual DbSet<Reservacion> Reservaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Habitacion>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Habitaciones");
                entity.ToTable("Habitaciones");

                entity.Property(e => e.CodigoDeHabitacion).HasMaxLength(7).IsRequired();
                entity.Property(e => e.NombreDeHabitacion).HasMaxLength(30).IsRequired();
                entity.Property(e => e.Ubicacion).HasMaxLength(10).IsRequired();
                entity.Property(e => e.EncargadoDeLimpieza).HasMaxLength(100).IsRequired();
                entity.Property(e => e.CostoDeLimpieza).HasColumnType("decimal(18, 2)").IsRequired();
                entity.Property(e => e.CostoDeReserva).HasColumnType("decimal(18, 2)").IsRequired();
                entity.Property(e => e.FechaDeRegistro).HasColumnType("datetime").IsRequired();
                entity.Property(e => e.FechaDeModificacion).HasColumnType("datetime").IsRequired(false);
                entity.Property(e => e.Estado);
            });

            modelBuilder.Entity<Reservacion>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Reservaciones");
                entity.ToTable("Reservaciones");

                entity.Property(e => e.NombreDeLaPersona).HasMaxLength(150).IsRequired();
                entity.Property(e => e.Identificacion).HasMaxLength(30).IsRequired();
                entity.Property(e => e.Telefono).HasMaxLength(10).IsRequired();
                entity.Property(e => e.Correo).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Direccion).HasMaxLength(200).IsRequired();
                entity.Property(e => e.MontoTotal).HasColumnType("decimal(18, 2)").IsRequired();
                entity.Property(e => e.FechaNacimiento).HasColumnType("datetime").IsRequired();
                entity.Property(e => e.FechaInicioReserva).HasColumnType("datetime").IsRequired();
                entity.Property(e => e.FechaFinReserva).HasColumnType("datetime").IsRequired();
                entity.Property(e => e.FechaDeRegistro).HasColumnType("datetime").IsRequired();
                entity.Property(e => e.IdHabitacion).IsRequired();

                
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}