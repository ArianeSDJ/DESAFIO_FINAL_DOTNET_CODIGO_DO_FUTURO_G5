
using desafio_dotnet.Models;

namespace desafio_dotnet.DTOs;

public record PedidoProdutoDTO
{
    public double Valor { get; set; } = default!;
    public int Quantidade { get; set; } = default!;
    public int PedidoId { get; set; }= default!;
    public Pedido ? Pedido { get; set; } 
    public int ProdutoId { get; set; }= default!;
    public Produto ? Produto { get; set; } 
}