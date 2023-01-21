using desafio_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace desafio_dotnet.Contexto;

public class DbContexto : DbContext
{
    public DbContexto(DbContextOptions<DbContexto> options) : base(options) { }
    
    public DbSet<Loja> Lojas { get; set; } = default!;
    public DbSet<Campanha> Campanhas { get; set; } = default!;
    public DbSet<PosicaoProduto> PosicoesProdutos { get; set; } = default!;

    public DbSet<Cliente> Clientes { get; set; } = default!;
    public DbSet<Produto> Produtos { get; set; } = default!;

} 
