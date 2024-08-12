using MontrealApi.Models;

namespace MontrealApi.Repositories
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> PegarTodosAsync();
        Task<Usuario> PegarPorIdAsync(int id);
        Task AdicionarAsync(Usuario usuario);
        Task AtualizarAsync(Usuario usuario);
        Task DeletarAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<Usuario> PegarPorNomeUsuarioAsync(string nomeUsuario);
    }
}
