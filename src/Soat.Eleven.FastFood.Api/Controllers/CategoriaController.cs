using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Application.DTOs.Categoria;
using Soat.Eleven.FastFood.Application.Interfaces;

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
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategorias()
        {
            var categorias = await _categoriaService.ListarCategorias(incluirInativos: true);
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaDTO>> GetCategoria(Guid id)
        {
            var categoria = await _categoriaService.ObterCategoriaPorId(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> PostCategoria(CategoriaDTO categoria)
        {
            var categoriaCriada = await _categoriaService.CriarCategoria(categoria);
            return CreatedAtAction(nameof(GetCategoria), new { id = categoriaCriada.Id }, categoriaCriada);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(Guid id, CategoriaDTO categoria)
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
    }
} 