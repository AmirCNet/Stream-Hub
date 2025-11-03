using StreamHub.Models;
using StreamHub.Context;
using StreamHub.Interfaces;

namespace StreamHub.Services
{
    public class ContenidoDbService : IContenidoService
    {
        private readonly StreamHubDbContext _context;

        public ContenidoDbService(StreamHubDbContext context)
        {
            _context = context;
        }

        public List<Contenido> GetAll()
        {
            return _context.Content.ToList();
        }

        public Contenido? GetById(int id)
        {
            return _context.Content.Find(id);
        }

        public Contenido Add(Contenido contenido)
        {
            _context.Content.Add(contenido);
            _context.SaveChanges();
            return contenido;
        }

        public Contenido? Update(int id, Contenido contenido)
        {
            var existing = _context.Content.Find(id);
            if (existing == null) return null;

            existing.Titulo = contenido.Titulo;
            existing.Descripcion = contenido.Descripcion;
            existing.Genero = contenido.Genero;
            //existing.Duracion = contenido.Duracion;
            _context.SaveChanges();

            return existing;
        }

        public bool Delete(int id)
        {
            var contenido = _context.Content.Find(id);
            if (contenido == null) return false;

            _context.Content.Remove(contenido);
            _context.SaveChanges();
            return true;
        }
    }
}