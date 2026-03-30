using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface ILocalizacaoRepository
    {
        List<Localizacao> Listar();
        Localizacao BuscarPorId(Guid localizacaoId);
        void Adicionar(Localizacao localizacao);
        bool AreaExiste(Guid areaId);
        void Atualizar(Localizacao localizacao);
        Localizacao BuscarPorNome(string nomeLocal, Guid areaId);
    }
}
