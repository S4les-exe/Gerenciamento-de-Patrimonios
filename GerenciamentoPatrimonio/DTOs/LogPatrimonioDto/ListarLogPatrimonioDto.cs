namespace GerenciamentoPatrimonio.DTOs.LogPatrimonioDto
{
    public class ListarLogPatrimonioDto
    {
        public Guid LogPatrimonioID {  get; set; }
        public DateTime DataTransferencia { get; set; }
        public Guid PatrimonioID { get; set; }
        public string DenominacaoPatrimonio { get; set; } = string.Empty;
        public string TipoAlteracao {  get; set; } = string.Empty;
        public string StatusPatrimonio { get; set; } = string.Empty;
        public string Usuario {  get; set; } = string.Empty;
        public string Localizacao {  get; set; } = string.Empty;
    }
}
