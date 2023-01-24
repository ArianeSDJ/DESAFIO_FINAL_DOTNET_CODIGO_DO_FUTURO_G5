
namespace desafio_dotnet.DTOs;

public record ProdutoDTO
{
    public string Nome {get;set;} =default!;
    public string Descricao {get;set;} =default!;
    public double Valor {get;set;} = default!;
    public int QuantidadeEstoque {get;set;} =default!;
}