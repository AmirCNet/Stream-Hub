using System.Collections.Generic;
using System.Threading.Tasks;
using StreamHub.Models;

namespace StreamHub.Interfaces
{
    public interface ISuscripcionService
    {
        List<Suscripcion> GetAll();
        Suscripcion? GetById(int id);
        Suscripcion Add(Suscripcion suscripcion);
        Suscripcion? Update(int id, Suscripcion suscripcion);
        bool Delete(int id);
    }
}