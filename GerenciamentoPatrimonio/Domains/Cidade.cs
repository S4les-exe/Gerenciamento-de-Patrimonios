using System;
using System.Collections.Generic;

namespace GerenciamentoPatrimonio.Domains;

public partial class Cidade
{
    public Guid CidadeID { get; set; }

    public string NomeCidade { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public virtual ICollection<Bairro> Bairro { get; set; } = new List<Bairro>();
}
