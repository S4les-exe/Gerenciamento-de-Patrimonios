using System;
using System.Collections.Generic;

namespace GerenciamentoPatrimonio.Domains;

public partial class TipoUsuario
{
    public Guid TipoUsuarioID { get; set; }

    public string? NomeTipo { get; set; }

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
