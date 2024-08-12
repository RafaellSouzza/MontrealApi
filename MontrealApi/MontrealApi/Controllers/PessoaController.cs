using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MontrealApi.Models;
using MontrealApi.Services;

namespace MontrealApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;

        public PessoaController(IPessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoa>>> PegarTodasPessoas()
        {
            var pessoas = await _pessoaService.PegarTodasAsync();
            return Ok(pessoas);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> PegarPessoaPorId(int id)
        {
            var pessoa = await _pessoaService.PegarPorIdAsync(id);

            if (pessoa == null)
            {
                return NotFound();
            }

            return Ok(pessoa);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("paginado")]
        public async Task<ActionResult<(IEnumerable<Pessoa>, int)>> PegarPaginado(
            int pageIndex = 1,
            int pageSize = 10,
            string nome = null,
            string cpf = null,
            DateTime? dataNascimento = null,
            string sexo = null)
        {
            var resultado = await _pessoaService.PegarPaginadoAsync(pageIndex, pageSize, nome, cpf, dataNascimento, sexo);
            return Ok(new { Pessoas = resultado.Item1, TotalCount = resultado.Item2 });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Pessoa>> AdicionarPessoa(Pessoa pessoa)
        {
            await _pessoaService.AdicionarAsync(pessoa);
            return CreatedAtAction(nameof(PegarPessoaPorId), new { id = pessoa.Id }, pessoa);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarPessoa(int id, Pessoa pessoa)
        {
            if (id != pessoa.Id)
            {
                return BadRequest();
            }

            await _pessoaService.AtualizarAsync(pessoa);

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarPessoa(int id)
        {
            await _pessoaService.DeletarAsync(id);
            return NoContent();
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("{id}/foto")]
        public async Task<ActionResult> PegarFotoMaisRecente(int id)
        {
            var foto = await _pessoaService.PegarFotoMaisRecenteAsync(id);

            if (foto == null)
            {
                return NotFound("Foto não encontrada.");
            }

            var caminhoCompleto = Path.Combine(Directory.GetCurrentDirectory(), "uploads", foto.Caminho);

            if (!System.IO.File.Exists(caminhoCompleto))
            {
                return NotFound("Arquivo de foto não encontrado.");
            }

            var image = System.IO.File.ReadAllBytes(caminhoCompleto);
            return File(image, "image/jpeg");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id}/upload-foto-base64")]
        public async Task<IActionResult> UploadFotoBase64(int id, [FromBody] UploadFotoBase64Request request)
        {
            try
            {
                var result = await _pessoaService.UploadFotoBase64Async(id, request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
