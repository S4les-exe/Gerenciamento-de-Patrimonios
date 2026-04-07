using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoPatrimonio.Repositories
{
    public class LogPatrimonioRepository : ILogPatrimonioRepository
    {
        private readonly GerenciamentoSenaiDBContext _context;

        public LogPatrimonioRepository(GerenciamentoSenaiDBContext context)
        {
            _context = context;
        }

        public List<LogPatrimonio> Listar()
        {
            return _context.LogPatrimonio
                .Include(log => log.Usuario)
                .Include(log => log.Localizacao)
                .Include(log => log.TipoAlteracao)
                .Include(log => log.StatusPatrimonio)
                .Include(log => log.Patrimonio)
                .OrderByDescending(log => log.DataTransferencia)
                .ToList();
        }

        public List<LogPatrimonio> BuscarPorPatrimonio(Guid patrimonioId)
        {
            return _context.LogPatrimonio
                .Include(log => log.Usuario)
                .Include(log => log.Localizacao)
                .Include(log => log.TipoAlteracao)
                .Include(log => log.StatusPatrimonio)
                .Include(log => log.Patrimonio)
                .Where(log => log.PatrimonioID == patrimonioId)
                .OrderByDescending(log => log.DataTransferencia)
                .ToList();
        }
    }
}
