using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPatrimonio.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly GerenciamentoSenaiDBContext _context;

        public UsuarioRepository(GerenciamentoSenaiDBContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuario.OrderBy(usuario => usuario.Nome).ToList();
        }

        public Usuario BuscarPorId(Guid usuarioId)
        {
            return _context.Usuario.Find(usuarioId);
        }

        public Usuario BuscarDuplicado(string nif, string cpf, string email, Guid? usuarioId = null)
        {
            var consulta = _context.Usuario.AsQueryable();

            if(usuarioId.HasValue)
            {
                consulta = consulta.Where(usuario => usuario.UsuarioID == usuarioId.Value);
            }

            return consulta.FirstOrDefault(usuario =>
                usuario.NIF == nif ||
                usuario.CPF == cpf ||
                usuario.Email.ToLower() == email.ToLower()
                );
        }

        public bool EnderecoExiste(Guid enderecoId)
        {
            return _context.Endereco.Any(endereco => endereco.EnderecoID == enderecoId);
        }

        public bool CargoExiste(Guid cargoId)
        {
            return _context.Cargo.Any(cargo => cargo.CargoID == cargoId);
        }

        public bool TipoUsuarioExiste(Guid tipoUsuarioId)
        {
            return _context.TipoUsuario.Any(tipoUsuario => tipoUsuario.TipoUsuarioID == tipoUsuarioId);
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuario.Add(usuario);  
            _context.SaveChanges(); 
        }

        public void Atualizar(Usuario usuario)
        {
            if(usuario == null)
            {
                return; 
            }

            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioID);

            if(usuarioBanco == null)
            {
                return;
            }

            usuarioBanco.NIF = usuario.NIF;
            usuarioBanco.Nome = usuario.Nome;
            usuarioBanco.CPF = usuario.CPF; 
            usuarioBanco.CarteiraTrabalho = usuario.CarteiraTrabalho;
            usuarioBanco.Email = usuario.Email;
            usuarioBanco.EnderecoID = usuario.EnderecoID;
            usuarioBanco.CargoID = usuario.CargoID; 
            usuarioBanco.TipoUsuarioID = usuario.TipoUsuarioID;

            _context.SaveChanges();
        }

        public void AtualizarStatus(Usuario usuario)
        {
            if(usuario == null)
            {
                return; 
            }

            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioID);

            if( usuarioBanco == null)
            {
                return; 
            }

            usuarioBanco.Ativo = usuario.Ativo;
            _context.SaveChanges();
        }

        public Usuario ObterPorNIFComTipoUsuario(string nif)
        {
            return _context.Usuario.Include(usuario => usuario.TipoUsuario).FirstOrDefault(usuario => usuario.NIF == nif);
        }

        public void AtualizarSenha(Usuario usuario)
        {
            if(usuario == null)
            {
                return; 
            }

            Usuario usuarioBanco = _context.Usuario.Find(usuario.UsuarioID);

            if(usuarioBanco == null)
            {
                return;
            }
            
            usuarioBanco.Senha = usuario.Senha;
            _context.SaveChanges();
        }
    }
}
