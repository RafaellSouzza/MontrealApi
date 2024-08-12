using System.ComponentModel.DataAnnotations;

namespace MontrealApi.Models
{
    public class Pessoa
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Sobrenome { get; set; }

        [Required]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}\-\d{2}$", ErrorMessage = "CPF inválido")]
        public string CPF { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Pessoa), "ValidarDataNascimento")]
        public DateTime DataNascimento { get; set; }

        [Required]
        [EnumDataType(typeof(SexoEnum))]
        public SexoEnum Sexo { get; set; }

        [StringLength(255)]
        public string FotoPath { get; set; }

        public List<Foto> ListaFotos { get; set; } = new List<Foto>();

        public static ValidationResult ValidarDataNascimento(DateTime dataNascimento, ValidationContext context)
        {
            if (dataNascimento <= new DateTime(1900, 1, 1) || dataNascimento >= DateTime.Now)
            {
                return new ValidationResult("Data de nascimento inválida.");
            }
            return ValidationResult.Success;
        }
    }

}
