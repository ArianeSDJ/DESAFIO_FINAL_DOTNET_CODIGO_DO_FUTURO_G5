using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace desafio_dotnet.Models;

public record Loja
{
    [Key]
    public int Id {get;set;} = default!;
    
    [Column("nome", TypeName = "varchar(50)")]
    public string Nome {get;set;} = default!;

    [Column("observacao", TypeName = "varchar(150)")]
    public string? Observacao {get;set;}
    
    [Column("cep", TypeName = "varchar(9)")]
    public string Cep {get;set;} = default!;

    [Column("logradouro", TypeName = "varchar(50)")]
    public string Logradouro {get;set;} = default!;

    public int Numero {get;set;} = default!;

    [Column("bairro", TypeName = "varchar(50)")]
    public string Bairro {get;set;} = default!;

    [Column("cidade", TypeName = "varchar(50)")]
    public string Cidade {get;set;} = default!;

    [Column("estado", TypeName = "varchar(2)")]
    public string Estado {get;set;} = default!;

    [Column("complemento", TypeName = "varchar(50)")]
    public string? Complemento {get;set;} 
    public double Latitude {get;set;} = default!;
    public double Longitude {get;set;} = default!;
}
