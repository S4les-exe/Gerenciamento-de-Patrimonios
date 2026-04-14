namespace GerenciamentoPatrimonio.DTOs.PatrimonioDto
{
    public class ImportarPatrimonioCsvDto
    {
        public string NumeroPatrimonio { get; set; } = string.Empty;
        public string Denominacao {  get; set; } = string.Empty;
        public string? DataIncorporacao {  get; set; } 
        public string? ValorAquisicao {  get; set; }
    }
}
