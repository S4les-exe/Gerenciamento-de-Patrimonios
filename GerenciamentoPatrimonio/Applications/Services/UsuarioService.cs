using GerenciamentoPatrimonio.Applications.Autenticacao;
using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTOs.UsuarioDto;
using GerenciamentoPatrimonio.Exceptions;
using GerenciamentoPatrimonio.Interfaces;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Identity.Client;

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

        public ListarUsuarioDto BuscarPorId(Guid usuarioId)
        {
            Usuario usuario = _repository.BuscarPorId(usuarioId);

            if(usuario == null)
            {
                throw new DomainException("Usuario não encontrado");
            }

            ListarUsuarioDto usuarioDto = new ListarUsuarioDto
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
            };

            return usuarioDto;
        }

        public void Adicionar(CriarUsuarioDto dto)
        {
            Validar.ValidarNome(dto.Nome);
            Validar.ValidarNIF(dto.NIF);
            Validar.ValidarCPF(dto.CPF);
            Validar.ValidarEmail(dto.Email);    

            Usuario usuarioDuplicado = _repository.BuscarDuplicado(dto.NIF, dto.CPF, dto.Email);

            if(usuarioDuplicado != null)
            {
                if(usuarioDuplicado.NIF == dto.NIF)
                {
                    throw new DomainException("Ja existe um usuario cadastrado com esse NIF.");
                }

                if(usuarioDuplicado.CPF == dto.CPF)
                {
                    throw new DomainException("Ja existe um usuario cadastrado com esse CPF.");
                }

                if(usuarioDuplicado.Email.ToLower() == dto.Email.ToLower())
                {
                    throw new DomainException("Ja existe um usuario com esse email.");
                }

                if(!_repository.EnderecoExiste(dto.EnderecoID))
                {
                    throw new DomainException("Endereco informado não existe.");
                }

                if(!_repository.CargoExiste(dto.CargoID))
                {
                    throw new DomainException("Cargo informado não existe.");
                }

                if(!_repository.TipoUsuarioExiste(dto.TipoUsuarioID))
                {
                    throw new DomainException("Tipo de usuario informado não existe.");
                }

                Usuario usuario = new Usuario
                { 
                    NIF = dto.NIF,
                    CPF = dto.CPF,  
                    RG = dto.RG,
                    CarteiraTrabalho = dto.CarteiraTrabalho,
                    Senha = CriptografiaUsuario.CriptografarSenha(dto.NIF),
                    Email = dto.Email,
                    Ativo = true,
                    PrimeiroAcesso = true,
                    CargoID = dto.CargoID,
                    TipoUsuarioID = dto.TipoUsuarioID,
                };

                _repository.Adicionar(usuario); 
            }
        }
        
        public void Atualizar (Guid usuarioId, CriarUsuarioDto dto)
        {
            Validar.ValidarNome(dto.Nome);
            Validar.ValidarNIF(dto.NIF);
            Validar.ValidarCPF(dto.CPF);
            Validar.ValidarEmail(dto.Email);

            Usuario usuarioBanco = _repository.BuscarPorId(usuarioId);

            if(usuarioBanco == null)
            {
                throw new DomainException("Usuario não encontrado.");
            }

            Usuario usuarioDuplicado = _repository.BuscarDuplicado(dto.NIF, dto.CPF, dto.Email, usuarioId);

            if(usuarioDuplicado != null)
            {
                if(usuarioDuplicado.NIF == dto.NIF)
                {
                    throw new DomainException("Ja existe um usuario cadastrado com esse NIF.");
                }

                if(usuarioDuplicado.CPF == dto.CPF)
                {
                    throw new DomainException("Ja existe um usuario cadastrado com esse CPF.");
                }

                if(usuarioDuplicado.Email.ToLower() == dto.Email.ToLower())
                {
                    throw new DomainException("Ja existe um usuario cadastrado com esse email.");
                }
            }

            if(!_repository.EnderecoExiste(dto.EnderecoID))
            {
                throw new DomainException("Endereco informado nao existe.");
            }

            if(!_repository.CargoExiste(dto.CargoID))
            {
                throw new DomainException("Cargo informado não existe.");
            }

            if(!_repository.TipoUsuarioExiste(dto.TipoUsuarioID))
            {
                throw new DomainException("Tipo de usuario informado não existe.");
            }

            usuarioBanco.NIF = dto.NIF;
            usuarioBanco.Nome = dto.Nome;
            usuarioBanco.CPF = dto.CPF;
            usuarioBanco.CarteiraTrabalho = dto.CarteiraTrabalho;
            usuarioBanco.Email = dto.Email;
            usuarioBanco.EnderecoID = dto.EnderecoID;
            usuarioBanco.CargoID = dto.CargoID;
            usuarioBanco.TipoUsuarioID = dto.TipoUsuarioID;

            _repository.Atualizar(usuarioBanco);
        }

        public void AtualizarStatus(Guid usuarioId, AtualizarStatusUsuarioDto dto)
        {
            Usuario usuarioBanco = _repository.BuscarPorId(usuarioId);

            if(usuarioBanco == null)
            {
                throw new DomainException("Usuario nao encontrado.");
            }

            usuarioBanco.Ativo = dto.Ativo;
            _repository.AtualizarStatus(usuarioBanco);
        }
    }
}
