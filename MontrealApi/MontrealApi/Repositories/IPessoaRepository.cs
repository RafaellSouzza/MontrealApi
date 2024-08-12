using MontrealApi.Models;

namespace MontrealApi.Repositories
{
    public interface IPessoaRepository
    {
        Task<IEnumerable<Pessoa>> PegarTodasAsync();
        Task<Pessoa> PegarPorIdAsync(int id);
        Task AdicionarAsync(Pessoa pessoa);
        Task AtualizarAsync(Pessoa pessoa);
        Task DeletarAsync(int id);
        Task<bool> ExisteAsync(int id);
        Task<(IEnumerable<Pessoa>, int)> PegarPaginadoAsync(
            int pageIndex,
            int pageSize,
            string nome = null,
            string cpf = null,
            DateTime? dataNascimento = null,
            string sexo = null);

        Task<Foto> PegarFotoMaisRecenteAsync(int pessoaId);
    }
}
