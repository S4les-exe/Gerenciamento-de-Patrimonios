namespace GerenciamentoPatrimonio.DTOs.PatrimonioDto
{
    public class ListarPatrimonioDto
    {
        public Guid PatrimonioID { get; set; }  
        public string Denominacao { get; set; } = string.Empty;
        public string NumeroPatrimonio { get; set; } = string.Empty;
        public decimal? Valor {  get; set; }
        public string? Imagem {  get; set; } 
        public Guid LocalizacaoID { get; set; } 
        public Guid StatusPatrimonioID { get; set; }
    }
}
