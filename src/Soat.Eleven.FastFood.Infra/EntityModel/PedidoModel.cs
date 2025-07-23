﻿using Soat.Eleven.FastFood.Adapter.Infra.EntityModel.Base;
using Soat.Eleven.FastFood.Core.Enums;

namespace Soat.Eleven.FastFood.Adapter.Infra.EntityModel
{
    public class PedidoModel : EntityBase
    {
        public PedidoModel()
        {
            //Construtor vazio para o ORM
        }

        public PedidoModel(Guid tokenAtendimentoId, Guid? clienteId, decimal subtotal, decimal desconto, decimal total)
        {
            TokenAtendimentoId = tokenAtendimentoId;
            ClienteId = clienteId;                    
            Subtotal = subtotal;
            Desconto = desconto;
            Total = total;
            Status = StatusPedido.Pendente; //O pedido nasce com Status Pendente
        }
        public Guid TokenAtendimentoId { get; set; }
        public Guid? ClienteId { get; set; }
        public StatusPedido Status { get; set; }
        public string SenhaPedido { get; set; } = null!;
        public decimal Subtotal { get; set; }
        public decimal Desconto { get; set; }
        public decimal Total { get; set; }

        public ClienteModel Cliente { get; set; } = null!;
        public ICollection<ItemPedidoModel> Itens { get; set; } = [];
        public ICollection<PagamentoPedidoModel> Pagamentos { get; set; } = [];

        public void GerarSenha()
        {
            // Gera a senha baseada no TokenAtendimentoId, garantindo que não se repita  
            var random = new Random();
            var uniquePart = random.Next(100000, 999999).ToString();
            SenhaPedido = $"{TokenAtendimentoId.ToString("N")[..4].ToUpper()}{uniquePart}";
        }

        public void AdicionarItem(ItemPedidoModel item)
        {
            ArgumentNullException.ThrowIfNull(item, nameof(item));

            Itens.Add(item);
        }

        public void AdicionarItens(ICollection<ItemPedidoModel> itens)
        {
            ArgumentNullException.ThrowIfNull(itens, nameof(itens));

            foreach (var item in itens)
            {
                AdicionarItem(item);
            }
        }

        public void RemoverItem(Guid itemId)
        {
            var item = Itens.FirstOrDefault(i => i.Id == itemId);

            if (item == null)
            {
                return;
            }

            Itens.Remove(item);
        }

        public void AdicionarPagamento(PagamentoPedidoModel pagamento)
        {
            ArgumentNullException.ThrowIfNull(pagamento, nameof(pagamento));

            Pagamentos.Add(pagamento);
        }
    }
}
