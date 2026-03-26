using System;
using System.Collections.Generic;

namespace GerenciamentoPatrimonio.Domains;

public partial class LogPatrimonio
{
    public Guid LogPatrimonio1 { get; set; }

    public DateTime DataTransferencia { get; set; }

    public Guid TipoAlteracaoID { get; set; }

    public Guid StatusPatrimonioID { get; set; }

    public Guid PatrimonioID { get; set; }

    public Guid UsuarioID { get; set; }

    public Guid LocalizacaoID { get; set; }

    public virtual Localizacao Localizacao { get; set; } = null!;

    public virtual Patrimonio Patrimonio { get; set; } = null!;

    public virtual StatusPatrimonio StatusPatrimonio { get; set; } = null!;

    public virtual TipoAlteracao TipoAlteracao { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
