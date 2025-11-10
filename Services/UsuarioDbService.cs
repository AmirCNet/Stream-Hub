using StreamHub.Models;
using StreamHub.Context;
using StreamHub.Interfaces;

namespace StreamHub.Services
{
    public class UsuarioDbService : IUsuarioService
    {
        private readonly StreamHubDbContext _context;

        public UsuarioDbService(StreamHubDbContext context)
        {
            _context = context;
        }

        public List<Usuario> GetAll()
        {
            return _context.Users.ToList();
        }

        public Usuario? GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public Usuario Add(Usuario usuario)
        {
            usuario.FechaRegistro = DateTime.Now;
            _context.Users.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }

        public Usuario? Update(int id, Usuario usuario)
        {
            var existing = _context.Users.Find(id);
            if (existing == null) return null;

            existing.Nombre = usuario.Nombre;
            existing.Email = usuario.Email;
            existing.Contraseña = usuario.Contraseña;
            _context.SaveChanges();

            return existing;
        }

        public bool Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }
    }
}