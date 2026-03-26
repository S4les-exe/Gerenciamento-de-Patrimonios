using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Repositories
{
    public class LocalizacaoRepository : ILocalizacaoRepository
    {
        private readonly GerenciamentoSenaiDBContext _context;

        public LocalizacaoRepository(GerenciamentoSenaiDBContext context)
        {
            _context = context;
        }

        public List<Localizacao> Listar()
        {
            return _context.Localizacao
                .OrderBy(localizacao => localizacao.NomeLocal).ToList();
        }

        public Localizacao BuscarPorId(Guid localizacaoId)
        {
            return _context.Localizacao.Find(localizacaoId);
        }

        public void Adicionar(Localizacao localizacao)
        {
            _context.Localizacao.Add(localizacao);
            _context.SaveChanges();
        }

        public bool AreaExiste(Guid areaId)
        {
            return _context.Area.Any(area => area.AreaID == areaId);
        }

        public void Atualizar(Localizacao localizacao)
        {
            if(localizacao == null)
            {
                return;
            }

            Localizacao localizacaoBanco = _context.Localizacao.Find(localizacao.LocalizacaoID);

            if(localizacaoBanco == null)
            {
                return;
            }

            localizacaoBanco.NomeLocal = localizacao.NomeLocal;
            localizacaoBanco.LocalSAP = localizacao.LocalSAP;
            localizacaoBanco.DescricaoSAP = localizacao.DescricaoSAP;
            localizacaoBanco.AreaID = localizacao.AreaID;
        }
    }
}
