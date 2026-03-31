using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> Listar();
    }
}
