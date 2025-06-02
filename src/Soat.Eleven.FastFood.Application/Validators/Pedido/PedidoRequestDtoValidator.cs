using FluentValidation;
using Soat.Eleven.FastFood.Core.Domain.Contratos.Pedido.Request;

namespace Soat.Eleven.FastFood.Application.Validators.Pedido
{
    public class PedidoRequestDtoValidator : AbstractValidator<PedidoRequestDto>
    {
        public PedidoRequestDtoValidator()
        {
            RuleFor(x => x.TokenAtendimentoId)
                .NotEmpty().WithMessage("TokenAtendimentoId é obrigatório.");

            RuleFor(x => x.Subtotal)
                .GreaterThan(0).WithMessage("Subtotal deve ser maior que zero.");

            RuleFor(x => x.Desconto)
                .GreaterThanOrEqualTo(0).WithMessage("Desconto deve ser maior ou igual a zero.");

            RuleFor(x => x.Total)
                .GreaterThan(0).WithMessage("Total deve ser maior que zero.");

            RuleFor(x => x.Itens)
                .NotNull().WithMessage("Itens não pode ser nulo.")
                .Must(itens => itens != null && itens.Count != 0).WithMessage("Pedido deve conter pelo menos um item.");

            RuleForEach(x => x.Itens)
                .SetValidator(new ItemPedidoRequestDtoValidator());

            RuleFor(x => x)
                .Custom((dto, context) =>
                {
                    if (dto.Itens == null || dto.Itens.Count == 0)
                        return;

                    var subtotalItens = dto.Itens.Sum(i => i.PrecoUnitario * i.Quantidade);
                    var descontoItens = dto.Itens.Sum(i => i.DescontoUnitario * i.Quantidade);
                    var totalItens = subtotalItens - descontoItens;

                    if (Math.Round(dto.Subtotal, 2) != Math.Round(subtotalItens, 2))
                        context.AddFailure("Subtotal", $"Subtotal do pedido ({dto.Subtotal}) não confere com a soma dos itens ({subtotalItens}).");

                    if (Math.Round(dto.Desconto, 2) != Math.Round(descontoItens, 2))
                        context.AddFailure("Desconto", $"Desconto do pedido ({dto.Desconto}) não confere com a soma dos descontos dos itens ({descontoItens}).");

                    if (Math.Round(dto.Total, 2) != Math.Round(totalItens, 2))
                        context.AddFailure("Total", $"Total do pedido ({dto.Total}) não confere com a soma dos itens menos descontos ({totalItens}).");
                });
        }
    }
}
