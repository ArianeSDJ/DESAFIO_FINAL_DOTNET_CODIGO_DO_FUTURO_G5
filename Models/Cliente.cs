using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace desafio_dotnet.Models;

public record Cliente
{
    [Key]
    public int Id {get;set;} =default!;
    [Column("nome", TypeName = "varchar(80)")]
    public string Nome {get;set;} =default!;

    [Column("telefone", TypeName = "varchar(15)")]
    public string Telefone {get;set;} =default!;

    [Column("email", TypeName = "varchar(40)")]
    public string Email {get;set;} = default!;

    [Column("cpf", TypeName = "varchar(15)")]
    public string CPF {get;set;} =default!;

    [Column("cep", TypeName = "varchar(10)")]
    public string Cep {get;set;} =default!;

    [Column("logradouro", TypeName = "varchar(50)")]
    public string Logradouro {get;set;} = default!;

    [Column("numero", TypeName = "int")]
    public int Numero {get;set;} =default!;

    [Column("complemento", TypeName = "varchar(20)")]
    public string? Complemento {get;set;}

    [Column("bairro", TypeName = "varchar(20)")]
    public string Bairro {get;set;} =default!;

    [Column("cidade", TypeName = "varchar(20)")]
    public string Cidade {get;set;} =default!;

    [Column("estado", TypeName = "varchar(2)")]
    public string Estado {get;set;} =default!;
}
