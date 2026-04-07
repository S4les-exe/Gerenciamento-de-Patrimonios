using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTOs.CargoDto;
using GerenciamentoPatrimonio.DTOs.UsuarioDto;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public ActionResult<List<ListarUsuarioDto>> Listar()
        {
            List<ListarUsuarioDto> usuarios = _service.Listar();
            return Ok(usuarios);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<ListarCargoDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarUsuarioDto usuario = _service.BuscarPorId(id);
                return Ok(usuario);
            }
            catch(DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Coordenador")]
        [HttpPost]  
        public ActionResult Adicionar(CriarUsuarioDto dto)
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

        [Authorize]
        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, CriarUsuarioDto dto)
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

        [Authorize(Roles = "Coordenador")]
        [HttpPatch("{id/status}")]
        public ActionResult AtualizarStatus(Guid id, AtualizarStatusUsuarioDto dto)
        {
            try
            {
                _service.AtualizarStatus(id, dto);
                return NoContent();
            }
            catch(DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
