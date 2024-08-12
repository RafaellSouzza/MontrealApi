using MontrealApi.Models;

namespace MontrealApi.Services
{
    public interface IPessoaService
    {
        Task<IEnumerable<Pessoa>> PegarTodasAsync();
        Task<Pessoa> PegarPorIdAsync(int id);
        Task AdicionarAsync(Pessoa pessoa);
        Task AtualizarAsync(Pessoa pessoa);
        Task DeletarAsync(int id);
        Task<(IEnumerable<Pessoa>, int)> PegarPaginadoAsync(
            int pageIndex,
            int pageSize,
            string nome = null,
            string cpf = null,
            DateTime? dataNascimento = null,
            string sexo = null);

        Task<Foto> PegarFotoMaisRecenteAsync(int pessoaId);
        Task<object> UploadFotoBase64Async(int id, UploadFotoBase64Request request);
    }
}