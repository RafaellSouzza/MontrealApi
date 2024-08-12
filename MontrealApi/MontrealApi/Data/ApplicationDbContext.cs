using Microsoft.EntityFrameworkCore;
using MontrealApi.Models;

namespace MontrealApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Foto> Fotos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome)
                      .IsRequired()
                      .HasMaxLength(20);
                entity.Property(e => e.Sobrenome)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.Property(e => e.CPF)
                      .IsRequired()
                      .HasMaxLength(11);
                entity.Property(e => e.DataNascimento)
                      .IsRequired();
                entity.Property(e => e.Sexo)
                      .IsRequired();
                entity.Property(e => e.FotoPath)
                      .HasMaxLength(255)
                      .IsRequired(false);

                entity.HasMany(e => e.ListaFotos)
                      .WithOne(f => f.Pessoa)
                      .IsRequired(false)
                      .HasForeignKey(f => f.PessoaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NomeUsuario)
                      .IsRequired()
                      .HasMaxLength(50);
                entity.Property(e => e.Senha)
                      .IsRequired()
                      .HasMaxLength(12);
                entity.Property(e => e.Role)
                      .IsRequired()
                      .HasMaxLength(20);
            });

            modelBuilder.Entity<Foto>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Caminho)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(e => e.DataEnvio)
                      .IsRequired();

                entity.Property(e => e.Principal)
                      .IsRequired();

                entity.Property(e => e.Imagem)
                      .IsRequired(false);

                entity.HasOne(f => f.Pessoa)
                      .WithMany(p => p.ListaFotos)
                      .HasForeignKey(f => f.PessoaId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

        }
    }
}
