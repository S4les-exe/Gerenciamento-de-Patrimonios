using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Repositories
{
    public class SolicitacaoTransferenciaRepository : ISolicitacaoTransferenciaRepository
    {
        private readonly GerenciamentoSenaiDBContext _context;

        public SolicitacaoTransferenciaRepository(GerenciamentoSenaiDBContext context)
        {
            _context = context;
        }

        public List<SolicitacaoTransferencia> Listar()
        {
            return _context.SolicitacaoTransferencia.OrderByDescending(solicitacao => solicitacao.DataCriacaoSolicitante).ToList();
        }

        public SolicitacaoTransferencia BuscarPorId(Guid transferenciaId)
        {
            return _context.SolicitacaoTransferencia.Find(transferenciaId);
        }

        public StatusTransferencia BuscarStatusTransferenciaPorNome(string nomeStatus)
        {
            return _context.StatusTransferencia.FirstOrDefault(status => status.NomeStatus.ToLower() == nomeStatus.ToLower());
        }

        public bool ExisteSolicitacaoPendente(Guid patrimonioId)
        {
            StatusTransferencia statusPendente = BuscarStatusTransferenciaPorNome("Pendente de aprovação");

            if(statusPendente == null)
            {
                return false;
            }

            return _context.SolicitacaoTransferencia.Any(solicitacao => 
                solicitacao.PatrimonioID == patrimonioId &&
                solicitacao.StatusTransferenciaID == statusPendente.StatusTransferenciaID
            );
        }

        public bool UsuarioResponsavelDaLocalizacao(Guid usuarioId, Guid localizacaoId)
        {
            return _context.Usuario.Any(usuario =>
                usuario.UsuarioID == usuarioId &&
                usuario.Localizacao.Any(localizacao => localizacao.LocalizacaoID == localizacaoId)
                );
        }

        public void Adicionar(SolicitacaoTransferencia solicitacaoTransferencia)
        {
            _context.SolicitacaoTransferencia.Add(solicitacaoTransferencia);
            _context.SaveChanges();
        }

        public bool LocalizacaoExiste(Guid localizacaoId)
        {
            return _context.Localizacao.Any(localizacao => localizacao.LocalizacaoID == localizacaoId);
        }

        public Patrimonio BuscarPatrimonioPorId(Guid patrimonioId)
        {
            return _context.Patrimonio.Find(patrimonioId);
        }

        public StatusPatrimonio BuscarStatusPatrimonioPorNome(string nomeStatus)
        {
            return _context.StatusPatrimonio.FirstOrDefault(status => status.NomeStatus.ToLower() == nomeStatus.ToLower());
        }

        public TipoAlteracao BuscarTipoAlteracaoPorNome(string nomeTipo)
        {
            return _context.TipoAlteracao.FirstOrDefault(tipo => tipo.NomeTipo.ToLower() == nomeTipo.ToLower());
        }

        public void Atualizar(SolicitacaoTransferencia solicitacaoTransferencia)
        {
            if(solicitacaoTransferencia == null)
            {
                return;
            }

            SolicitacaoTransferencia solicitacaoBanco = _context.SolicitacaoTransferencia.Find(solicitacaoTransferencia.TransferenciaID);

            if(solicitacaoBanco == null)
            {
                return;
            }

            solicitacaoBanco.DataResposta = solicitacaoTransferencia.DataResposta;
            solicitacaoBanco.StatusTransferenciaID = solicitacaoTransferencia.StatusTransferenciaID;
            solicitacaoBanco.UsuarioIDAprovacao = solicitacaoTransferencia.UsuarioIDAprovacao;

            _context.SaveChanges();
        }

        public void AtualizarPatrimonio(Patrimonio  patrimonio)
        {
            if(patrimonio == null)
            {
                return; 
            }

            Patrimonio patrimonioBanco = _context.Patrimonio.Find(patrimonio.PatrimonioID);

            if(patrimonioBanco == null)
            {
                return; 
            }

            patrimonioBanco.LocalizacaoID = patrimonio.LocalizacaoID;
            patrimonioBanco.StatusPatrimonioID = patrimonio.StatusPatrimonioID;

            _context.SaveChanges();
        }

        public void AdicionarLog(LogPatrimonio logPatrimonio)
        {
            _context.LogPatrimonio.Add(logPatrimonio);
            _context.SaveChanges();
        }
    }
}
