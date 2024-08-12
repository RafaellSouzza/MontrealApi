using Moq;
using MontrealApi.Models;
using MontrealApi.Repositories;
using MontrealApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MontrealApi.Tests.Services
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private readonly IUsuarioService _usuarioService;

        public UsuarioServiceTests()
        {
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _usuarioService = new UsuarioService(_usuarioRepositoryMock.Object);
        }

        [Fact]
        public async Task PegarTodosUsuarios_DeveRetornarTodosOsUsuarios()
        {
            var usuarios = new List<Usuario>
            {
                new Usuario { Id = 1, NomeUsuario = "joao123", Senha = "senha1234", Role = RoleEnum.Admin },
                new Usuario { Id = 2, NomeUsuario = "maria456", Senha = "senha4567", Role = RoleEnum.User }
            };

            _usuarioRepositoryMock.Setup(repo => repo.PegarTodosAsync()).ReturnsAsync(usuarios);

            var resultado = await _usuarioService.PegarTodosAsync();

            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public async Task PegarUsuarioPorId_DeveRetornarUsuarioPeloId()
        {
            var usuario = new Usuario { Id = 1, NomeUsuario = "joao123", Senha = "senha1234", Role = RoleEnum.Admin };

            _usuarioRepositoryMock.Setup(repo => repo.PegarPorIdAsync(1)).ReturnsAsync(usuario);

            var resultado = await _usuarioService.PegarPorIdAsync(1);

            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.Id);
            Assert.Equal("joao123", resultado.NomeUsuario);
        }

        [Fact]
        public async Task AdicionarUsuario_DeveChamarRepositorioAdicionar()
        {
            var usuario = new Usuario { Id = 1, NomeUsuario = "joao123", Senha = "senha1234", Role = RoleEnum.Admin };

            await _usuarioService.AdicionarAsync(usuario);

            _usuarioRepositoryMock.Verify(repo => repo.AdicionarAsync(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact]
        public async Task AtualizarUsuario_DeveChamarRepositorioAtualizar()
        {
            var usuario = new Usuario { Id = 1, NomeUsuario = "joao123", Senha = "senha1234", Role = RoleEnum.Admin };

            await _usuarioService.AtualizarAsync(usuario);

            _usuarioRepositoryMock.Verify(repo => repo.AtualizarAsync(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact]
        public async Task DeletarUsuario_DeveChamarRepositorioDeletar()
        {
            _usuarioRepositoryMock.Setup(repo => repo.ExisteAsync(1)).ReturnsAsync(true);

            await _usuarioService.DeletarAsync(1);

            _usuarioRepositoryMock.Verify(repo => repo.DeletarAsync(1), Times.Once);
        }
    }
}
