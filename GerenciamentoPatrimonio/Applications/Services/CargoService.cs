using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTOs.AreaDto;
using GerenciamentoPatrimonio.DTOs.CargoDto;
using GerenciamentoPatrimonio.Exceptions;
using GerenciamentoPatrimonio.Repositories;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class CargoService
    {
        private readonly CargoRepository _repository;

        public CargoService(CargoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarCargoDto> Listar()
        {
            List<Cargo> cargos = _repository.Listar();

            List<ListarCargoDto> cargosDto = cargos.Select(cargo => new ListarCargoDto
            {
                CargoID = cargo.CargoID,
                NomeCargo = cargo.NomeCargo
            }).ToList();

            return cargosDto;
        }

        public ListarCargoDto BuscarPorId(Guid cargoId)
        {
            Cargo cargo = _repository.BuscarPorId(cargoId);

            if (cargo == null)
            {
                throw new DomainException("Cargo não Encontrado");
            }

            ListarCargoDto cargoDto = new ListarCargoDto
            {
                CargoID = cargo.CargoID,
                NomeCargo = cargo.NomeCargo
            };

            return cargoDto;
        }

        public void Adicionar(CriarCargoDto dto)
        {
            Validar.ValidarNome(dto.NomeCargo);

            Cargo CargoExistente = _repository.BuscarPorNome(dto.NomeCargo);

            if (CargoExistente != null)
            {
                throw new DomainException("Já existe um cargo cadastrado com esse nome.");
            }

            Cargo cargo = new Cargo
            {
                //AreaID = Guid.NewGuid(),
                NomeCargo = dto.NomeCargo
            };

            _repository.Adicionar(cargo);
        }

        public void Atualizar(Guid cargoId, CriarCargoDto dto)
        {
            Validar.ValidarNome(dto.NomeCargo);

            Cargo cargoBanco = _repository.BuscarPorId(cargoId);

            if (cargoBanco == null)
            {
                throw new DomainException("Cargo não encontrado.");
            }

            Cargo CargoExistente = _repository.BuscarPorNome(dto.NomeCargo);

            if (CargoExistente != null)
            {
                throw new DomainException("Já existe um cargo cadastrado com esse nome.");
            }

            cargoBanco.NomeCargo = dto.NomeCargo;

            _repository.Atualizar(cargoBanco);
        }
    }
}
