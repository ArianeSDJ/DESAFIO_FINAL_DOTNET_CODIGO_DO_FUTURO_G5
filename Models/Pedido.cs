using System.ComponentModel.DataAnnotations;

namespace desafio_dotnet.Models;

public record Pedido
{
    [Key]
    public int Id { get; set; } = default!;
    [Required]
    public double ValorTotal { get; set; } = default!;

    public DateTime? Data { get; set; } = DateTime.Now;

    public Cliente? Cliente { get; set; }
    public int ClienteId { get; set; }
}
