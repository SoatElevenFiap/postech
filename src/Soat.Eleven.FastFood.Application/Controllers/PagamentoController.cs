using Soat.Eleven.FastFood.Core.Interfaces.Gateways;

namespace Soat.Eleven.FastFood.Application.Controllers;

public class PagamentoController
{
    private readonly IPagamentoGateway pagamentoGateway;

    public PagamentoController(IPagamentoGateway pagamentoGateway)
    {
        this.pagamentoGateway = pagamentoGateway;
    }
}
