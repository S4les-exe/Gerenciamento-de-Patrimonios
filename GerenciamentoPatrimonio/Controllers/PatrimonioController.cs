using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTOs.PatrimonioDto;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatrimonioController : ControllerBase
    {
        private readonly PatrimonioService _service;

        public PatrimonioController(PatrimonioService service)
        {
            _service = service; 
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<ListarPatrimonioDto>> Listar()
        {
            List<ListarPatrimonioDto> patrimonios = _service.Listar();
            return Ok(patrimonios);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<ListarPatrimonioDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarPatrimonioDto patrimonio = _service.BuscarPorId(id);
                return Ok(patrimonio);  
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
