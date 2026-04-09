using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Repositories
{
    public class StatusPatrimonioRepository : IStatusPatrimonioRepository
    {
        private readonly GerenciamentoSenaiDBContext _context;

        public StatusPatrimonioRepository(GerenciamentoSenaiDBContext context)
        {
            _context = context;
        }

        public List<StatusPatrimonio> Listar()
        {
            return _context.StatusPatrimonio
                .OrderBy(status => status.NomeStatus)
                .ToList();
        }

        public StatusPatrimonio BuscarPorId(Guid statusPatrimonioId)
        {
            return _context.StatusPatrimonio.Find(statusPatrimonioId);
        }

        public StatusPatrimonio BuscarPorNome(string nomeStatus)
        {
            return _context.StatusPatrimonio.FirstOrDefault(status => status.NomeStatus.ToLower() == nomeStatus.ToLower());
        }

        public void Adicionar(StatusPatrimonio statusPatrimonio)
        {
            _context.StatusPatrimonio.Add(statusPatrimonio);
            _context.SaveChanges();
        }

        public void Atualizar(StatusPatrimonio statusPatrimonio)
        {
            if (statusPatrimonio == null)
            {
                return;
            }

            StatusPatrimonio statusBanco = _context.StatusPatrimonio.Find(statusPatrimonio.StatusPatrimonioID);

            if (statusBanco == null)
            {
                return;
            }

            statusBanco.NomeStatus = statusPatrimonio.NomeStatus;

            _context.SaveChanges();
        }
}
