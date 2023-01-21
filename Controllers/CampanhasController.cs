

using desafio_dotnet.Contexto;
using desafio_dotnet.DTOs;
using desafio_dotnet.Models;
using desafio_dotnet.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace desafio_dotnet.Controllers;

[Route("campanhas")]
public class CampanhasController : ControllerBase
{
    private readonly DbContexto _contexto;
    public CampanhasController(DbContexto contexto)
    {
        _contexto = contexto;
    }

    [HttpGet("")]
    public async Task<IActionResult> Lista()
    {
        List<Campanha> campanhas = await _contexto.Campanhas.ToListAsync();
        return StatusCode(200, campanhas);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Detalhes([FromRoute] int id)
    {
        var campanha = await _contexto.Campanhas.FindAsync(id);
        if (campanha is not null)
        {
             return StatusCode(200, campanha);
        }
        return StatusCode(404, new { Mensagem = "Campanha não encontrada"});
    }
    [HttpPost("")]
    public async Task<IActionResult> Novo([FromBody] CampanhaDto campanhaNova)
    {
        var campanha = DtoBuilder<Campanha>.Builder(campanhaNova);
        _contexto.Add(campanha);
        await _contexto.SaveChangesAsync();
        return StatusCode(201, campanhaNova);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Atualiza([FromRoute] int id, [FromBody] CampanhaDto campanhaAtualizda)
    {
        var campanha = await _contexto.Campanhas.FindAsync(id);
        if(campanha is not null)
        {
            campanha = DtoBuilder<Campanha>.Builder(campanhaAtualizda);
            await _contexto.SaveChangesAsync();
            return StatusCode(200, campanha);
        }
        return StatusCode(404, new { Mensagem = "Campanha não encontrada"});

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deleta([FromRoute] int id)
    {
        var campanha = await _contexto.Campanhas.FindAsync(id);
        if (campanha is not null)
        {
            _contexto.Campanhas.Remove(campanha);
            await _contexto.SaveChangesAsync();
        return StatusCode(200, new {Mensagem=$"campanha {campanha?.Nome} apagada com sucesso"});
        }
        return StatusCode(404, new { Mensagem = "Campanha não encontrada"});
    }
}