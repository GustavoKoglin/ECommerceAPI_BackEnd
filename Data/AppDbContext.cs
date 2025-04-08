using Microsoft.EntityFrameworkCore;
using E_Commerce_API.Models.Entities;

namespace E_Commerce_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<CarrinhoItem> CarrinhoItens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Herda do IdentityDbContext

            modelBuilder.Entity<Produto>()
                .HasIndex(p => p.Nome)
                .IsUnique();

            // Relacionamento Carrinho -> Usuario
            modelBuilder.Entity<Carrinho>()
                .HasOne<Usuario>()
                .WithOne(u => u.Carrinho)
                .HasForeignKey<Carrinho>(c => c.UsuarioId);

            // Relacionamento CarrinhoItem -> Carrinho
            modelBuilder.Entity<CarrinhoItem>()
                .HasOne(ci => ci.Carrinho)
                .WithMany(c => c.Itens)
                .HasForeignKey(ci => ci.CarrinhoId);

            // Relacionamento CarrinhoItem -> Produto
            modelBuilder.Entity<CarrinhoItem>()
               .HasOne(ci => ci.Produto)
               .WithMany(p => p.CarrinhoItens)
               .HasForeignKey(ci => ci.ProdutoId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}