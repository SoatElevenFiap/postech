using Soat.Eleven.FastFood.Core.Domain.ObjetosDeValor;
using Soat.Eleven.FastFood.Domain.Entidades.Base;

namespace Soat.Eleven.FastFood.Domain.Entidades
{
    public class PagamentoPedido : EntityBase
    {
        public PagamentoPedido()
        {
            //Construtor vazio para o ORM
        }

        public PagamentoPedido(TipoPagamento tipo, decimal valor, StatusPagamento status, string autorizacao)
        {
            Tipo = tipo;
            Valor = valor;
            Status = status;
            Autorizacao = autorizacao;
        }

        public Guid PedidoId { get; set; }
        public TipoPagamento Tipo { get; set; }
        public decimal Valor { get; set; }
        public decimal Troco { get; set; }
        public StatusPagamento Status { get; set; }
        public string Autorizacao { get; set; }

        public Pedido Pedido { get; set; } = null!;
    }
}
