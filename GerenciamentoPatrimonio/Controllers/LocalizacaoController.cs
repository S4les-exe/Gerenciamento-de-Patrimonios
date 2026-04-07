using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTOs.LocalizacaoDto;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizacaoController : ControllerBase
    {
        private readonly LocalizacaoService _service;

        public LocalizacaoController(LocalizacaoService service)
        {
            _service = service; 
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<ListarLocalizacaoDto>> Listar()
        {
            List<ListarLocalizacaoDto> localizacoes = _service.Listar();
            return Ok(localizacoes);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<ListarLocalizacaoDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarLocalizacaoDto localizacao = _service.BuscarPorId(id);
                return Ok(localizacao);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Coordenador")]
        [HttpPost]
        public ActionResult Adicionar(CriarLocalizacaoDto dto)
        {
            try
            {
                _service.Adicionar(dto);
                return Ok();    
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Coordenador")]
        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, CriarLocalizacaoDto dto)
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
