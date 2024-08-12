using Microsoft.EntityFrameworkCore;
using MontrealApi.Data;
using MontrealApi.Models;

namespace MontrealApi.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly ApplicationDbContext _contexto;

        public PessoaRepository(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Pessoa>> PegarTodasAsync()
        {
            return await _contexto.Pessoas.ToListAsync();
        }

        public async Task<Pessoa> PegarPorIdAsync(int id)
        {
            return await _contexto.Pessoas.FindAsync(id);
        }

        public async Task AdicionarAsync(Pessoa pessoa)
        {
            if (await CPFJaExisteAsync(pessoa.CPF))
                throw new InvalidOperationException("O CPF já está cadastrado.");

            await _contexto.Pessoas.AddAsync(pessoa);
            await _contexto.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Pessoa pessoa)
        {
            if (await CPFJaExisteAsync(pessoa.CPF, pessoa.Id))
                throw new InvalidOperationException("O CPF já está cadastrado para outra pessoa.");

            _contexto.Pessoas.Update(pessoa);
            await _contexto.SaveChangesAsync();
        }

        public async Task DeletarAsync(int id)
        {
            var pessoa = await _contexto.Pessoas.FindAsync(id);
            if (pessoa != null)
            {
                _contexto.Pessoas.Remove(pessoa);
                await _contexto.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _contexto.Pessoas.AnyAsync(e => e.Id == id);
        }

        public async Task<(IEnumerable<Pessoa>, int)> PegarPaginadoAsync(
            int pageIndex,
            int pageSize,
            string nome = null,
            string cpf = null,
            DateTime? dataNascimento = null,
            string sexo = null)
        {
            var query = _contexto.Pessoas.AsQueryable();

            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(p => p.Nome.Contains(nome));
            }

            if (!string.IsNullOrEmpty(cpf))
            {
                query = query.Where(p => p.CPF.Contains(cpf));
            }

            if (dataNascimento.HasValue)
            {
                query = query.Where(p => p.DataNascimento == dataNascimento.Value);
            }

            if (!string.IsNullOrEmpty(sexo) && Enum.TryParse<SexoEnum>(sexo, out var sexoEnum))
            {
                query = query.Where(p => p.Sexo == sexoEnum);
            }

            var totalItens = await query.CountAsync();

            var pessoas = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (pessoas, totalItens);
        }

        private async Task<bool> CPFJaExisteAsync(string cpf, int? pessoaId = null)
        {
            return await _contexto.Pessoas
                .AnyAsync(p => p.CPF == cpf && (pessoaId == null || p.Id != pessoaId));
        }

        public async Task<Foto> PegarFotoMaisRecenteAsync(int pessoaId)
        {
            return await _contexto.Fotos
                .Where(f => f.PessoaId == pessoaId)
                .OrderByDescending(f => f.DataEnvio)
                .FirstOrDefaultAsync();
        }
    }
}
