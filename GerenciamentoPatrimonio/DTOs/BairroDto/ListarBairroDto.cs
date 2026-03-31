namespace GerenciamentoPatrimonio.DTOs.BairroDto
{
    public class ListarBairroDto
    {
        public Guid BairroID { get; set; }
        public string NomeBairro { get; set; }
        public Guid CidadeID { get; set; }
    }
}
