using Microsoft.EntityFrameworkCore;
using Soat.Eleven.FastFood.Domain.Entidades;
using Soat.Eleven.FastFood.Infra.Data.ModelConfiguration;

namespace Soat.Eleven.FastFood.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<CategoriaProduto> CategoriasProduto { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<DescontoProduto> DescontosProduto { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<PagamentoPedido> PagamentosPedido { get; set; }
        public DbSet<TokenAtendimento> TokensAtendimento { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacionamento opcional Cliente.UsuarioId
            modelBuilder.ApplyConfiguration(new ClienteModelConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioModelConfiguration());

            #region Configuration Pedido
            modelBuilder.ApplyConfiguration(new PedidoModelConfiguration());
            modelBuilder.ApplyConfiguration(new ItemPedidoModelConfiguration());
            modelBuilder.ApplyConfiguration(new PagamentoPedidoModelConfiguration());
            #endregion

            modelBuilder.Entity<Produto>()
                .Property(p => p.Preco)
                .HasPrecision(10, 2);

            modelBuilder.Entity<DescontoProduto>()
                .Property(d => d.Valor)
                .HasPrecision(10, 2);

            modelBuilder.Entity<TokenAtendimento>()
             .HasNoKey();
        }
    }

}
