
namespace desafio_dotnet.DTOs;

public record LojaDTO
{
    public string Nome {get;set;} = default!;
    public string Cep {get;set;} = default!;
    public string Logradouro {get;set;} = default!;
    public int Numero {get;set;} = default!;
    public string Bairro {get;set;} = default!;
    public string Cidade {get;set;} = default!;
    public string Estado {get;set;} = default!;
    public double Latitude {get;set;} = default!;
    public double Longitude {get;set;} = default!;
    public string? Observacao {get;set;}
    public string? Complemento {get;set;} 
}