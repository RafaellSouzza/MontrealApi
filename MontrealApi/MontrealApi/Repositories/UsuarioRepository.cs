using Microsoft.EntityFrameworkCore;
using MontrealApi.Data;
using MontrealApi.Models;

namespace MontrealApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _contexto;

        public UsuarioRepository(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Usuario>> PegarTodosAsync()
        {
            return await _contexto.Usuarios.ToListAsync();
        }

        public async Task<Usuario> PegarPorIdAsync(int id)
        {
            return await _contexto.Usuarios.FindAsync(id);
        }

        public async Task AdicionarAsync(Usuario usuario)
        {
            await _contexto.Usuarios.AddAsync(usuario);
            await _contexto.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            _contexto.Usuarios.Update(usuario);
            await _contexto.SaveChangesAsync();
        }

        public async Task DeletarAsync(int id)
        {
            var usuario = await _contexto.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _contexto.Usuarios.Remove(usuario);
                await _contexto.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _contexto.Usuarios.AnyAsync(e => e.Id == id);
        }

        public async Task<Usuario> PegarPorNomeUsuarioAsync(string nomeUsuario)
        {
            return await _contexto.Usuarios
                .FirstOrDefaultAsync(u => u.NomeUsuario == nomeUsuario);
        }
    }
}
