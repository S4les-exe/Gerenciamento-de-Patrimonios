using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTOs.CargoDto;
using GerenciamentoPatrimonio.DTOs.UsuarioDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarUsuarioDto>> Listar()
        {
            List<ListarUsuarioDto> usuarios = _service.Listar();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarCargoDto> BuscarPorId(Guid id)
        {

        }
    }
}
