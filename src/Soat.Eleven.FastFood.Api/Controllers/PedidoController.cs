using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Application.DTOs;
using Soat.Eleven.FastFood.Application.Services.Interfaces;

namespace Soat.Eleven.FastFood.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly IPedidoService _pedidoService;

        public PedidoController(ILogger<PedidoController> logger, IPedidoService pedidoService)
        {
            _logger = logger;
            _pedidoService = pedidoService;
        }

        [HttpPost("CriarPedido")]
        public async Task<IActionResult> CriarPedido([FromBody] PedidoDTO pedidoDto)
        {
            if (pedidoDto == null)
            {
                return BadRequest("Pedido inválido.");
            }

            try
            {
                var pedidoCriado = await _pedidoService.CriarPedido(pedidoDto);
                return CreatedAtAction(nameof(CriarPedido), pedidoCriado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar pedido.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpGet("ListarPedidos")]
        public async Task<IActionResult> ListarPedidos()
        {
            try
            {
                var pedidos = await _pedidoService.ListarPedido();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar pedidos.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }
    }
}
