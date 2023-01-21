using System.ComponentModel.DataAnnotations;

namespace desafio_dotnet.Models;

public record PedidoProduto
{
    [Key]
    public int Id {get;set;} =default!;
    [Required]
    public double Valor {get;set;} =default!;
    public int Quantidade {get;set;} =default!;
    
}