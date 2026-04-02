using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.DTOs.CargoDto
{
    public class ListarCargoDto
    {
        public Guid CargoID { get; set; }

        public string NomeCargo { get; set; } = null!;

        public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
    }
}
