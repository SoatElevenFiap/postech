namespace Soat.Eleven.FastFood.Domain.Entidades
{
    public class LogPedido
    {
        public Guid Id { get; set; }
        public Guid PedidoId { get; set; }
        public string StatusAtual { get; set; } = null!;
        public Guid? AlteradoPor { get; set; }
        public string? IP { get; set; }
        public DateTime DataAlteracao { get; set; }
        public Pedido Pedido { get; set; } = null!;
        public Usuario? UsuarioSistema { get; set; }
    }

}
