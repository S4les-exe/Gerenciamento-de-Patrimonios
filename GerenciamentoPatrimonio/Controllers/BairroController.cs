using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTOs.BairroDto;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BairroController : ControllerBase
    {
        private readonly BairroService _service;

        public BairroController(BairroService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarBairroDto>> Listar()
        {
            return Ok(_service.Listar());
        }

        [HttpGet("{id}")]
        public ActionResult<ListarBairroDto> BuscarPorId(Guid id)
        {
            try
            {
                return Ok(_service.BuscarPorId(id));
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarBairroDto dto)
        {
            try
            {
                _service.Adicionar(dto);
                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, CriarBairroDto dto)
        {
            try
            {
                _service.Atualizar(id, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
