using GerenciamentoPatrimonio.Domains;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> Listar();

        Usuario BuscarPorId(Guid id);

        Usuario BuscarDuplicado(string nif, string cpf, string email, Guid? usuarioId = null);
        bool EnderecoExiste(Guid enderecoId);
        bool CargoExiste(Guid cargoId);
        bool TipoUsuarioExiste(Guid tipoUsuarioId);
        void Adicionar(Usuario usuario);
        void Atualizar(Usuario usuario);
        void AtualizarStatus(Usuario usuario);
        Usuario ObterPorNIFComTipoUsuario(string nif);
        void AtualizarSenha(Usuario usuario);
        void AtualizarPrimeiroAcesso(Usuario usuario);
    }
}
