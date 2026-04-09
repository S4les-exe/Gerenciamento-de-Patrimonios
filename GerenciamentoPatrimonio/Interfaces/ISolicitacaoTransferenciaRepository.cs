using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface ISolicitacaoTransferenciaRepository
    {
        List<SolicitacaoTransferencia> Listar();
        SolicitacaoTransferencia BuscarPorId(Guid transferenciaId);
        bool ExisteSolicitacaoPendente(Guid patrimonioId);
        bool UsuarioResponsavelDaLocalizacao(Guid UsuarioId, Guid localizacaoId);
        StatusTransferencia BuscarStatusTransferenciaPorNome(string nomeStatus);
        void Adicionar(SolicitacaoTransferencia solicitacaoTransferencia);
        bool LocalizacaoExiste(Guid localizacaoId);
        Patrimonio BuscarPatrimonioPorId(Guid patrimonioId);
        StatusPatrimonio BuscarStatusPatrimonioPorNome(string NomeStatus);
        TipoAlteracao BuscarTipoAlteracaoPorNome(string nomeTipo);
        void Atualizar(SolicitacaoTransferencia solicitacaoTransferencia);
        void AtualizarPatrimonio(Patrimonio patrimonio);
        void AdicionarLog(LogPatrimonio logPatrimonio);
    }
}
