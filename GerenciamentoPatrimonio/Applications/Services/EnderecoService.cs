using GerenciamentoPatrimonio.Applications.Regras;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.DTOs.EnderecoDto;
using GerenciamentoPatrimonio.Exceptions;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Applications.Services
{
    public class EnderecoService
    {
        private readonly IEnderecoRepository _repository;

        public EnderecoService(IEnderecoRepository repository)
        {
            _repository = repository;   
        }

        public List<ListarEnderecoDto> Listar()
        {
            List<Endereco> enderecos = _repository.Listar();  
            
            List<ListarEnderecoDto> enderecosDto = enderecos.Select(endereco =>  new ListarEnderecoDto
            {
                EnderecoID = endereco.EnderecoID,
                BairroID = endereco.BairroID,   
                Numero = endereco.Numero,
                CEP = endereco.CEP,
                Logradouro = endereco.Logradouro,   
                Complemento = endereco.Complemento
            }).ToList();

            return enderecosDto;
        }

        public ListarEnderecoDto BuscarPorId(Guid enderecoId)
        {
            Endereco endereco = _repository.BuscarPorId(enderecoId);

            if(endereco == null)
            {
                throw new DomainException("Endereço não encontrada");
            }

            ListarEnderecoDto enderecoDto = new ListarEnderecoDto
            {
                EnderecoID = endereco.EnderecoID,
                BairroID = endereco.BairroID,
                Numero = endereco.Numero,
                CEP = endereco.CEP,
                Logradouro = endereco.Logradouro,
                Complemento = endereco.Complemento
            };

            return enderecoDto;
        }

        public void Adicionar(CriarEnderecoDto dto)
        {
            Validar.ValidarNome(dto.)
        }
    }
}
