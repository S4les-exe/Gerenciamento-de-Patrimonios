namespace GerenciamentoPatrimonio.DTOs.SolicitacaoTransferenciaDto
{
    public class CriarSolicitacaoTransferenciaDto
    {
        public string Justificativa { get; set; } = string.Empty;
        public Guid PatrimonioID { get; set; }
        public Guid LocalizacaoID { get; set; }
    }
}
