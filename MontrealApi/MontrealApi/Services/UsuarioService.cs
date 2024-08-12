using MontrealApi.Models;
using MontrealApi.Repositories;

namespace MontrealApi.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repositorioDeUsuarios;

        public UsuarioService(IUsuarioRepository repositorioDeUsuarios)
        {
            _repositorioDeUsuarios = repositorioDeUsuarios;
        }

        public async Task<IEnumerable<Usuario>> PegarTodosAsync()
        {
            return await _repositorioDeUsuarios.PegarTodosAsync();
        }

        public async Task<Usuario> PegarPorIdAsync(int id)
        {
            return await _repositorioDeUsuarios.PegarPorIdAsync(id);
        }

        public async Task AdicionarAsync(Usuario usuario)
        {
            await _repositorioDeUsuarios.AdicionarAsync(usuario);
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            await _repositorioDeUsuarios.AtualizarAsync(usuario);
        }

        public async Task DeletarAsync(int id)
        {
            await _repositorioDeUsuarios.DeletarAsync(id);
        }
    }
}
