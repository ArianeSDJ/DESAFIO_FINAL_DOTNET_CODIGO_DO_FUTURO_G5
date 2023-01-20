

using desafio_dotnet.Contexto;
using desafio_dotnet.DTOs;
using desafio_dotnet.Models;
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
    public async Task<IActionResult> Novo([FromBody] LojaDto lojaNova)
    {
        var loja = DtoBuilder<Loja>.Builder(lojaNova);
        _contexto.Add(loja);
        await _contexto.SaveChangesAsync();
        return StatusCode(201, lojaNova);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Atualiza([FromRoute] int id, [FromBody] LojaDto lojaAtualizda)
    {
        var loja = await _contexto.Lojas.FindAsync(id);
        if(loja is not null)
        {
            loja.Bairro = lojaAtualizda.Bairro;
            loja.Cep = lojaAtualizda.Cep;
            loja.Cidade = lojaAtualizda.Cidade;
            loja.Complemento = lojaAtualizda.Complemento;
            loja.Estado = lojaAtualizda.Estado;
            loja.Latitude = lojaAtualizda.Latitude;
            loja.Logradouro = lojaAtualizda.Logradouro;
            loja.Longitude = lojaAtualizda.Longitude;
            loja.Nome = lojaAtualizda.Nome;
            loja.Numero = lojaAtualizda.Numero;
            loja.Observacao = lojaAtualizda.Observacao;
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
        return StatusCode(200, new {Mensagem=$"Loja {loja?.Nome} apagada com sucesso"});
    }
}