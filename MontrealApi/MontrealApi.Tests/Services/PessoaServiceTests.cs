using Moq;
using MontrealApi.Models;
using MontrealApi.Repositories;
using MontrealApi.Services;

namespace MontrealApi.Tests.Services
{
    public class PessoaServiceTests
    {
        private readonly Mock<IPessoaRepository> _pessoaRepositoryMock;
        private readonly IPessoaService _pessoaService;

        public PessoaServiceTests()
        {
            _pessoaRepositoryMock = new Mock<IPessoaRepository>();
            _pessoaService = new PessoaService(_pessoaRepositoryMock.Object);
        }

        [Fact]
        public async Task PegarTodasPessoas()
        {
            var pessoas = new List<Pessoa>
            {
                new Pessoa { Id = 1, Nome = "João", Sobrenome = "Silva", CPF = "123.456.789-01" },
                new Pessoa { Id = 2, Nome = "Maria", Sobrenome = "Souza", CPF = "987.654.321-01" }
            };

            _pessoaRepositoryMock.Setup(repo => repo.PegarTodasAsync()).ReturnsAsync(pessoas);

            var resultado = await _pessoaService.PegarTodasAsync();

            Assert.Equal(2, resultado.Count());
            Assert.Equal("João", resultado.First().Nome);
            Assert.Equal("Maria", resultado.Last().Nome);
        }

        [Fact]
        public async Task PegarPessoaPorId()
        {
            var pessoa = new Pessoa { Id = 1, Nome = "João", Sobrenome = "Silva", CPF = "123.456.789-01" };

            _pessoaRepositoryMock.Setup(repo => repo.PegarPorIdAsync(1)).ReturnsAsync(pessoa);

            var resultado = await _pessoaService.PegarPorIdAsync(1);

            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.Id);
            Assert.Equal("João", resultado.Nome);
        }

        [Fact]
        public async Task AdicionarPessoa()
        {
            var pessoa = new Pessoa { Id = 1, Nome = "João", Sobrenome = "Silva", CPF = "123.456.789-01" };

            await _pessoaService.AdicionarAsync(pessoa);

            _pessoaRepositoryMock.Verify(repo => repo.AdicionarAsync(It.IsAny<Pessoa>()), Times.Once, "O método AdicionarAsync não foi chamado o número esperado de vezes.");
        }

        [Fact]
        public async Task AtualizarPessoa()
        {
            var pessoa = new Pessoa { Id = 1, Nome = "João", Sobrenome = "Silva", CPF = "123.456.789-01" };

            await _pessoaService.AtualizarAsync(pessoa);

            _pessoaRepositoryMock.Verify(repo => repo.AtualizarAsync(It.IsAny<Pessoa>()), Times.Once, "O método AtualizarAsync não foi chamado o número esperado de vezes.");
        }

        [Fact]
        public async Task DeletarPessoa()
        {
            _pessoaRepositoryMock.Setup(repo => repo.ExisteAsync(1)).ReturnsAsync(true);

            await _pessoaService.DeletarAsync(1);

            _pessoaRepositoryMock.Verify(repo => repo.DeletarAsync(1), Times.Once, "O método DeletarAsync não foi chamado o número esperado de vezes.");
        }

        [Fact]
        public async Task PegarPaginadoAsync()
        {
            var pessoas = new List<Pessoa>
            {
                new Pessoa { Id = 1, Nome = "João", Sobrenome = "Silva", CPF = "123.456.789-01", DataNascimento = new DateTime(1990, 1, 1), Sexo = SexoEnum.Masculino },
                new Pessoa { Id = 2, Nome = "Maria", Sobrenome = "Souza", CPF = "987.654.321-01", DataNascimento = new DateTime(1992, 2, 2), Sexo = SexoEnum.Feminino }
            };

            _pessoaRepositoryMock.Setup(repo => repo.PegarPaginadoAsync(1, 10, null, null, null, null))
                .ReturnsAsync((pessoas, pessoas.Count));

            var (resultado, totalItens) = await _pessoaService.PegarPaginadoAsync(1, 10);

            Assert.Equal(2, totalItens);
            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public async Task PegarPaginadoAsyncComFiltros()
        {
            var pessoas = new List<Pessoa>
            {
                new Pessoa { Id = 1, Nome = "João", Sobrenome = "Silva", CPF = "123.456.789-01", DataNascimento = new DateTime(1990, 1, 1), Sexo = SexoEnum.Masculino }
            };

            _pessoaRepositoryMock.Setup(repo => repo.PegarPaginadoAsync(1, 10, "João", "123.456.789-01", new DateTime(1990, 1, 1), "Masculino"))
                .ReturnsAsync((pessoas, pessoas.Count));

            var (resultado, totalItens) = await _pessoaService.PegarPaginadoAsync(1, 10, "João", "123.456.789-01", new DateTime(1990, 1, 1), "Masculino");

            Assert.Equal(1, totalItens);
            Assert.Single(resultado);
            Assert.Equal("João", resultado.First().Nome);
        }

        [Fact]
        public async Task PegarPaginadoAsyncSemResultados()
        {
            _pessoaRepositoryMock.Setup(repo => repo.PegarPaginadoAsync(1, 10, "Inexistente", null, null, null))
                .ReturnsAsync((new List<Pessoa>(), 0));

            var (resultado, totalItens) = await _pessoaService.PegarPaginadoAsync(1, 10, "Inexistente");

            Assert.Equal(0, totalItens);
            Assert.Empty(resultado);
        }
    }
}
