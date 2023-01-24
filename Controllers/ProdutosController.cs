using desafio_dotnet.Contexto;
using desafio_dotnet.Models;
using desafio_dotnet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace desafio_dotnet.Controllers;

[Route("produtos")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        var produto = DtoBuilder<Produto>.Builder(produtoNovo);
        _contexto.Add(produto);
        await _contexto.SaveChangesAsync();
        return StatusCode(201, produtoNovo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualiza([FromRoute] int id, [FromBody] Produto produtoAtualizado)
    {
        if (id != produtoAtualizado.Id)
        {
            return StatusCode(404, new { Mensagem = "Produto não encontrada"});
        }

        _contexto.Entry(produtoAtualizado).State = EntityState.Modified;
        await _contexto.SaveChangesAsync();

        return StatusCode(200, produtoAtualizado);
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