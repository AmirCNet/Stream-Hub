using Microsoft.EntityFrameworkCore;
using StreamHub.Models;

namespace StreamHub.Data;

public class StreamHubDbContext : DbContext
{
    // Constructor para configurar la BD
    public StreamHubDbContext(DbContextOptions<StreamHubDbContext> options): base(options)
    {
        // Configuración adicional si es necesaria
    }

    // Colecciones que se mapearán a tablas en la base de datos SQL
    public DbSet<Contenido> Content { get; set; }
    public DbSet<Usuario> Users { get; set; }
    public DbSet<Suscripcion> Subscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 1. Configuración de Relaciones
        
        // Un Usuario tiene Cero o Una Suscripción (Relación 1 a 1 opcional)
        // Configurar la relación desde el lado de Suscripcion para evitar depender
        // de una propiedad de navegación opcional en Usuario.
        modelBuilder.Entity<Suscripcion>()
            .HasOne(s => s.User)
            .WithOne()
            .HasForeignKey<Suscripcion>("UserId");

        // 2. Data Inicial (Opcional: para probar el catálogo)
        modelBuilder.Entity<Contenido>().HasData(
            new Contenido { Id = 1, Titulo = "The Matrix", Descripcion = "A classic sci-fi film.", Tipo = "Movie", Genero = "Sci-Fi", ClasificacionEdad = "13+", Url = "https://streamhub.com/play/matrix-stream-url" },
            new Contenido { Id = 2, Titulo = "Stranger Things S1", Descripcion = "Kids battling supernatural forces.", Tipo = "Series", Genero = "Fantasy", ClasificacionEdad = "16+", Url = "https://streamhub.com/play/stranger-things-s1-stream-url" }
        );
        
        base.OnModelCreating(modelBuilder);
    }
}
