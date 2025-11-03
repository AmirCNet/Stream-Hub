using StreamHub.Models;

namespace StreamHub.Interfaces
{
    public interface IUsuarioService
    {
        List<Usuario> GetAll();
        Usuario? GetById(int id);
        Usuario Add(Usuario usuario);
        Usuario? Update(int id, Usuario usuario);
        bool Delete(int id);
    }
}