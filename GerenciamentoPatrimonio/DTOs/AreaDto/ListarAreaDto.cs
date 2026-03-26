using Microsoft.AspNetCore.Server.HttpSys;

namespace GerenciamentoPatrimonio.DTOs.AreaDto
{
    public class ListarAreaDto
    {
        public Guid AreaID { get; set; }
        public string NomeArea { get; set; } = string.Empty;
    }
}
