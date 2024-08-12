namespace MontrealApi.Models
{
    public class RequestResult
    {
        public bool Sucesso { get; }
        public string Mensagem { get; }

        public RequestResult(bool sucesso, string mensagem)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
        }
    }
}
