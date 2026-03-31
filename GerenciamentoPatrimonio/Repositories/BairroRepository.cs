using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Repositories
{
    public class BairroRepository : IBairroRepository
    {
        private readonly GerenciamentoSenaiDBContext _context;

        public BairroRepository(GerenciamentoSenaiDBContext context)
        {
            _context = context;
        }

        public List<Bairro> Listar()
        {
            return _context.Bairro.OrderBy(bairro => bairro.NomeBairro).ToList();
        }

        public Bairro BuscarPorId(Guid BairroId)
        {
            return _context.Bairro.Find(BairroId);
        }

        public Bairro BuscarPorNome(string nomeBairro, Guid bairroId)
        {
            return _context.Bairro.FirstOrDefault(bairro =>
            bairro.NomeBairro.ToLower() == nomeBairro.ToLower() &&
            bairro.BairroID == bairroId
            );
        }

        public bool CidadeExiste(Guid CidadeId)
        {
            return _context.Bairro.Any(bairro => bairro.CidadeID == CidadeId);
        }

        public void Adicionar(Bairro bairro)
        {
            _context.Bairro.Add(bairro);
            _context.SaveChanges();
        }

        public void Atualizar(Bairro bairro)
        {
            if (bairro == null)
            {
                return;
            }

            Bairro bairroBanco = _context.Bairro.Find(bairro.BairroID);

            if (bairro == null)
            {
                return;
            }

            bairroBanco.NomeBairro = bairro.NomeBairro;
            _context.SaveChanges();
        }
    }
}
