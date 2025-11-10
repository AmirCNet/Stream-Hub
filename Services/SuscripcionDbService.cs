using StreamHub.Models;
using StreamHub.Context;
using StreamHub.Interfaces;

namespace StreamHub.Services
{
    public class SuscripcionDbService : ISuscripcionService
    {
        private readonly StreamHubDbContext _context;

        public SuscripcionDbService(StreamHubDbContext context)
        {
            _context = context;
        }

        public List<Suscripcion> GetAll()
        {
            return _context.Subscriptions.ToList();
        }

        public Suscripcion? GetById(int id)
        {
            return _context.Subscriptions.Find(id);
        }

        public Suscripcion? GetByUserId(int userId)
        {
            return _context.Subscriptions.FirstOrDefault(s => s.UsuarioId == userId);
        }

        public Suscripcion Add(Suscripcion suscripcion)
        {
            suscripcion.FechaInicio = DateTime.Now;
            _context.Subscriptions.Add(suscripcion);
            _context.SaveChanges();
            return suscripcion;
        }

        public Suscripcion? Update(int id, Suscripcion suscripcion)
        {
            var existing = _context.Subscriptions.Find(id);
            if (existing == null) return null;

            existing.Plan = suscripcion.Plan;
            if (suscripcion.FechaExpiracion.HasValue)
            {
                existing.FechaExpiracion = suscripcion.FechaExpiracion;
            }
            if (!string.IsNullOrWhiteSpace(suscripcion.Estado))
            {
                existing.Estado = suscripcion.Estado;
            }
            _context.SaveChanges();

            return existing;
        }

        public Suscripcion UpsertForUser(int userId, Suscripcion data)
        {
            var existing = _context.Subscriptions.FirstOrDefault(s => s.UsuarioId == userId);
            if (existing == null)
            {
                var nueva = new Suscripcion
                {
                    UsuarioId = userId,
                    Plan = data.Plan,
                    FechaInicio = DateTime.UtcNow,
                    FechaExpiracion = data.FechaExpiracion,
                    Estado = string.IsNullOrWhiteSpace(data.Estado) ? "Activa" : data.Estado
                };
                _context.Subscriptions.Add(nueva);
                _context.SaveChanges();
                return nueva;
            }

            existing.Plan = data.Plan;
            existing.FechaExpiracion = data.FechaExpiracion;
            if (!string.IsNullOrWhiteSpace(data.Estado))
            {
                existing.Estado = data.Estado;
            }
            _context.SaveChanges();
            return existing;
        }

        public bool Delete(int id)
        {
            var suscripcion = _context.Subscriptions.Find(id);
            if (suscripcion == null) return false;

            _context.Subscriptions.Remove(suscripcion);
            _context.SaveChanges();
            return true;
        }
    }
}