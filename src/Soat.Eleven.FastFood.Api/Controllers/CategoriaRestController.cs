using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Soat.Eleven.FastFood.Core.Controllers;
using Soat.Eleven.FastFood.Core.DTOs.Categorias;
using Soat.Eleven.FastFood.Core.Interfaces.Gateways;
using Soat.Eleven.FastFood.Domain.Enums;

namespace Soat.Eleven.FastFood.Api.Controllers
{
    [ApiController]
    [Route("api/Categoria")]
    public class CategoriaRestController : ControllerBase
    {
        private readonly ICategoriaGateway _categoriaGateway;

        public CategoriaRestController(ICategoriaGateway categoriaGateway)
        {
            _categoriaGateway = categoriaGateway;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ResumoCategoriaDto>>> GetCategorias()
        {
            var controller = new CategoriaController(_categoriaGateway);
            var categorias = await controller.ListarCategorias(incluirInativos: true);
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ResumoCategoriaDto>> GetCategoria(Guid id)
        {
            var controller = new CategoriaController(_categoriaGateway);
            var categoria = await controller.GetCategoriaPorId(id);

            return Ok(categoria);
        }

        [HttpPost]
        [Authorize(PolicyRole.Administrador)]
        public async Task<ActionResult<ResumoCategoriaDto>> PostCategoria(CriarCategoriaDto categoria)
        {
            var controller = new CategoriaController(_categoriaGateway);
            var categoriaCriada = await controller.CriarCategoria(categoria);
            return CreatedAtAction(nameof(PostCategoria), new { id = categoriaCriada.Id }, categoriaCriada);
        }

        [HttpPut("{id}")]
        [Authorize(PolicyRole.Administrador)]
        public async Task<IActionResult> PutCategoria(Guid id, AtualizarCategoriaDto categoria)
        {
            try
            {
                categoria.Id = id;
                var controller = new CategoriaController(_categoriaGateway);
                var categoriaAtualizada = await controller.AtualizarCategoria(categoria);
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
                var controller = new CategoriaController(_categoriaGateway);
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
                var controller = new CategoriaController(_categoriaGateway);
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