using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Core.Application.Portas.Inputs;
using Soat.Eleven.FastFood.Core.Domain.Contratos.Categoria;
using Soat.Eleven.FastFood.Domain.Enums;

namespace Soat.Eleven.FastFood.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ResumoCategoria>>> GetCategorias()
        {
            var categorias = await _categoriaService.ListarCategorias(incluirInativos: true);
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ResumoCategoria>> GetCategoria(Guid id)
        {
            var categoria = await _categoriaService.ObterCategoriaPorId(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);
        }

        [HttpPost]
        [Authorize(PolicyRole.Administrador)]
        public async Task<ActionResult<ResumoCategoria>> PostCategoria(ResumoCategoria categoria)
        {
            var categoriaCriada = await _categoriaService.CriarCategoria(categoria);
            return CreatedAtAction(nameof(GetCategoria), new { id = categoriaCriada.Id }, categoriaCriada);
        }

        [HttpPut("{id}")]
        [Authorize(PolicyRole.Administrador)]
        public async Task<IActionResult> PutCategoria(Guid id, ResumoCategoria categoria)
        {
            try
            {
                var categoriaAtualizada = await _categoriaService.AtualizarCategoria(id, categoria);
                return Ok(categoriaAtualizada);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(PolicyRole.Administrador)]
        public async Task<IActionResult> DeleteCategoria(Guid id)
        {
            try
            {
                await _categoriaService.DesativarCategoria(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("{id}/reativar")]
        [Authorize(PolicyRole.Administrador)]
        public async Task<IActionResult> ReativarCategoria(Guid id)
        {
            try
            {
                await _categoriaService.ReativarCategoria(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}