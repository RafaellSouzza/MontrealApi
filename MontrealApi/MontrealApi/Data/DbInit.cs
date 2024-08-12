using MontrealApi.Models;

namespace MontrealApi.Data
{
    public static class DbInit
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Pessoas.Any())
            {
                return; 
            }

            var pessoas = new Pessoa[]
            {
                new Pessoa { Nome = "Rafael", Sobrenome = "Souza", CPF = "12345678901", DataNascimento = DateTime.Parse("1990-05-15"), Sexo = SexoEnum.Masculino, FotoPath = null },
                new Pessoa { Nome = "Maria", Sobrenome = "Oliveira", CPF = "23456789012", DataNascimento = DateTime.Parse("1985-08-20"), Sexo = SexoEnum.Feminino, FotoPath = null },
                new Pessoa { Nome = "Carlos", Sobrenome = "Pereira", CPF = "34567890123", DataNascimento = DateTime.Parse("1980-07-10"), Sexo = SexoEnum.Masculino, FotoPath = null},
                new Pessoa { Nome = "Ana", Sobrenome = "Souza", CPF = "45678901234", DataNascimento = DateTime.Parse("1995-12-25"), Sexo = SexoEnum.Feminino, FotoPath = null},
                new Pessoa { Nome = "Luiz", Sobrenome = "Mendes", CPF = "56789012345", DataNascimento = DateTime.Parse("2000-02-28"), Sexo = SexoEnum.Masculino, FotoPath = null }
            };
            context.Pessoas.AddRange(pessoas);
            context.SaveChanges();

            var usuarios = new Usuario[]
            {
                new Usuario { NomeUsuario = "Admin", Senha = "123", Role = RoleEnum.Admin },
                new Usuario { NomeUsuario = "souza", Senha = "senha123", Role = RoleEnum.User },
                new Usuario { NomeUsuario = "maria.oliveira", Senha = "senha123", Role = RoleEnum.User },
                new Usuario { NomeUsuario = "carlos.pereira", Senha = "senha123", Role = RoleEnum.User },
                new Usuario { NomeUsuario = "ana.souza", Senha = "senha123", Role = RoleEnum.User },
                new Usuario { NomeUsuario = "luiz.mendes", Senha = "senha123", Role = RoleEnum.Admin }
            };
            context.Usuarios.AddRange(usuarios);
            context.SaveChanges();

            var fotos = new Foto[]
            {
                new Foto { PessoaId = 1, Caminho = "fotos/souza1.jpg", DataEnvio = DateTime.Now, Principal = true, Imagem = new byte[0] },
                new Foto { PessoaId = 2, Caminho = "fotos/maria1.jpg", DataEnvio = DateTime.Now, Principal = true, Imagem = new byte[0] },
                new Foto { PessoaId = 3, Caminho = "fotos/carlos1.jpg", DataEnvio = DateTime.Now, Principal = true, Imagem = new byte[0] },
                new Foto { PessoaId = 4, Caminho = "fotos/ana1.jpg", DataEnvio = DateTime.Now, Principal = true, Imagem = new byte[0] },
                new Foto { PessoaId = 5, Caminho = "fotos/luiz1.jpg", DataEnvio = DateTime.Now, Principal = true, Imagem = new byte[0] }
            };
            context.Fotos.AddRange(fotos);
            context.SaveChanges();

        }
    }
}
