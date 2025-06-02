using FluentValidation;
using Soat.Eleven.FastFood.Core.Domain.Contratos.Pedido.Inputs;

namespace Soat.Eleven.FastFood.Application.Validators.Pedido
{
    public class ItemPedidoRequestDtoValidator : AbstractValidator<ItemPedidoInput>
    {
        public ItemPedidoRequestDtoValidator()
        {
            RuleFor(x => x.ProdutoId)
                .NotEmpty().WithMessage("ProdutoId é obrigatório.");

            RuleFor(x => x.Quantidade)
                .GreaterThan(0).WithMessage("Quantidade deve ser maior que zero.");

            RuleFor(x => x.PrecoUnitario)
                .GreaterThan(0).WithMessage("Preço unitário deve ser maior que zero.");

            RuleFor(x => x.DescontoUnitario)
                .GreaterThanOrEqualTo(0).WithMessage("Desconto unitário deve ser maior ou igual a zero.");
        }
    }
}
