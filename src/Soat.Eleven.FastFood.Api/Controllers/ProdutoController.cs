using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Application.DTOs.Produto;
using Soat.Eleven.FastFood.Application.Interfaces;

namespace Soat.Eleven.FastFood.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutos(
            [FromQuery] bool? incluirInativos = false,
            [FromQuery] Guid? categoriaId = null)
        {
            try
            {
                var produtos = await _produtoService.ListarProdutos(incluirInativos, categoriaId);
                return Ok(produtos);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoDTO>> GetProduto(Guid id)
        {
            var produto = await _produtoService.ObterProdutoPorId(id);
            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> PostProduto(ProdutoDTO produto)
        {
            try
            {
                var produtoCriado = await _produtoService.CriarProduto(produto);
                return CreatedAtAction(nameof(GetProduto), new { id = produtoCriado.Id }, produtoCriado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduto(Guid id, AtualizarProdutoDTO produto)
        {
            try
            {
                var produtoAtualizado = await _produtoService.AtualizarProduto(id, produto);
                return Ok(produtoAtualizado);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(Guid id)
        {
            try
            {
                await _produtoService.DesativarProduto(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{id}/reativar")]
        public async Task<IActionResult> ReativarProduto(Guid id)
        {
            try
            {
                await _produtoService.ReativarProduto(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
} 