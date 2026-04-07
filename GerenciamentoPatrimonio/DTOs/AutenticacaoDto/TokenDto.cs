namespace GerenciamentoPatrimonio.DTOs.AutenticacaoDto
{
    public class TokenDto
    {
        public string Token { get; set; } = string.Empty;
        public bool PrimeiroAcesso { get; set; }
        public string TipoUsuario { get; set; } = string.Empty;
    }
}
