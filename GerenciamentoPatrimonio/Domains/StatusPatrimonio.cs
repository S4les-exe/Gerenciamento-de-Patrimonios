using System;
using System.Collections.Generic;

namespace GerenciamentoPatrimonio.Domains;

public partial class StatusPatrimonio
{
    public Guid StatusPatrimonioID { get; set; }

    public string NomeStatus { get; set; } = null!;

    public virtual ICollection<LogPatrimonio> LogPatrimonio { get; set; } = new List<LogPatrimonio>();

    public virtual ICollection<Patrimonio> Patrimonio { get; set; } = new List<Patrimonio>();
}
