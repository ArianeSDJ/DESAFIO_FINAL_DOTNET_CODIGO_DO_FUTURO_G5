namespace desafio_dotnet.Models;

public record Loja
{
    public int id {get;set;} = default!;
    public string nome {get;set;} = default!;
    public string? observacao {get;set;}
    public string cep {get;set;} = default!;
    public string logradouro {get;set;} = default!;
    public int numero {get;set;} = default!;
    public string bairro {get;set;} = default!;
    public string cidade {get;set;} = default!;
    public string estado {get;set;} = default!;
    public string? complemento {get;set;} 
    public double latitude {get;set;} = default!;
    public double longitude {get;set;} = default!;
}
