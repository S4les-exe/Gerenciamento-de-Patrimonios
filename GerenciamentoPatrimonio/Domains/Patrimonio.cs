using System;
using System.Collections.Generic;

namespace GerenciamentoPatrimonio.Domains;

public partial class Patrimonio
{
    public Guid PatrimonioID { get; set; }

    public string Denominacao { get; set; } = null!;

    public string NumeroPatrimonio { get; set; } = null!;

    public decimal? Valor { get; set; }

    public string? Imagem { get; set; }

    public Guid LocalizacaoID { get; set; }

    public Guid TipoPatrimonioID { get; set; }

    public Guid StatusPatrimonioID { get; set; }

    public virtual Localizacao Localizacao { get; set; } = null!;

    public virtual ICollection<LogPatrimonio> LogPatrimonio { get; set; } = new List<LogPatrimonio>();

    public virtual ICollection<SolicitacaoTransferencia> SolicitacaoTransferencia { get; set; } = new List<SolicitacaoTransferencia>();

    public virtual StatusPatrimonio StatusPatrimonio { get; set; } = null!;
}
