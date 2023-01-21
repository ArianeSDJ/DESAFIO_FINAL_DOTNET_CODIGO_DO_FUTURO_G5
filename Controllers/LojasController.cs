

using desafio_dotnet.Contexto;
using desafio_dotnet.Models;
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
    public async Task<IActionResult> Lista()
    {
        List<Loja> lojas = await _contexto.Lojas.ToListAsync();
        return StatusCode(200, lojas);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Detalhes([FromRoute] int id)
    {
        var loja = await _contexto.Lojas.FindAsync(id);
        if (loja is not null)
        {
             return StatusCode(200, loja);
        }
        return StatusCode(404, new { Mensagem = "Loja não encontrada"});
    }
    [HttpPost("")]
    public async Task<IActionResult> Novo([FromBody] Loja lojaNova)
    {
        _contexto.Add(lojaNova);
        await _contexto.SaveChangesAsync();
        return StatusCode(201, lojaNova);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Atualiza([FromRoute] int id, [FromBody] Loja lojaAtualizda)
    {
        var loja = await _contexto.Lojas.FindAsync(id);
        if(loja is not null)
        {
            loja.bairro = lojaAtualizda.bairro;
            loja.cep = lojaAtualizda.cep;
            loja.cidade = lojaAtualizda.cidade;
            loja.complemento = lojaAtualizda.complemento;
            loja.estado = lojaAtualizda.estado;
            loja.latitude = lojaAtualizda.latitude;
            loja.logradouro = lojaAtualizda.logradouro;
            loja.longitude = lojaAtualizda.longitude;
            loja.nome = lojaAtualizda.nome;
            loja.numero = lojaAtualizda.numero;
            loja.observacao = lojaAtualizda.observacao;
            await _contexto.SaveChangesAsync();
        }
        return StatusCode(200, loja);
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
        return StatusCode(200, new {Mensagem=$"Loja {loja?.nome} apagada com sucesso"});
    }
}