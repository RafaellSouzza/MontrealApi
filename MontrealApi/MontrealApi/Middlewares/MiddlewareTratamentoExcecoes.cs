using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MontrealApi.Middlewares
{
    public class MiddlewareTratamentoExcecoes
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddlewareTratamentoExcecoes> _logger;

        public MiddlewareTratamentoExcecoes(RequestDelegate next, ILogger<MiddlewareTratamentoExcecoes> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext contexto)
        {
            try
            {
                await _next(contexto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro: {ex.Message}");
                await TratarExcecaoAsync(contexto, ex);
            }
        }

        private Task TratarExcecaoAsync(HttpContext contexto, Exception excecao)
        {
            contexto.Response.ContentType = "application/json";
            contexto.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var detalhesErro = new DetalhesErro
            {
                StatusCode = contexto.Response.StatusCode,
                Mensagem = "Erro interno do servidor a partir do middleware personalizado."
            };

            return contexto.Response.WriteAsync(detalhesErro.ToString());
        }
    }

    public class DetalhesErro
    {
        public int StatusCode { get; set; }
        public string Mensagem { get; set; }

        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}
