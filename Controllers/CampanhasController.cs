
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

[Route("campanhas")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class CampanhasController : ControllerBase
{
    private readonly DbContexto _contexto;
    public CampanhasController(DbContexto contexto)
    {
        _contexto = contexto;
    }

    [HttpGet("")]
    public async Task<IActionResult> Lista([FromQuery] int page = 1, [FromQuery] int take = 20)
    {
        int total = await _contexto.Campanhas.CountAsync();
        if (total == 0) return StatusCode(200, new ListarRetorno<Campanha> { Mensagem = "Ainda não há campanhas cadastradas", TotalRegistros=total});

        int maximoPaginas = (total / take) + 1;
        if(page>maximoPaginas) return StatusCode(404, new ListarRetorno<Campanha> {Mensagem="Essa pagina não existe", MaximoPaginas=maximoPaginas});
        
        try
        {
            int pagina = (page - 1) * take;
            List<Campanha> campanhas = await _contexto.Campanhas
                .Skip(pagina)
                .Take(take)
                .ToListAsync();
            return StatusCode(200, new ListarRetorno<Campanha> { TotalRegistros = total, PaginaAtual = page, MaximoPaginas = maximoPaginas, Dados = campanhas });
        }
        catch
        {
            return StatusCode(400, new ListarRetorno<Campanha> {Mensagem = "Algo deu errado"});
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Detalhes([FromRoute] int id)
    {
        var campanha = await _contexto.Campanhas.FindAsync(id);
        if (campanha is not null)
        {
            return StatusCode(200, campanha);
        }
        return StatusCode(404, new { Mensagem = "Campanha não encontrada" });
    }
    [HttpPost("")]
    public async Task<IActionResult> Novo([FromBody] CampanhaDTO campanhaNova)
    {
        var campanha = DtoBuilder<Campanha>.Builder(campanhaNova);
        _contexto.Add(campanha);
        await _contexto.SaveChangesAsync();
        return StatusCode(201, campanhaNova);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Atualiza([FromRoute] int id, [FromBody] CampanhaDTO campanhaAtualizada)
    {
        if (id != campanhaAtualizada.Id)
        {
            return StatusCode(404, new { Mensagem = "Campanha não encontrada" });
        }

        _contexto.Entry(campanhaAtualizada).State = EntityState.Modified;
        await _contexto.SaveChangesAsync();

        return StatusCode(200, campanhaAtualizada);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deleta([FromRoute] int id)
    {
        var campanha = await _contexto.Campanhas.FindAsync(id);
        if (campanha is not null)
        {
            _contexto.Campanhas.Remove(campanha);
            await _contexto.SaveChangesAsync();
            return StatusCode(200, new { Mensagem = $"campanha {campanha?.Nome} apagada com sucesso" });
        }
        return StatusCode(404, new { Mensagem = "Campanha não encontrada" });
    }
}