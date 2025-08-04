using Soat.Eleven.FastFood.Core.Enums;

namespace Soat.Eleven.FastFood.Core.DTOs.Pedidos
{
    public class StatusPagamentoPedidoDto
    {
        public StatusPagamento Status { get; set; }
        public string Descricao
        {
            get
            {
                return Status switch
                {
                    StatusPagamento.Pendente => "Pedido Pendente",
                    StatusPagamento.Aprovado => "Pedido Aprovado",
                    StatusPagamento.Rejeitado => "Pedido Rejeitado",
                    StatusPagamento.NaoEncontrado => "Pedido Não Encontrado",
                    _ => "Status Desconhecido"
                };
            }
        }
        public Guid PedidoId { get; set; }
    }
}
