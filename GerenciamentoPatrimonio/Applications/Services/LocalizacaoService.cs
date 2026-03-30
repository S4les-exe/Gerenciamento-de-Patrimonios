using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTOs.LocalizacaoDto;
using GerenciamentoPatrimonio.Exceptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class LocalizacaoService
    {
        private readonly ILocalizacaoRepository _repository;

        public LocalizacaoService(ILocalizacaoRepository repository)
        {
            _repository = repository;   
        }

        public List<ListarLocalizacaoDto> Listar()
        {
            List<Localizacao> localizacoes = _repository.Listar();

            List<ListarLocalizacaoDto> localizacoesDto = localizacoes.Select(localizacao => new ListarLocalizacaoDto
            {
                LocalizacaoID = localizacao.LocalizacaoID,
                NomeLocal = localizacao.NomeLocal,
                LocalSAP = localizacao.LocalSAP,
                DescricaoSAP = localizacao.DescricaoSAP,
                AreaID = localizacao.AreaID
            }).ToList();

            return localizacoesDto;
        }

        public ListarLocalizacaoDto BuscarPorId(Guid localizacaoId)
        {
            Localizacao localizacao = _repository.BuscarPorId(localizacaoId);

            if(localizacao == null)
            {
                throw new CannotUnloadAppDomainException("Localização não encontrada");
            }

            ListarLocalizacaoDto localizacaoDto = new ListarLocalizacaoDto
            {
                LocalizacaoID = localizacao.LocalizacaoID,
                NomeLocal = localizacao.NomeLocal,
                LocalSAP = localizacao.LocalSAP,
                DescricaoSAP = localizacao.DescricaoSAP,
                AreaID = localizacao.AreaID
            };

            return localizacaoDto;
        }

        public void Adicionar(CriarLocalizacaoDto dto)
        {
            Validar.ValidarNome(dto.NomeLocal);

            Localizacao localExistente = _repository.BuscarPorNome(dto.NomeLocal, dto.AreaID);

            if(localExistente != null)
            {
                throw new DomainException("Ja existe um localizacao cadastrado com esse nome nessa area");
            }

            if(!_repository.AreaExiste(dto.AreaID))
            {
                throw new DomainException("Area informada nao existe.");
            }

            Localizacao localizacao = new Localizacao
            {
                NomeLocal = dto.NomeLocal,
                LocalSAP = dto.LocalSAP,
                DescricaoSAP = dto.DescricaoSAP,
                AreaID = dto.AreaID
            };

            _repository.Adicionar(localizacao);
        }

        public void Atualizar(Guid localizacaoId, CriarLocalizacaoDto dto)
        {
            Validar.ValidarNome(dto.NomeLocal);

            Localizacao localizacaoBanco = _repository.BuscarPorId(localizacaoId);

            if(localizacaoBanco == null)
            {
                throw new DomainException("Localização não encontrada");
            }

            Localizacao localExistente = _repository.BuscarPorNome(dto.NomeLocal, dto.AreaID);

            if (localExistente != null)
            {
                throw new DomainException("Ja existe um localizacao cadastrado com esse nome nessa area");
            }

            if (!_repository.AreaExiste(dto.AreaID))
            {
                throw new DomainException("Area informada não existe");
            }

            localizacaoBanco.NomeLocal = dto.NomeLocal;
            localizacaoBanco.LocalSAP = dto.LocalSAP;
            localizacaoBanco.DescricaoSAP = dto.DescricaoSAP;
            localizacaoBanco.AreaID = dto.AreaID;

            _repository.Atualizar(localizacaoBanco);
        }
    }
}
