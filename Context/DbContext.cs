using Microsoft.EntityFrameworkCore;
using StreamHub.Models;

namespace StreamHub.Context;

public class StreamHubDbContext : DbContext
{
    public StreamHubDbContext(DbContextOptions<StreamHubDbContext> options): base(options)
    {
        
    }

    public DbSet<Contenido> Content { get; set; }
    public DbSet<Usuario> Users { get; set; }
    public DbSet<Suscripcion> Subscriptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // Configuración 1:1 Usuario <-> Suscripcion
        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Subscription)
            .WithOne(s => s.Usuario)
            .HasForeignKey<Suscripcion>(s => s.UsuarioId);
        
        modelBuilder.Entity<Contenido>().HasData(
            new Contenido { Id = 1, Titulo = "The Matrix", Descripcion = "A classic sci-fi film.", Tipo = "Movie", Genero = "Sci-Fi", ClasificacionEdad = "13+", Url = "https://streamhub.com/play/matrix-stream-url" },
            new Contenido { Id = 2, Titulo = "Stranger Things S1", Descripcion = "Kids battling supernatural forces.", Tipo = "Series", Genero = "Fantasy", ClasificacionEdad = "16+", Url = "https://streamhub.com/play/stranger-things-s1-stream-url" }
        );//
        
        base.OnModelCreating(modelBuilder);
    }
}