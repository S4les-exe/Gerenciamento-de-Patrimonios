using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface IPatrimonioRepository
    {
        List<Patrimonio> Listar();
        Patrimonio BuscarPorId(Guid patrimonioId);

        // fazer esse com AsQueryable igual foi feito no endereço
        Patrimonio BuscarPorNumeroPatrimonio(string numeroPatrimonio, Guid? patrimonioId = null);

        bool LocalizacaoExiste(Guid localizacaoId);
        bool TipoPatrimonioExiste(Guid tipoPatrimonioId);
        bool StatusPatrimonioExiste(Guid statusPatrimonioId);

        void Adicionar(Patrimonio patrimonio);
        void Atualizar(Patrimonio patrimonio);
        void AtualizarStatus(Patrimonio patrimonio);
    }
}
