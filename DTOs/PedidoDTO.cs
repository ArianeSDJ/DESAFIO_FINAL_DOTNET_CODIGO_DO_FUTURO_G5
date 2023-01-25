
using desafio_dotnet.Models;

namespace desafio_dotnet.DTOs;

public record PedidoDTO
{
    public double ValorTotal { get; set; } = default!;
    public DateTime? Data { get; set; } = DateTime.Now;
    public Cliente? Cliente { get; set; }
    public int ClienteId { get; set; }
}
