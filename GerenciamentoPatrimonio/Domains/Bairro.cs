using System;
using System.Collections.Generic;

namespace GerenciamentoPatrimonio.Domains;

public partial class Bairro
{
    public Guid BairroID { get; set; }

    public string NomeBairro { get; set; } = null!;

    public Guid CidadeID { get; set; }

    public virtual Cidade Cidade { get; set; } = null!;

    public virtual ICollection<Endereco> Endereco { get; set; } = new List<Endereco>();
}
