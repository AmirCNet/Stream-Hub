using System.Collections.Generic;
using System.Threading.Tasks;
using StreamHub.Models;

namespace StreamHub.Interfaces
{
    public interface ISuscripcionService
    {
        List<Suscripcion> GetAll();
        Suscripcion? GetById(int id);
        Suscripcion? GetByUserId(int userId);
        Suscripcion Add(Suscripcion suscripcion);
        Suscripcion? Update(int id, Suscripcion suscripcion);
        Suscripcion UpsertForUser(int userId, Suscripcion data);
        bool Delete(int id);
    }
}