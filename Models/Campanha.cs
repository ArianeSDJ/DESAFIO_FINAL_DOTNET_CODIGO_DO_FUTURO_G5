using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace desafio_dotnet.Models;

public record Campanha
{
    [Key]
    public int Id {get;set;} =default!;
    [Column("nome", TypeName = "varchar(50)")]
    public string Nome {get;set;} =default!;
    [Column("descricacao", TypeName = "varchar(100)")]
    public string Descricao {get;set;} =default!;
    [Column("data", TypeName = "datetime")]
    public DateTime? Data {get;set;} = DateTime.Now;
     [Column("url_foto_prateleira", TypeName = "varchar(150)")]
    public string UrlFotoPrateleira {get;set;} =default!;
}