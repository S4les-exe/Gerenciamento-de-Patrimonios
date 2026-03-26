using System;
using System.Collections.Generic;

namespace GerenciamentoPatrimonio.Domains;

public partial class Localizacao
{
    public Guid LocalizacaoID { get; set; }

    public string NomeLocal { get; set; } = null!;

    public int? LocalSAP { get; set; }

    public string? DescricaoSAP { get; set; }

    public bool? Ativo { get; set; }

    public Guid AreaID { get; set; }

    public virtual Area Area { get; set; } = null!;

    public virtual ICollection<LogPatrimonio> LogPatrimonio { get; set; } = new List<LogPatrimonio>();

    public virtual ICollection<Patrimonio> Patrimonio { get; set; } = new List<Patrimonio>();

    public virtual ICollection<SolicitacaoTransferencia> SolicitacaoTransferencia { get; set; } = new List<SolicitacaoTransferencia>();

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
