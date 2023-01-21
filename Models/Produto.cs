using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace desafio_dotnet.Models;

public record Produto
{
    [Key]
    public int Id {get;set;} =default!;
    [Column("nome", TypeName = "varchar(50)")]
    public string Nome {get;set;} =default!;
    [Column("descricacao", TypeName = "varchar(250)")]
    public string Descricao {get;set;} =default!;
    [Column("valor", TypeName = "double")]
    public double Valor {get;set;} = default!;
     [Column("quantidade_estoque", TypeName = "int")]
    public int QuantidadeEstoque {get;set;} =default!;
}