using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Core.Application.Portas.Inputs;
using Soat.Eleven.FastFood.Core.Domain.Contratos.Produto;
using Soat.Eleven.FastFood.Domain.Enums;

namespace Soat.Eleven.FastFood.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly ILogger<ProdutoController> _logger;

        public ProdutoController(IProdutoService produtoService, ILogger<ProdutoController> logger)
        {
            _produtoService = produtoService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
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
        [Authorize]
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
        [Authorize(PolicyRole.Administrador)]
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
        [Authorize(PolicyRole.Administrador)]
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
        [Authorize(PolicyRole.Administrador)]
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
        [Authorize(PolicyRole.Administrador)]
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

        [HttpPost("{id}/imagem")]
        public async Task<IActionResult> UploadImagem(Guid id, [FromForm] IFormFile imagem)
        {
            try
            {
                if (imagem == null || imagem.Length == 0)
                    return BadRequest(new { mensagem = "Nenhuma imagem enviada." });

                var IMAGE_MAX_SIZE = 2 * 1024 * 1024; // 2MB
                if (imagem.Length > IMAGE_MAX_SIZE)
                    return BadRequest(new { mensagem = "A imagem deve ter no máximo 2MB." });

                var imagemDto = new ImagemProduto
                {
                    Nome = imagem.FileName,
                    ContentType = imagem.ContentType,
                    Conteudo = imagem.OpenReadStream()
                };

                await _produtoService.UploadImagemAsync(id, imagemDto);
                return Ok(new { mensagem = "Imagem de produto alterada com sucesso." });
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Erro ao fazer upload da imagem para o produto {Id}", id);
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpDelete("{id}/imagem")]
        public async Task<IActionResult> RemoverImagem(Guid id)
        {
            await _produtoService.RemoverImagemAsync(id);
            return NoContent();
        }
    }
} 