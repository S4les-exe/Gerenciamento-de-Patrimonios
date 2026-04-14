using GerenciamentoPatrimonio.DTOs.SolicitacaoTransferenciaDto;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacaoTransferenciaController : ControllerBase
    {
        private readonly SolicitacaoTransferenciaController _service;

        public SolicitacaoTransferenciaController(SolicitacaoTransferenciaController service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<ListarSolicitacaoTransferenciaDto>> Listar()
        {
            List<ListarSolicitacaoTransferenciaDto> solicitacoes = _service.Listar();
            return Ok(solicitacoes);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<ListarSolicitacaoTransferenciaDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarSolicitacaoTransferenciaDto solicitacao = _service.BuscarPorId(id);
                return Ok(solicitacao);
            }
            catch(DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Adicionar(CriarSolicitacaoTransferenciaDto dto)
        {
            try
            {
                string usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if(string.IsNullOrWhiteSpace(usuarioIdClaim))
                {
                    return Unauthorized("Usuario não autenticado");
                }

                Guid usuarioId = Guid.Parse(usuarioIdClaim);

                _service.Adicionar(usuarioId, dto);
                return Created();
            }
            catch(DomainException ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        [Authorize]
        [HttpPatch("{id}/responder")]
        public ActionResult Responder(Guid id, ResponderSolicitacaoTransferenciaDto dto)
        {
            try
            {
                string usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrWhiteSpace(usuarioIdClaim))
                {
                    return Unauthorized("Usuario não autenticado");
                }

                Guid usuarioId = Guid.Parse(usuarioIdClaim);

                _service.Responder(id, usuarioId, dto);
                return NoContent();
            }
            catch(DomainException ex)
            {
                return BadRequest(ex.Message); 
            }
        }
    }
}
