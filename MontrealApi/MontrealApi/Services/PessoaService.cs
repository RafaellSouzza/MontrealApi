using MontrealApi.Models;
using MontrealApi.Repositories;

namespace MontrealApi.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _repositorioDePessoas;

        public PessoaService(IPessoaRepository repositorioDePessoas)
        {
            _repositorioDePessoas = repositorioDePessoas;
        }

        public async Task<IEnumerable<Pessoa>> PegarTodasAsync()
        {
            return await _repositorioDePessoas.PegarTodasAsync();
        }

        public async Task<Pessoa> PegarPorIdAsync(int id)
        {
            return await _repositorioDePessoas.PegarPorIdAsync(id);
        }

        public async Task AdicionarAsync(Pessoa pessoa)
        {
            await _repositorioDePessoas.AdicionarAsync(pessoa);
        }

        public async Task AtualizarAsync(Pessoa pessoa)
        {
            await _repositorioDePessoas.AtualizarAsync(pessoa);
        }

        public async Task DeletarAsync(int id)
        {
            await _repositorioDePessoas.DeletarAsync(id);
        }

        public async Task<(IEnumerable<Pessoa>, int)> PegarPaginadoAsync(
            int pageIndex,
            int pageSize,
            string nome = null,
            string cpf = null,
            DateTime? dataNascimento = null,
            string sexo = null)
        {
            return await _repositorioDePessoas.PegarPaginadoAsync(pageIndex, pageSize, nome, cpf, dataNascimento, sexo);
        }

        public async Task<Foto> PegarFotoMaisRecenteAsync(int pessoaId)
        {
            return await _repositorioDePessoas.PegarFotoMaisRecenteAsync(pessoaId);
        }


        public async Task<object> UploadFotoBase64Async(int id, UploadFotoBase64Request request)
        {
            if (string.IsNullOrWhiteSpace(request.Base64))
            {
                throw new ArgumentException("Por favor, envie uma string Base64 válida.");
            }

            var pessoa = await _repositorioDePessoas.PegarPorIdAsync(id);
            if (pessoa == null)
            {
                throw new KeyNotFoundException("Pessoa não encontrada.");
            }

            byte[] fotoBytes;
            try
            {
                fotoBytes = Convert.FromBase64String(request.Base64);
            }
            catch (FormatException)
            {
                throw new ArgumentException("A string Base64 fornecida é inválida.");
            }

            var fileName = $"{id}_{Guid.NewGuid()}.{request.Extensao}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "uploads", fileName);

            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "uploads")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "uploads"));
            }

            await System.IO.File.WriteAllBytesAsync(path, fotoBytes);

            var novaFoto = new Foto
            {
                PessoaId = id,
                Caminho = fileName,
                DataEnvio = DateTime.Now,
                Principal = true
            };

            pessoa.ListaFotos.Add(novaFoto);
            await _repositorioDePessoas.AtualizarAsync(pessoa);

            return new { Message = "Foto enviada com sucesso!", FilePath = path };
        }
    }
}
