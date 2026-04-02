namespace GerenciamentoPatrimonio.DTOs.EnderecoDto
{
    public class CriarEnderecoDto
    {
        public string Logradouro { get; set; } = string.Empty;  
        public int? Numero { get; set; } 
        public string? Complemento { get; set; }
        public string? CEP { get; set; }
        public Guid BairroID { get; set; }
    }
}
