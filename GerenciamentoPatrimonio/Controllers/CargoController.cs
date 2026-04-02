using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTOs.CargoDto;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly CargoService _service;

        public CargoController(CargoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult <List<ListarCargoDto>> Listar()
        {
            List<ListarCargoDto> cargos = _service.Listar();

            return Ok(cargos);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarCargoDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarCargoDto cargo = _service.BuscarPorId(id);

                return Ok(cargo);
            }
            catch(DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarCargoDto dto)
        {
            try
            {
                _service.Adicionar(dto);
                return Created();
            }
            catch(DomainException ex)
            {
                return BadRequest(ex.Message); 
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, CriarCargoDto dto)
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
