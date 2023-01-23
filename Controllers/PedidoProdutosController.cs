using desafio_dotnet.Contexto;
using desafio_dotnet.Models;
using desafio_dotnet.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace desafio_dotnet.Controllers;

[Route("pedidoProdutos")]
public class PedidoProdutosController : ControllerBase
{
    private readonly DbContexto _contexto;
    public PedidoProdutosController(DbContexto contexto)
    {
        _contexto = contexto;
    }
    [HttpGet("")]
    public async Task<IActionResult> Lista([FromQuery] int page = 1, [FromQuery] int take = 20)
    {
        int total = await _contexto.PedidoProdutos.CountAsync();
        if (total == 0) return StatusCode(200, new ListarRetorno<PedidoProduto> { Mensagem = "Ainda não há Produtos cadastradas em Pedidos", TotalRegistros=total});

        int maximoPaginas = (total / take) + 1;
        if(page>maximoPaginas) return StatusCode(404, new ListarRetorno<PedidoProduto> {Mensagem="Essa pagina não existe", MaximoPaginas=maximoPaginas});
        
        try
        {
            int pagina = (page - 1) * take;
            List<PedidoProduto> pedidoProdutos = await _contexto.PedidoProdutos
                .Skip(pagina)
                .Take(take)
                .ToListAsync();
            return StatusCode(200, new ListarRetorno<PedidoProduto> { TotalRegistros = total, PaginaAtual = page, MaximoPaginas = maximoPaginas, Dados = pedidoProdutos });
        }
        catch
        {
            return StatusCode(400, new ListarRetorno<PedidoProduto> {Mensagem = "ALgo deu errado"});
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Detalhes([FromRoute] int id)
    {
        var pedidoProduto = await _contexto.PedidoProdutos.FindAsync(id);
        if (pedidoProduto is not null)
        {
             return StatusCode(200, pedidoProduto);
        }
        return StatusCode(404, new { Mensagem = "Pedido não encontrado"});
    }
    [HttpPost("")]
    public async Task<IActionResult> Novo([FromBody] Pedido pedidoProdutoNovo)
    {
        _contexto.Add(pedidoProdutoNovo);
        await _contexto.SaveChangesAsync();
        return StatusCode(201, pedidoProdutoNovo);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Atualiza([FromRoute] int id, [FromBody] PedidoProduto pedidoProdutoAtualizado)
    {
        if (id != pedidoProdutoAtualizado.Id)
        {
            return StatusCode(404, new { Mensagem = "Pedido não encontrada"});
        }

        _contexto.Entry(pedidoProdutoAtualizado).State = EntityState.Modified;
        await _contexto.SaveChangesAsync();

        return StatusCode(200, pedidoProdutoAtualizado);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Deleta([FromRoute] int id)
    {
        var pedidoProduto = await _contexto.PedidoProdutos.FindAsync(id);
        if (pedidoProduto is not null)
        {
            _contexto.PedidoProdutos.Remove(pedidoProduto);
            await _contexto.SaveChangesAsync();
            return StatusCode(200, new {Mensagem="pedido apagado com sucesso"});
        }   
        return StatusCode(404, new { Mensagem = "Pedido não encontrado"});     
    }
}