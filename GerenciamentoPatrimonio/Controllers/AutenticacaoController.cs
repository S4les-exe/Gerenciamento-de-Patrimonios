using GerenciamentoPatrimonio.Applications.Services;
using GerenciamentoPatrimonio.DTOs.AutenticacaoDto;
using GerenciamentoPatrimonio.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GerenciamentoPatrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly AutenticacaoService _service;

        public AutenticacaoController(AutenticacaoService service)
        {
            _service = service; 
        }

        [HttpPost("login")]
        public ActionResult<TokenDto> Login(LoginDto loginDto)
        {
            try
            {
                TokenDto token = _service.Login(loginDto);
                return Ok(token);
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPatch("trocar-senha")]
        public ActionResult TrocarPrimeiraSenha(TrocarPrimeiraSenhaDto dto)
        {
            try
            {
                string usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if(string.IsNullOrWhiteSpace(usuarioIdClaim))
                {
                    return Unauthorized("Usuario não autenticado.");
                }

                //convertendo string para GUID 
                Guid usuarioId = Guid.Parse(usuarioIdClaim);

                _service.TrocarPrimeiraSenha(usuarioId, dto);

                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message); 
            }
        }
    }
}
