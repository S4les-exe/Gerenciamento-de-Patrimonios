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

        public BuscarPorId(Guid EnderecoId)
        {
            return _context.Endereco.Find(EnderecoId);
        }

        public Endereco BuscarPorLogradouroENumero(string logradouro, int? numero, Guid bairroId)
        {
            return _context.Endereco.FirstOrDefault(endereco => endereco.Logradouro.ToLower() == logradouro.ToLower()
                && endereco.Numero == numero 
                && endereco.BairroID == bairroId
                );
        }

        Endereco BairroExiste(Guid bairroId)
        {
            return _context.Endereco.Any(bairroId);
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
