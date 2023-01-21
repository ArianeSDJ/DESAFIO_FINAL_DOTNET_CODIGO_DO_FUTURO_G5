
namespace desafio_dotnet.DTOs;

public record CampanhaDto
{
    public int Id {get;set;} =default!;
    public string Nome {get;set;} =default!;
    public string Descricao {get;set;} =default!;
    public DateTime? Data {get;set;}
    public string UrlFotoPrateleira {get;set;} =default!;
}