using System;
using System.Collections.Generic;

namespace GerenciamentoPatrimonio.Domains;

public partial class Endereco
{
    public Guid EnderecoID { get; set; }

    public string Logradouro { get; set; } = null!;

    public int? Numero { get; set; }

    public string? Complemento { get; set; }

    public string? CEP { get; set; }

    public Guid BairroID { get; set; }

    public virtual Bairro Bairro { get; set; } = null!;

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
