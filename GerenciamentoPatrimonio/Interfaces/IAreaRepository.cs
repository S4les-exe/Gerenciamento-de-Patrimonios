using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface IAreaRepository
    {
        List<Area> Listar();

        Area BuscarPorId(Guid areaId);
        Area BuscarPorNome(string nomeArea);
        void Adicionar(Area area);
        void Atualizar(Area area);
    }
}
