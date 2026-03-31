using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTOs.UsuarioDto;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;   
        }

        public List<ListarUsuarioDto> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();

            List<ListarUsuarioDto> usuariosDto = usuarios.Select(usuario => new ListarUsuarioDto
            { 
                UsuarioID = usuario.UsuarioID,  
                NIF = usuario.NIF,
                Nome = usuario.Nome,    
                RG = usuario.RG,
                CPF = usuario.CPF,
                CarteiraTrabalho = usuario.CarteiraTrabalho,
                Email = usuario.Email,  
                Ativo = usuario.Ativo,
                PrimeiroAcesso = usuario.PrimeiroAcesso,
                EnderecoID = usuario.EnderecoID,    
                CargoID = usuario.CargoID,
                TipoUsuarioID = usuario.TipoUsuarioID
            }).ToList();

            return usuariosDto;
        }
    }
}
