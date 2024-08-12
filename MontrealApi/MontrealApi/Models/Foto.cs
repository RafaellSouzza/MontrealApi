using System.ComponentModel.DataAnnotations;

namespace MontrealApi.Models
{
    public class Foto
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }

        [Required]
        public string Caminho { get; set; }

        [Required]
        public DateTime DataEnvio { get; set; }

        [Required]
        public bool Principal { get; set; }

        [Required]
        public byte[] Imagem { get; set; } 

        public Pessoa Pessoa { get; set; }
    }
}
