

using desafio_dotnet.Contexto;
using desafio_dotnet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace desafio_dotnet.Controllers;

[Route("posicoes-produtos")]
public class PosicoesProdutosController : ControllerBase
{
    private readonly DbContexto _contexto;
    public PosicoesProdutosController(DbContexto contexto)
    {
        _contexto = contexto;
    }

    [HttpGet("")]
    public async Task<IActionResult> Lista()
    {
        List<PosicaoProduto> posicaoProdutos = await _contexto.PosicoesProdutos.ToListAsync();
        return StatusCode(200, posicaoProdutos);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Detalhes([FromRoute] int id)
    {
        var posicaoProdutos = await _contexto.PosicoesProdutos.FindAsync(id);
        if (posicaoProdutos is not null)
        {
             return StatusCode(200, posicaoProdutos);
        }
        return StatusCode(404, new { Mensagem = "não encontrada"});
    }
    [HttpPost("")]
    public async Task<IActionResult> Novo([FromBody] PosicaoProduto posicaoProdutoNova)
    {
        _contexto.Add(posicaoProdutoNova);
        await _contexto.SaveChangesAsync();
        return StatusCode(201, posicaoProdutoNova);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Atualiza([FromRoute] int id, [FromBody] PosicaoProduto posicaoProdutoNova)
    {
        var posicaoProdutos = await _contexto.PosicoesProdutos.FindAsync(id);
        if(posicaoProdutos is not null)
        {
            posicaoProdutos.CampanhaId = posicaoProdutoNova.CampanhaId;
            posicaoProdutos.PosicaoX = posicaoProdutoNova.PosicaoX;
            posicaoProdutos.PosicaoY = posicaoProdutoNova.PosicaoY;
            await _contexto.SaveChangesAsync();
        }
        return StatusCode(200, posicaoProdutos);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deleta([FromRoute] int id)
    {
        var posicaoProdutos = await _contexto.PosicoesProdutos.FindAsync(id);
        if (posicaoProdutos is not null)
        {
            _contexto.PosicoesProdutos.Remove(posicaoProdutos);
        }
        await _contexto.SaveChangesAsync();
        return StatusCode(200, new {Mensagem=$"Posicao de produtos apagada com sucesso"});
    }
}