using Microsoft.EntityFrameworkCore;
using Persistencia.Models;

namespace Persistencia.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<EstudianteDbModel> Estudiantes { get; set; }
    public DbSet<ProfesorDbModel> Profesor { get; set; }
    public DbSet<NotaDbModel> Nota { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EstudianteDbModel>(entity =>
        {
            entity.ToTable("Estudiante");

            entity.HasKey(e => e.id);

            entity.Property(e => e.nombre)
                  .HasColumnName("nombre");
        });

        modelBuilder.Entity<ProfesorDbModel>(entity =>
        {
            entity.ToTable("Profesor");

            entity.HasKey(e => e.id);

            entity.Property(e => e.nombre)
                  .HasColumnName("nombre");
        });

        modelBuilder.Entity<NotaDbModel>(entity =>
        {
            entity.ToTable("Nota");

            entity.HasKey(e => e.id);

            entity.Property(e => e.nombre)
                  .HasColumnName("nombre");

            entity.Property(e => e.idProfesor)
                  .HasColumnName("idProfesor");

            entity.Property(e => e.idEstudiante)
                  .HasColumnName("idEstudiante");

            entity.Property(e => e.valor)
                  .HasColumnName("valor")
                  .HasPrecision(5, 2);
        });
    }
}
