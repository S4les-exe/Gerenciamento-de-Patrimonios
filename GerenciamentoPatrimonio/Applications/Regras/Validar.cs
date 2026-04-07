using GerenciamentoPatrimonio.Exceptions;

namespace GerenciamentoPatrimonio.Applications.Regras
{
    public class Validar
    {
        public static void ValidarNome(string nome)
        {
            if(string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Nome é obrigatório");
            }
        }

        public static void ValidarEstado(string estado)
        {
            if (string.IsNullOrWhiteSpace(estado))
            {
                throw new DomainException("Estado é obrigatório.");
            }
        }

        public static void ValidarLogradouro(string logradouro)
        {
            if (string.IsNullOrWhiteSpace(logradouro))
            {
                throw new DomainException("Logradouro é obrigatório.");
            }
        }

        public static void ValidarNIF(string nif)
        {
            if(string.IsNullOrWhiteSpace(nif))
            {
                throw new DomainException("NIF é obrigatório.");
            }
        }

        public static void ValidarCPF(string cpf)
        {
            if(string.IsNullOrWhiteSpace(cpf))
            {
                throw new DomainException("CPF é obrigatório");
            }
        }

        public static void ValidarEmail(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
            {
                throw new DomainException("Email é obrigatório");
            }
        }

        public static void ValidarSenha(string senha)
        {
            if(string.IsNullOrWhiteSpace(senha))
            {
                throw new DomainException("Senha é obrigatoria");
            }
        }
    }
}
