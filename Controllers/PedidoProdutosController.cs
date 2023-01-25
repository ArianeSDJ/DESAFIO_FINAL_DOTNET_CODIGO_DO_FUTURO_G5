using desafio_dotnet.Contexto;
using desafio_dotnet.DTOs;
using desafio_dotnet.Models;
using desafio_dotnet.ModelView;
using desafio_dotnet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace desafio_dotnet.Controllers;

[Route("pedidoProdutos")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PedidoProdutosController : ControllerBase
{
    private readonly DbContexto _contexto;
    public PedidoProdutosController(DbContexto contexto)
    {
        _contexto = contexto;
    }
    
    [HttpGet("")]
    public async Task<IActionResult> Lista([FromQuery] int pedidoId)
    {
        int total = await _contexto.PedidoProdutos.CountAsync();
        if (total == 0) return StatusCode(200, new ListarRetorno<PedidoProduto> { Mensagem = "Ainda não há Produtos cadastradas em Pedidos" });

        try
        {
            var pedidoProdutos = _contexto.PedidoProdutos.Where(pp => pp.PedidoId == pedidoId);
            return StatusCode(200, pedidoProdutos);
        }
        catch
        {
            return StatusCode(400, new ListarRetorno<PedidoProduto> { Mensagem = "ALgo deu errado" });
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
        return StatusCode(404, new { Mensagem = "Pedido não encontrado" });
    }

    [HttpPost("")]
    public async Task<IActionResult> Novo([FromBody] PedidoProdutoDTO pedidoProdutoNovo)
    {
        try
        {
            var pedidoProduto = DtoBuilder<PedidoProduto>.Builder(pedidoProdutoNovo);
            _contexto.Add(pedidoProduto);
            await _contexto.SaveChangesAsync();
            return StatusCode(201, pedidoProdutoNovo);
        }
        catch
        {
            return StatusCode(400, pedidoProdutoNovo);
        }

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualiza([FromRoute] int id, [FromBody] PedidoProduto pedidoProdutoAtualizado)
    {
        if (id != pedidoProdutoAtualizado.Id)
        {
            return StatusCode(404, new { Mensagem = "Pedido não encontrada" });
        }

        try
        {
            _contexto.Entry(pedidoProdutoAtualizado).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
             return StatusCode(200, pedidoProdutoAtualizado);
        }
        catch
        {
            Console.WriteLine(pedidoProdutoAtualizado);
            return StatusCode(400, new {Mensagem="xii"});
        }

       
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Deleta([FromRoute] int id)
    {
        var pedidoProduto = await _contexto.PedidoProdutos.FindAsync(id);
        if (pedidoProduto is not null)
        {
            _contexto.PedidoProdutos.Remove(pedidoProduto);
            await _contexto.SaveChangesAsync();
            return StatusCode(200, new { Mensagem = "pedido apagado com sucesso" });
        }
        return StatusCode(404, new { Mensagem = "Pedido não encontrado" });
    }
}