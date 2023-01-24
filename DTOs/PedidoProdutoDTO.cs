
using desafio_dotnet.Models;

namespace desafio_dotnet.DTOs;

public record PedidoProdutoDTO
{
    public double Valor { get; set; } = default!;
    public int Quantidade { get; set; } = default!;
    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; } = default!;
    public int ProdutoId { get; set; }
    public Produto Produto { get; set; } = default!;
}