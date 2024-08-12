using System.ComponentModel.DataAnnotations;

namespace MontrealApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string NomeUsuario { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,12}$", ErrorMessage = "A senha dev conter pelo menos uma letra e um número.")]
        public string Senha { get; set; }

        [Required]
        [EnumDataType(typeof(RoleEnum))]
        public RoleEnum Role { get; set; }
    }
}
