using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTOs.CidadeDto;
using GerenciamentoPatrimonio.Exceptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class CidadeService
    {
        private readonly ICidadeRepository _repository;

        public CidadeService(ICidadeRepository repository)
        {
            _repository = repository;
        }

        public List<ListarCidadeDto> Listar()
        {
            List<Cidade> areas = _repository.Listar();

            List<Cidade> cidades = _repository.Listar();

            List<ListarCidadeDto> cidadesDto = cidades.Select(cidade => new ListarCidadeDto
            {
                CidadeID = cidade.CidadeID,
                NomeCidade = cidade.NomeCidade,
                Estado = cidade.Estado
            }).ToList();

            return cidadesDto;
        }

        public ListarCidadeDto BuscarPorId(Guid cidadeId)
        {
            Cidade? cidade = _repository.BuscarPorId(cidadeId);

            if (cidade == null)
            {
                throw new DomainException("Cidade não encontrada.");
            }

            ListarCidadeDto cidadeDto = new ListarCidadeDto
            {
                CidadeID = cidade.CidadeID,
                NomeCidade = cidade.NomeCidade,
                Estado = cidade.Estado
            };

            return cidadeDto;
        }

        public void Adicionar(CriarCidadeDto dto)
        {
            Validar.ValidarNome(dto.NomeCidade);
            Validar.ValidarEstado(dto.Estado);

            Cidade? cidadeExistente = _repository.BuscarPorNomeEEstado(dto.NomeCidade, dto.Estado);

            if (cidadeExistente != null)
            {
                throw new DomainException("Já existe uma cidade cadastrada com esse nome nesse estado.");
            }

            Cidade cidade = new Cidade
            {
                NomeCidade = dto.NomeCidade,
                Estado = dto.Estado
            };

            _repository.Adicionar(cidade);
        }

        public void Atualizar(Guid cidadeId, CriarCidadeDto dto)
        {
            Validar.ValidarNome(dto.NomeCidade);
            Validar.ValidarEstado(dto.Estado);

            Cidade? cidadeBanco = _repository.BuscarPorId(cidadeId);

            if (cidadeBanco == null)
            {
                throw new DomainException("Cidade não encontrada.");
            }

            Cidade? cidadeExistente = _repository.BuscarPorNomeEEstado(dto.NomeCidade, dto.Estado);

            if (cidadeExistente != null && cidadeExistente.CidadeID != cidadeId)
            {
                throw new DomainException("Já existe uma cidade cadastrada com esse nome nesse estado.");
            }

            cidadeBanco.NomeCidade = dto.NomeCidade;
            cidadeBanco.Estado = dto.Estado;

            _repository.Atualizar(cidadeBanco);
        }
    }
}

