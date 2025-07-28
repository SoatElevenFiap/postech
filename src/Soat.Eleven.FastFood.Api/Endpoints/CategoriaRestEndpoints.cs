using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Application.Controllers;
using Soat.Eleven.FastFood.Common.Interfaces.DataSources;
using Soat.Eleven.FastFood.Core.DTOs.Categorias;
using Soat.Eleven.FastFood.Core.Enums;

namespace Soat.Eleven.FastFood.Api.Controllers
{
    [ApiController]
    [Route("api/Categoria")]
    public class CategoriaRestEndpoints : ControllerBase
    {
        private readonly ICategoriaProdutoDataSource _categoriaDataSource;

        public CategoriaRestEndpoints(ICategoriaProdutoDataSource categoriaDataSource)
        {
            _categoriaDataSource = categoriaDataSource;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CategoriaProdutoDto>>> GetCategorias()
        {
            var controller = new CategoriaController(_categoriaDataSource);
            var categorias = await controller.ListarCategorias(incluirInativos: true);
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CategoriaProdutoDto>> GetCategoria(Guid id)
        {
            var controller = new CategoriaController(_categoriaDataSource);
            var categoria = await controller.GetCategoriaPorId(id);

            return Ok(categoria);
        }

        [HttpPost]
        [Authorize(PolicyRole.Administrador)]
        public async Task<ActionResult<CategoriaProdutoDto>> PostCategoria(CriarCategoriaDto categoria)
        {
            var controller = new CategoriaController(_categoriaDataSource);
            var categoriaCriada = await controller.CriarCategoria(categoria);
            return CreatedAtAction(nameof(PostCategoria), new { id = categoriaCriada.Id }, categoriaCriada);
        }

        [HttpPut("{id}")]
        [Authorize(PolicyRole.Administrador)]
        public async Task<IActionResult> PutCategoria(Guid id, AtualizarCategoriaDto categoria)
        {
            try
            {
                var controller = new CategoriaController(_categoriaDataSource);
                var categoriaAtualizada = await controller.AtualizarCategoria(id, categoria);
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
                var controller = new CategoriaController(_categoriaDataSource);
                await controller.DesativarCategoria(id);
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
                var controller = new CategoriaController(_categoriaDataSource);
                await controller.ReativarCategoria(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}