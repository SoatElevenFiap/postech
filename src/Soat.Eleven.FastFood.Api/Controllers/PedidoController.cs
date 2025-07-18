using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Application.Validators.Pedido;
using Soat.Eleven.FastFood.Domain.UseCases;
using Soat.Eleven.FastFood.Core.Domain.Contratos.Pagamento;
using Soat.Eleven.FastFood.Core.Domain.Contratos.Pedido.Inputs;
using Soat.Eleven.FastFood.Domain.Enums;

namespace Soat.Eleven.FastFood.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly IPedidoUseCase _pedidoUseCase;

        public PedidoController(ILogger<PedidoController> logger, IPedidoUseCase pedidoUseCase)
        {
            _logger = logger;
            _pedidoUseCase = pedidoUseCase;
        }

        [HttpPost]
        [Authorize(PolicyRole.ClienteTotem)]
        public async Task<IActionResult> CriarPedido([FromBody] PedidoInput pedidoDto)
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
                var pedidoCriado = await _pedidoUseCase.CriarPedido(pedidoDto);
                return CreatedAtAction(nameof(CriarPedido), pedidoCriado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar pedido.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpGet]
        [Authorize(PolicyRole.Administrador)]
        public async Task<IActionResult> ListarPedidos()
        {
            try
            {
                var pedidos = await _pedidoUseCase.ListarPedidos();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar pedidos.");
                return StatusCode(500, "Erro interno do servidor.");
            }
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> ObterPedidoPorId(Guid id)
        {
            try
            {
                var pedido = await _pedidoUseCase.ObterPedidoPorId(id);
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
        [Authorize]
        public async Task<IActionResult> AtualizarPedido(Guid id, [FromBody] PedidoInput pedidoDto)
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
                var pedidoAtualizado = await _pedidoUseCase.AtualizarPedido(id, pedidoDto);
                return Ok(pedidoAtualizado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar pedido.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{id:guid}/pagar")]
        [Authorize(PolicyRole.ClienteTotem)]
        public async Task<IActionResult> PagarPedido(Guid id, [FromBody] SolicitacaoPagamento pagamento)
        {
            try
            {
                var pagamentoProcessado = await _pedidoUseCase.PagarPedido(id, pagamento);
                return Ok(pagamentoProcessado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao pagar o pedido");

                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{id:guid}/iniciar-preparacao")]
        [Authorize(PolicyRole.Administrador)]
        public async Task<IActionResult> IniciarPreparacaoPedido(Guid id)
        {
            try
            {
                await _pedidoUseCase.IniciarPreparacaoPedido(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao iniciar preparação do pedido.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{id:guid}/finalizar-preparacao")]
        [Authorize(PolicyRole.Administrador)]
        public async Task<IActionResult> FinalizarPreparacaoPedido(Guid id)
        {
            try
            {
                await _pedidoUseCase.FinalizarPreparacaoPedido(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao finalizar preparação do pedido.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{id:guid}/finalizar")]
        [Authorize(PolicyRole.Administrador)]
        public async Task<IActionResult> FinalizarPedido(Guid id)
        {
            try
            {
                await _pedidoUseCase.FinalizarPedido(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao finalizar pedido.");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("{id:guid}/cancelar")]
        [Authorize]
        public async Task<IActionResult> CancelarPedido(Guid id)
        {
            try
            {
                await _pedidoUseCase.CancelarPedido(id);
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
