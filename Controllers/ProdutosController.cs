using desafio_dotnet.Contexto;
using desafio_dotnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace desafio_dotnet.Controllers;

[Route("produtos")]
public class ProdutosController : ControllerBase
{
    private readonly DbContexto _contexto;
    public ProdutosController(DbContexto contexto)
    {
        _contexto = contexto;
    }

    [HttpGet("")]
    public async Task<IActionResult> Lista()
    {
        List<Produto> produtos = await _contexto.Produtos.ToListAsync();
        return StatusCode(200, produtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Detalhes([FromRoute] int id)
    {
        var produto = await _contexto.Produtos.FindAsync(id);
        if (produto is not null)
        {
             return StatusCode(200, produto);
        }
        return StatusCode(404, new { Mensagem = "Produto não encontrado"});
    }

    [HttpPost("")]
    public async Task<IActionResult> Novo([FromBody] Produto produtoNovo)
    {
        _contexto.Add(produtoNovo);
        await _contexto.SaveChangesAsync();
        return StatusCode(201, produtoNovo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualiza([FromRoute] int id, [FromBody] Produto produtoAtualizado)
    {
        var produto = await _contexto.Produtos.FindAsync(id);
        if(produto is not null)
        {
            _contexto.Entry(produto).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
        return StatusCode(200, produto);
        }
        return StatusCode(404, new{Mensagem = "Produto não encontrado"});
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deleta([FromRoute] int id)
    {
        var produto = await _contexto.Produtos.FindAsync(id);
        if (produto is not null)
        {
            _contexto.Produtos.Remove(produto);
                await _contexto.SaveChangesAsync();
        return StatusCode(200, new {Mensagem=$"Produto {produto?.Nome} apagado com sucesso"});
        }
        return StatusCode(404, new{Mensagem = "Produto não encontrado"});
    }


}