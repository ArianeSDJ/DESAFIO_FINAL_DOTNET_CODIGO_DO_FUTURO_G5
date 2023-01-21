using desafio_dotnet.Contexto;
using desafio_dotnet.Models;
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
    public async Task<IActionResult> Lista()
    {
        List<PedidoProduto> pedidoProduto = await _contexto.PedidoProdutos.ToListAsync();
        return StatusCode(200, pedidoProduto);
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
        var pedidoProduto = await _contexto.PedidoProdutos.FindAsync(id);
        if(pedidoProduto is not null)
        {
            pedidoProduto.Valor = pedidoProdutoAtualizado.Valor;
            pedidoProduto.Quantidade = pedidoProdutoAtualizado.Quantidade;
            await _contexto.SaveChangesAsync();
            return StatusCode(200, pedidoProduto);
        }
        return StatusCode(404, new { Mensagem = "Pedido não encontrado"});
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