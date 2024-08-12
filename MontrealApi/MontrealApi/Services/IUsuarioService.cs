using MontrealApi.Models;

namespace MontrealApi.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> PegarTodosAsync();
        Task<Usuario> PegarPorIdAsync(int id);
        Task AdicionarAsync(Usuario usuario);
        Task AtualizarAsync(Usuario usuario);
        Task DeletarAsync(int id);
    }
}
