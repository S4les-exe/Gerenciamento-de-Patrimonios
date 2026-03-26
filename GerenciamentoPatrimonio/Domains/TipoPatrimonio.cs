using System;
using System.Collections.Generic;

namespace GerenciamentoPatrimonio.Domains;

public partial class TipoPatrimonio
{
    public Guid TipoPatrimonioID { get; set; }

    public string NomeTipo { get; set; } = null!;

    public virtual ICollection<Patrimonio> Patrimonio { get; set; } = new List<Patrimonio>();
}
