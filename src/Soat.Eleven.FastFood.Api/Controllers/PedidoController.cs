using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Application.DTOs.Pagamento.Request;
using Soat.Eleven.FastFood.Application.DTOs.Pedido.Request;
using Soat.Eleven.FastFood.Application.Interfaces;
using Soat.Eleven.FastFood.Application.Validators.Pedido;

namespace Soat.Eleven.FastFood.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly IPedidoService _pedidoService;

        public PedidoController(ILogger<PedidoController> logger, IPedidoService pedidoService)
        {
            _logger = logger;
            _pedidoService = pedidoService;
        }

        [HttpPost]
        public async Task<IActionResult> CriarPedido([FromBody] PedidoRequestDto pedidoDto)
        {
            if (pedidoDto == null)
            {
                return BadRequest("Pedido inválido.");
            }

            var validator = new PedidoRequestDtoValidator();
            var validationResult = validator.Validate(pedidoDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
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

        [HttpGet]
        public async Task<IActionResult> ListarPedidos()
        {
            try
            {
                var pedidos = await _pedidoService.ListarPedidos();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar pedidos.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPedidoPorId(Guid id)
        {
            try
            {
                var pedido = await _pedidoService.ObterPedidoPorId(id);
                if (pedido == null)
                    return NotFound();
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter pedido por id.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> AtualizarPedido(Guid id, [FromBody] PedidoRequestDto pedidoDto)
        {
            if (pedidoDto == null)
                return BadRequest("Pedido inválido.");

            var validator = new PedidoRequestDtoValidator();
            var validationResult = validator.Validate(pedidoDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            try
            {
                var pedidoAtualizado = await _pedidoService.AtualizarPedido(id, pedidoDto);
                return Ok(pedidoAtualizado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar pedido.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{id:guid}/pagar")]
        public async Task<IActionResult> PagarPedido(Guid id, [FromBody] PagamentoRequestDto pagamento)
        {
            try
            {
                var pagamentoProcessado = await _pedidoService.PagarPedido(id, pagamento);
                return Ok(pagamentoProcessado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao pagar o pedido");

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{id:guid}/iniciar-preparacao")]
        public async Task<IActionResult> IniciarPreparacaoPedido(Guid id)
        {
            try
            {
                await _pedidoService.IniciarPreparacaoPedido(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao iniciar preparação do pedido.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{id:guid}/finalizar-preparacao")]
        public async Task<IActionResult> FinalizarPreparacaoPedido(Guid id)
        {
            try
            {
                await _pedidoService.FinalizarPreparacaoPedido(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao finalizar preparação do pedido.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{id:guid}/finalizar")]
        public async Task<IActionResult> FinalizarPedido(Guid id)
        {
            try
            {
                await _pedidoService.FinalizarPedido(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao finalizar pedido.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{id:guid}/cancelar")]
        public async Task<IActionResult> CancelarPedido(Guid id)
        {
            try
            {
                await _pedidoService.CancelarPedido(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cancelar pedido.");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
