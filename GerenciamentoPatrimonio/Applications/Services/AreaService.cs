
using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTOs.AreaDto;
using GerenciamentoPatrimonio.Exceptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class AreaService 
    {
        private readonly IAreaRepository _repository;
        
        public AreaService(IAreaRepository repository)
        {
            _repository = repository;
        }

        public List<ListarAreaDto> Listar()
        {
            List<Area> areas = _repository.Listar();

            List<ListarAreaDto> areasDto = areas.Select(area => new ListarAreaDto 
            { 
                AreaID = area.AreaID,
                NomeArea = area.NomeArea 
            }).ToList();

            return areasDto;
        }

        public ListarAreaDto BuscarPorId(Guid areaId)
        {
            Area area = _repository.BuscarPorId(areaId);

            if (area == null)
            {
                throw new DomainException("Area não Encontrada");
            }

            ListarAreaDto areaDto = new ListarAreaDto
            {
                AreaID = area.AreaID,
                NomeArea = area.NomeArea
            };

            return areaDto;
        }

        public void Adicionar(CriarAreaDto dto)
        {
            Validar.ValidarNome(dto.NomeArea);

            Area areaExistente = _repository.BuscarPorNome(dto.NomeArea);

            if(areaExistente != null)
            {
                throw new DomainException("Já existe uma area cadastrada com esse nome.");
            }

            Area area = new Area
            {
                //AreaID = Guid.NewGuid(),
                NomeArea = dto.NomeArea
            };

            _repository.Adicionar(area);
        }

        public void Atualizar(Guid areaId, CriarAreaDto dto)
        {
            Validar.ValidarNome(dto.NomeArea);

            Area areaBanco = _repository.BuscarPorId(areaId);

            if (areaBanco == null)
            {
                throw new DomainException("Area não encontrada.");
            }

            Area areaExistente = _repository.BuscarPorNome(dto.NomeArea);

            if(areaExistente != null)
            {
                throw new DomainException("Já existe uma area cadastrada com esse nome.");
            }

            areaBanco.NomeArea = dto.NomeArea;

            _repository.Atualizar(areaBanco);
        }
    }
}
