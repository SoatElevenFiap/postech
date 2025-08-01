using Soat.Eleven.FastFood.Core.Enums;

namespace Soat.Eleven.FastFood.Core.DTOs.Webhooks;

public class NotificacaoPagamentoDto
{
    public string ExternalId { get; set; }
    public StatusPagamento Status { get; set; }
}