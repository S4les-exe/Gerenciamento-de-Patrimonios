using GerenciamentoPatrimonio.Contexts;
using GerenciamentoPatrimonio.Domains;
using GerenciamentoPatrimonio.Interfaces;

namespace GerenciamentoPatrimonio.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly GerenciamentoSenaiDBContext _context; 

        public EnderecoRepository(GerenciamentoSenaiDBContext context)
        {
            _context = context;
        }

        public List<Endereco> Listar()
        {
            return _context.Endereco.OrderBy(endereco => endereco.CEP).ToList();
        }

        public Endereco BuscarPorId(Guid EnderecoId)
        {
            return _context.Endereco.Find(EnderecoId);
        }

        public Endereco BuscarPorLogradouroENumero(string logradouro, int? numero, Guid bairroId, Guid? enderecoId = null)
        {
            var consulta = _context.Endereco.AsQueryable();

            if (enderecoId.HasValue)
            {
                consulta = consulta.Where(endereco => endereco.EnderecoID != enderecoId.Value);
            }

            return consulta.FirstOrDefault(endereco =>
                   endereco.Logradouro.ToLower() == logradouro.ToLower() 
                && endereco.Numero == numero 
                && endereco.BairroID == bairroId
                && endereco.EnderecoID == enderecoId
            );
        }

        public bool BairroExiste(Guid bairroId)
        {
            return _context.Endereco.Any(endereco => endereco.BairroID == bairroId);
        }

        public void Adicionar(Endereco endereco)
        {
            _context.Endereco.Add(endereco);
            _context.SaveChanges();
        }

        public void Atualizar(Endereco endereco)
        {
            if (endereco == null)
            {
                return;
            }

            Endereco enderecoBanco = _context.Endereco.Find(endereco.EnderecoID);

            if (endereco == null)
            {
                return;
            }

            enderecoBanco.Logradouro = endereco.Logradouro;
            enderecoBanco.Numero = endereco.Numero;
            enderecoBanco.Complemento = endereco.Complemento;   
            enderecoBanco.CEP = endereco.CEP;   

            _context.SaveChanges();
        }
    }
}
