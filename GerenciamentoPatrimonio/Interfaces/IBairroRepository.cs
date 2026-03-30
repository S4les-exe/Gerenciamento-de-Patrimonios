using GerenciamentoPatrimonio.Domains;

namespace GerenciamentoPatrimonio.Interfaces
{
    public interface IBairroRepository
    {
        List<Bairro> Listar();
        Bairro BuscarPorId(Guid bairroId);
        void Adicionar(Bairro bairro);
        void Atualizar(Bairro bairro);
        Bairro BuscarPorNome(string nomeBairro, Guid bairroId);
        bool CidadeExiste(Guid cidadeId);
    }
}
