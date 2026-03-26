using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTOs.AreaDto;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens.Configuration;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly AreaService _service;

        public AreaController(AreaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarAreaDto>> Listar()
        {
            List<ListarAreaDto> areas = _service.Listar();
            return Ok(areas);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarAreaDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarAreaDto area = _service.BuscarPorId(id);

                return Ok(area);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarAreaDto dto)
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
        public ActionResult Atualizar(Guid id, CriarAreaDto dto)
        {
            try
            {
                _service.Atualizar(id, dto);
                return NoContent();
            }
            catch(DomainException ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
