

using desafio_dotnet.Contexto;
using desafio_dotnet.DTOs;
using desafio_dotnet.Models;
using desafio_dotnet.ModelView;
using desafio_dotnet.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace desafio_dotnet.Controllers;

[Route("lojas")]
public class LojasController : ControllerBase
{
    private readonly DbContexto _contexto;
    public LojasController(DbContexto contexto)
    {
        _contexto = contexto;
    }

    [HttpGet("")]
    public async Task<IActionResult> Lista([FromQuery] int page = 1, [FromQuery] int take = 20)
    {
        int total = await _contexto.Lojas.CountAsync();
        if (total == 0) return StatusCode(200, new ListarRetorno<Loja> { Mensagem = "Ainda não há Lojas cadastradas", TotalRegistros=total});

        int maximoPaginas = (total / take) + 1;
        if(page>maximoPaginas) return StatusCode(404, new ListarRetorno<Loja> {Mensagem="Essa pagina não existe", MaximoPaginas=maximoPaginas});
        
        try
        {
            int pagina = (page - 1) * take;
            List<Loja> lojas = await _contexto.Lojas
                .Skip(pagina)
                .Take(take)
                .ToListAsync();
            return StatusCode(200, new ListarRetorno<Loja> { TotalRegistros = total, PaginaAtual = page, MaximoPaginas = maximoPaginas, Dados = lojas });
        }
        catch
        {
            return StatusCode(400, new ListarRetorno<Loja> {Mensagem = "ALgo deu errado"});
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Detalhes([FromRoute] int id)
    {
        var loja = await _contexto.Lojas.FindAsync(id);
        if (loja is not null)
        {
            return StatusCode(200, loja);
        }
        return StatusCode(404, new { Mensagem = "Loja não encontrada" });
    }
    [HttpPost("")]
    public async Task<IActionResult> Novo([FromBody] LojaDto lojaNova)
    {
        var loja = DtoBuilder<Loja>.Builder(lojaNova);
        _contexto.Add(loja);
        await _contexto.SaveChangesAsync();
        return StatusCode(201, lojaNova);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Atualiza([FromRoute] int id, [FromBody] Loja lojaAtualizada)
    {

        if (id != lojaAtualizada.Id)
        {
            return StatusCode(404, new { Mensagem = "Loja não encontrada"});
        }

        _contexto.Entry(lojaAtualizada).State = EntityState.Modified;
        await _contexto.SaveChangesAsync();

        return StatusCode(200, lojaAtualizada);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deleta([FromRoute] int id)
    {
        var loja = await _contexto.Lojas.FindAsync(id);
        if (loja is not null)
        {
            _contexto.Lojas.Remove(loja);
        }
        await _contexto.SaveChangesAsync();
        return StatusCode(200, new { Mensagem = $"Loja {loja?.Nome} apagada com sucesso" });
    }
}