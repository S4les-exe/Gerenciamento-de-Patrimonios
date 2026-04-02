namespace GerenciamentoPatrimonio.DTOs.CidadeDto
{
    public class ListarCidadeDto
    {
        public Guid CidadeID { get; set; }
        public string NomeCidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty ;
    }
}
