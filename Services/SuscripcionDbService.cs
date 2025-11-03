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

            existing.TipoSuscripcion = suscripcion.TipoSuscripcion;
            //existing.Precio = suscripcion.Precio;??? --Consultar con pasarela de pagos
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