
namespace desafio_dotnet.Models;

public record PosicaoProduto
{
    public int Id { get; set; } = default!;
    public int CampanhaId { get; set; } = default!;
   // public Campanha Campanha { get; set; } = default!;
    public double PosicaoX { get; set; } = default!;
    public double PosicaoY { get; set; } = default!;
}