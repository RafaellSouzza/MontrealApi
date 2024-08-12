using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MontrealApi.Models;
using MontrealApi.Services;

namespace MontrealApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> PegarTodosUsuarios()
        {
            var usuarios = await _usuarioService.PegarTodosAsync();
            return Ok(usuarios);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> PegarUsuarioPorId(int id)
        {
            var usuario = await _usuarioService.PegarPorIdAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Usuario>> AdicionarUsuario(Usuario usuario)
        {
            await _usuarioService.AdicionarAsync(usuario);
            return CreatedAtAction(nameof(PegarUsuarioPorId), new { id = usuario.Id }, usuario);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            await _usuarioService.AtualizarAsync(usuario);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarUsuario(int id)
        {
            await _usuarioService.DeletarAsync(id);
            return NoContent();
        }
    }
}
