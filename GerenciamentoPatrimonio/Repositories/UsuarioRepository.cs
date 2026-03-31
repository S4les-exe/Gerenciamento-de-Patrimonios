using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

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
    }
}
