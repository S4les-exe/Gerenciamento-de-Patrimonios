namespace GerenciamentoPatrimonio.DTOs.LocalizacaoDto
{
    public class CriarLocalizacaoDto
    {
        public string NomeLocal {  get; set; } = string.Empty;
        public int LocalSAP { get; set; }
        public string DescricaoSAP { get; set; }
        public Guid AreaID { get; set; }
    }
}
