using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTOs.SolicitacaoTransferenciaDto;
using GerenciamentoPatrimonio.Exceptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class SolicitacaoTransferenciaService
    {
        private readonly ISolicitacaoTransferenciaRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;

        public SolicitacaoTransferenciaService(ISolicitacaoTransferenciaRepository repository, IUsuarioRepository usuarioRepository)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
        }

        public List<ListarSolicitacaoTransferenciaDto> Listar()
        {
            List<SolicitacaoTransferencia> solicitacoes = _repository.Listar();

            List<ListarSolicitacaoTransferenciaDto> solicitacoesDto = solicitacoes.Select(solicitacao => new ListarSolicitacaoTransferenciaDto
            { 
                TransferenciaID = solicitacao.TransferenciaID,
                DataCriacaoSolicitante = solicitacao.DataCriacaoSolicitante,
                DataResposta = solicitacao.DataResposta,
                Justificativa = solicitacao.Justificativa,
                StatusTransferenciaID = solicitacao.StatusTransferenciaID,
                UsuarioIDSolicitacao = solicitacao.UsuarioIDSolicitacao,
                UsuarioIDAprovacao = solicitacao.UsuarioIDAprovacao,
                PatrimonioID = solicitacao.PatrimonioID,
                LocalizacaoID = solicitacao.LocalizacaoID
            }).ToList();

            return solicitacoesDto;
        }
        
        public ListarSolicitacaoTransferenciaDto BuscarPorId(Guid transferenciaId)
        {
            SolicitacaoTransferencia solicitacao = _repository.BuscarPorId(transferenciaId);

            if(solicitacao == null)
            {
                throw new DomainException("Solicitacao de Transferencia não encontrada.");
            }

            ListarSolicitacaoTransferenciaDto solicitacaoDto = new ListarSolicitacaoTransferenciaDto
            {
                TransferenciaID = solicitacao.TransferenciaID,
                DataCriacaoSolicitante = solicitacao.DataCriacaoSolicitante,
                DataResposta = solicitacao.DataResposta,
                Justificativa = solicitacao.Justificativa,
                StatusTransferenciaID = solicitacao.StatusTransferenciaID,
                UsuarioIDSolicitacao = solicitacao.UsuarioIDSolicitacao,
                UsuarioIDAprovacao = solicitacao.UsuarioIDAprovacao,
                PatrimonioID = solicitacao.PatrimonioID,
                LocalizacaoID = solicitacao.LocalizacaoID
            };

            return solicitacaoDto;
        }

        public void Adicionar(Guid usuarioID, CriarSolicitacaoTransferenciaDto dto)
        {
            Validar.ValidarJustificativa(dto.Justificativa);

            Usuario usuario = _usuarioRepository.BuscarPorId(usuarioID);

            if(usuario == null)
            {
                throw new DomainException("Usuario não encontrado");
            }

            Patrimonio patrimonio = _repository.BuscarPatrimonioPorId(dto.PatrimonioID);

            if (patrimonio == null)
            {
                throw new DomainException("Patrimonio não encontrado.");
            }

            if(!_repository.LocalizacaoExiste(dto.LocalizacaoID))
            {
                throw new DomainException("Localizacao de destino não existe.");
            }

            if(patrimonio.LocalizacaoID == dto.LocalizacaoID)
            {
                throw new DomainException("O patrimonio ja esta nessa localizacao");
            }

            if(!_repository.ExisteSolicitacaoPendente(dto.PatrimonioID))
            {
                throw new DomainException("Ja existe uma solicitação pendente para esse patrimonio.");
            }
            
            if(usuario.TipoUsuario.NomeTipo == "Responsavel")
            {
                bool usuarioResponsavel = _repository.UsuarioResponsavelDaLocalizacao(UsuarioId, patrimonio.LocalizacaoID);

                if(!usuarioResponsavel) // se retornar falso
                {
                    throw new DomainException("O responsavel só pode solicitar transferencia de patrimonio do ambiente ao qual esta vinculado.");
                }
            }

            StatusTransferencia statusPendente = _repository.BuscarStatusTransferenciaPorNome("Pendente de Aprovação");

            if(statusPendente == null)
            {
                throw new DomainException("Status de transferencia pendente não encontrado.");
            }

            SolicitacaoTransferencia solicitacao = new SolicitacaoTransferencia
            {
                DataCriacaoSolicitante = DateTime.Now,
                Justificativa = dto.Justificativa,
                StatusTransferenciaID = statusPendente.StatusTransferenciaID,
                UsuarioIDSolicitacao = usuarioID,
                UsuarioIDAprovacao = null,
                PatrimonioID = dto.PatrimonioID,
                LocalizacaoID = dto.LocalizacaoID,
            };

            _repository.Adicionar(solicitacao);
        }

        public void Responder(Guid transferenciaId, Guid usuarioId, ResponderSolicitacaoTransferenciaDto dto)
        {
            Usuario usuario = _usuarioRepository.BuscarPorId(usuarioId);

            if(usuario == null)
            {
                throw new DomainException("Usuario não encontrado.");
            }

            SolicitacaoTransferencia solicitacao = _repository.BuscarPorId(transferenciaId);

            if(solicitacao == null)
            {
                throw new DomainException("Solicitacao de transferencia não encontrada.");
            }

            Patrimonio patrimonio = _repository.BuscarPatrimonioPorId(solicitacao.PatrimonioID);

            if(patrimonio == null)
            {
                throw new DomainException("Patrimonio não encontrado");
            }

            StatusTransferencia statusPendente = _repository.BuscarStatusTransferenciaPorNome("Pendente de aprovação");

            if(statusPendente == null)
            {
                throw new DomainException("Status pendente não encontrado.");
            }

            if(solicitacao.StatusTransferencia != statusPendente.StatusTransferenciaID)
            {
                throw new DomainException("Essa solicitacao ja foi respondida.");
            }

            if(usuario.TipoUsuario.NomeTipo == "Responsavel")
            {
                bool usuarioResponsavel = _repository.UsuarioResponsavelDaLocalizacao(usuarioId, patrimonio.LocalizacaoID);
                
                if(!usuarioResponsavel)
                {
                    throw new DomainException("Somente o responsavel do ambiente de origem pode aprovar ou rejeitar essa solicitação.");
                }
            }

            StatusTransferencia statusResposta;

            if(dto.Aprovado)
            {
                statusResposta = _repository.BuscarStatusTransferenciaPorNome("Aprovado");
            }
            else
            {
                statusResposta = _repository.BuscarStatusTransferenciaPorNome("Recusado");
            }

            if(statusResposta == null)
            {
                throw new DomainException("Status de resposta da transferencia não encontrado.");
            }

            solicitacao.StatusTransferenciaID = statusResposta.StatusTransferenciaID;
            solicitacao.UsuarioIDAprovacao = usuarioId;
            solicitacao.DataResposta = DateTime.Now;

            _repository.Atualizar(solicitacao);

            if(dto.Aprovado)
            {
                StatusPatrimonio statusTransferido = _repository.BuscarStatusPatrimonioPorNome("Transferido");

                if(statusTransferido == null)
                {
                    throw new DomainException("Status de patrimonio 'Transferido' não encontrado");
                }

                TipoAlteracao tipoAlteracao = _repository.BuscarTipoAlteracaoPorNome("Transferencia");
                
                if(tipoAlteracao == null)
                {
                    throw new DomainException("Tipo alteracao 'Transferencia' não encontrado");
                }

                patrimonio.LocalizacaoID = solicitacao.LocalizacaoID;
                patrimonio.StatusPatrimonioID = statusTransferido.StatusPatrimonioID;

                _repository.AtualizarPatrimonio(patrimonio);

                LogPatrimonio log = new LogPatrimonio
                { 
                    DataTransferencia = DateTime.Now,   
                    TipoAlteracaoID = tipoAlteracao.TipoAlteracaoID,
                    StatusPatrimonioID = statusTransferido.StatusPatrimonioID,
                    PatrimonioID = patrimonio.PatrimonioID,
                    UsuarioID = usuarioId,
                    LocalizacaoID = patrimonio.LocalizacaoID
                };

                _repository.Adicionar(log);
            }
        }
    }
}
