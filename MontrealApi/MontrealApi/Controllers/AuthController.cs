using Microsoft.AspNetCore.Mvc;
using MontrealApi.Repositories;
using MontrealApi.Services;

namespace MontrealApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly TokenService _tokenService;

        public AuthController(IUsuarioRepository usuarioRepository, TokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var usuario = await _usuarioRepository.PegarPorNomeUsuarioAsync(login.Username);

            if (usuario != null && usuario.Senha == login.Password)
            {
                var token = _tokenService.GenerateToken(usuario.NomeUsuario, usuario.Role.ToString());

                return Ok(new { Token = token });
            }

            return Unauthorized("Usuário ou senha inválidos.");
        }
    }

    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

}
