using System.Collections.Generic;
using System.Threading.Tasks;
using StreamHub.Models;

namespace StreamHub.Interfaces
{
    public interface IContenidoService
    {
        List<Contenido> GetAll();
        Contenido? GetById(int id);
        Contenido Add(Contenido contenido);
        Contenido? Update(int id, Contenido contenido);
        bool Delete(int id);
    }
}