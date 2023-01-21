using desafio_dotnet.Contexto;
using desafio_dotnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace desafio_dotnet.Controllers;

[Route("pedidos")]
public class PedidosController : ControllerBase
{
    private readonly DbContexto _contexto;
    public PedidosController(DbContexto contexto)
    {
        _contexto = contexto;
    }

    [HttpGet("")]
    public async Task<IActionResult> Lista()
    {
        List<Pedido> pedidos = await _contexto.Pedidos.ToListAsync();
        return StatusCode(200, pedidos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Detalhes([FromRoute] int id)
    {
        var pedido = await _contexto.Pedidos.FindAsync(id);
        if (pedido is not null)
        {
             return StatusCode(200, pedido);
        }
        return StatusCode(404, new { Mensagem = "Pedido não encontrado"});
    }

    [HttpPost("")]
    public async Task<IActionResult> Novo([FromBody] Pedido pedidoNovo)
    {
        _contexto.Add(pedidoNovo);
        await _contexto.SaveChangesAsync();
        return StatusCode(201, pedidoNovo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualiza([FromRoute] int id, [FromBody] Loja pedidoAtualizado)
    {
        var pedido = await _contexto.Pedidos.FindAsync(id);
        if(pedido is not null)
        {
            _contexto.Entry(pedido).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
        return StatusCode(200, pedido);
        }
        return StatusCode(404, new{Mensagem = "Pedido não encontrado"});
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deleta([FromRoute] int id)
    {
        var pedido = await _contexto.Pedidos.FindAsync(id);
        if (pedido is not null)
        {
            _contexto.Pedidos.Remove(pedido);
                await _contexto.SaveChangesAsync();
        return StatusCode(200, new {Mensagem=$"Pedido {pedido?.Nome} apagado com sucesso"});
        }
        return StatusCode(404, new{Mensagem = "Pedido não encontrado"});
    }


}