using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface IPatrimonioRepository
    {
        List<Patrimonio> Listar();
        Patrimonio BuscarPorId(Guid patrimonioId);

        // fazer esse com AsQueryable igual foi feito no endereço
        bool BuscarPorNumeroPatrimonio(string numeroPatrimonio);

        bool LocalizacaoExiste(Guid localizacaoId);
        bool StatusPatrimonioExiste(Guid statusPatrimonioId);

        void Adicionar(Patrimonio patrimonio);
        void AtualizarStatus(Patrimonio patrimonio);
        void AdicionarLog(LogPatrimonio logPatrimonio);

        Localizacao BuscarLocalizacaoPorNome(string nomeLocalizacao);
        StatusPatrimonio BuscarStatusPatrimonioPorNome(string nomeStatus);
        TipoAlteracao BuscarTipoAlteracaoPorNome(string nomeTipo);
    }
}
