using desafio_dotnet.DTOs;
using desafio_dotnet.Contexto;
using desafio_dotnet.Models;
using desafio_dotnet.ModelView;
using desafio_dotnet.Services;
using desafiodotnet.Migrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace desafio_dotnet.Controllers;

[Route("posicoes-produtos")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PosicoesProdutosController : ControllerBase
{
    private readonly DbContexto _contexto;
    public PosicoesProdutosController(DbContexto contexto)
    {
        _contexto = contexto;
    }

    [HttpGet("")]
    public async Task<IActionResult> Lista([FromQuery] int page = 1, [FromQuery] int take = 20)
    {
        int total = await _contexto.PosicoesProdutos.CountAsync();
        if (total == 0) return StatusCode(200, new ListarRetorno<PosicaoProduto> { Mensagem = "Ainda não há Posições de Produtos cadastradas", TotalRegistros = total });

        int maximoPaginas = (total / take) + 1;
        if (page > maximoPaginas) return StatusCode(404, new ListarRetorno<PosicaoProduto> { Mensagem = "Essa pagina não existe", MaximoPaginas = maximoPaginas });

        try
        {
            int pagina = (page - 1) * take;
            List<PosicaoProduto> posicaoProdutos = await _contexto.PosicoesProdutos
                .Skip(pagina)
                .Take(take)
                .ToListAsync();
            return StatusCode(200, new ListarRetorno<PosicaoProduto> { TotalRegistros = total, PaginaAtual = page, MaximoPaginas = maximoPaginas, Dados = posicaoProdutos });
        }
        catch
        {
            return StatusCode(400, new ListarRetorno<PosicaoProduto> { Mensagem = "ALgo deu errado" });
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Detalhes([FromRoute] int id)
    {
        var posicaoProdutos = await _contexto.PosicoesProdutos.FindAsync(id);
        if (posicaoProdutos is not null)
        {
            return StatusCode(200, posicaoProdutos);
        }
        return StatusCode(404, new { Mensagem = "não encontrada" });
    }
    [HttpPost("")]
    public async Task<IActionResult> Novo([FromBody] PosicaoProduto posicaoProdutoNova)
    {
        var posicaoProdutos = DtoBuilder<PosicaoProduto>.Builder(posicaoProdutoNova);
        _contexto.Add(posicaoProdutos);
        await _contexto.SaveChangesAsync();
        return StatusCode(201, posicaoProdutoNova);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Atualiza([FromRoute] int id, [FromBody] PosicaoProduto posicaoProdutoNova)
    {
        if (id != posicaoProdutoNova.Id)
        {
            return StatusCode(404, new { Mensagem = "Produto não atualizado" });
        }

        _contexto.Entry(posicaoProdutoNova).State = EntityState.Modified;
        await _contexto.SaveChangesAsync();

        return StatusCode(200, posicaoProdutoNova);
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
        return StatusCode(200, new { Mensagem = $"Posicao de produtos apagada com sucesso" });
    }
}