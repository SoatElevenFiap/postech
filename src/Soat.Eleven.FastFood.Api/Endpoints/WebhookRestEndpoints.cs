using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Application.Controllers;
using Soat.Eleven.FastFood.Common.Interfaces.DataSources;
using Soat.Eleven.FastFood.Core.DTOs.Pagamentos;
using Soat.Eleven.FastFood.Core.DTOs.Webhooks;
using Soat.Eleven.FastFood.Core.Enums;
using Soat.Eleven.FastFood.Core.Gateways;
using Soat.Eleven.FastFood.Core.Interfaces.DataSources;
using Soat.Eleven.FastFood.Core.Interfaces.Services;

namespace Soat.Eleven.FastFood.Api.Controllers;

[ApiController]
[Route("api")]
public class WebhookRestEndpoints : ControllerBase
{
    private readonly IMercadoPagoService _mercadoPagoService;
    private readonly PagamentoGateway _pagamentoGateway;
    private readonly PedidoController _pedidoController;

    public WebhookRestEndpoints(IMercadoPagoService mercadoPagoService, IPagamentoDataSource pagamentoDataSource, IPedidoDataSource pedidoDataSource)
    {
        _mercadoPagoService = mercadoPagoService;
        _pagamentoGateway = new PagamentoGateway(pagamentoDataSource);
        _pedidoController = new PedidoController(pedidoDataSource);
    }
    
    [HttpPost("Webhook/Pagamento/MercadoPago")]
    public async Task<IActionResult> ProcessarWebhookPagamentoMp(
        [FromQuery] String type, 
        [FromHeader(Name = "x-signature")] String signature,
        [FromBody] MercadoPagoNotificationDto request)
    {
        if (type != "payment")
        {
            Debug.WriteLine($"Tipo de evento webhook não suportado para mercado pago {type}");
            return Unauthorized();
        }
        
        if (!_mercadoPagoService.ValidarNotificacao(signature))
        {
            return Unauthorized();
        };

        StatusPagamento paymentStatus = _mercadoPagoService.GetStatusPagamento(request);
        await _pedidoController.PagarPedido(new SolicitacaoPagamento
        {
            PedidoId = Guid.Parse(request.Data.Id),
            Tipo = TipoPagamento.MercadoPago,
            Valor = 0
        }, _pagamentoGateway);
        return Ok();
    }
}
