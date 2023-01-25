
namespace desafio_dotnet.ModelView;

public struct ListarRetorno<T>
{
    public int? TotalRegistros {get; set;}
    public List<T>? Dados {get;set;}
    public int? PaginaAtual {get;set;}
    public int? MaximoPaginas {get; set;}
    public string? Mensagem {get;set;}
}