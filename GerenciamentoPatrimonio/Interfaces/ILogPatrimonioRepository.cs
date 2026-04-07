using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface ILogPatrimonioRepository
    {
        List<LogPatrimonio> Listar();
        List<LogPatrimonio> BuscarPorPatrimonio(Guid patrimonioId);
    }
}
