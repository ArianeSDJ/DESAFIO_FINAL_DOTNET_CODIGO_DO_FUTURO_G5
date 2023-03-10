using desafio_dotnet.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace desafio_dotnet.Contexto;

public class DbContexto : IdentityDbContext
{
    public DbContexto(DbContextOptions<DbContexto> options) : base(options) { }
    
    public DbSet<Pedido> Pedidos { get; set; } = default!;
    public DbSet<PedidoProduto> PedidoProdutos { get; set; } = default!;
    public DbSet<Loja> Lojas { get; set; } = default!;
    public DbSet<Campanha> Campanhas { get; set; } = default!;
    public DbSet<PosicaoProduto> PosicoesProdutos { get; set; } = default!;
    public DbSet<Cliente> Clientes { get; set; } = default!;
    public DbSet<Produto> Produtos { get; set; } = default!;

} 
