using System;
using System.Collections.Generic;

namespace GerenciamentoPatrimonio.Domains;

public partial class Usuario
{
    public Guid UsuarioID { get; set; }

    public string NIF { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string? RG { get; set; }

    public string CPF { get; set; } = null!;

    public string CarteiraTrabalho { get; set; } = null!;

    public byte[] Senha { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? Ativo { get; set; }

    public Guid EnderecoID { get; set; }

    public Guid CargoID { get; set; }

    public Guid TipoUsuarioID { get; set; }

    public virtual Cargo Cargo { get; set; } = null!;

    public virtual Endereco Endereco { get; set; } = null!;

    public virtual ICollection<LogPatrimonio> LogPatrimonio { get; set; } = new List<LogPatrimonio>();

    public virtual ICollection<SolicitacaoTransferencia> SolicitacaoTransferenciaUsuarioIDAprovacaoNavigation { get; set; } = new List<SolicitacaoTransferencia>();

    public virtual ICollection<SolicitacaoTransferencia> SolicitacaoTransferenciaUsuarioIDSolicitacaoNavigation { get; set; } = new List<SolicitacaoTransferencia>();

    public virtual TipoUsuario TipoUsuario { get; set; } = null!;

    public virtual ICollection<Localizacao> Localizacao { get; set; } = new List<Localizacao>();
}
