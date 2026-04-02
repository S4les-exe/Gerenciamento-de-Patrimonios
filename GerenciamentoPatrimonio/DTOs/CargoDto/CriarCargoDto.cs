using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.DTOs.CargoDto
{
    public class CriarCargoDto
    {
        public string NomeCargo { get; set; } = null!;

        public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
    }
}
