using desafio_dotnet.Contexto;
using desafio_dotnet.DTOs;
using desafio_dotnet.Models;
using desafio_dotnet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace desafio_dotnet.Controllers;

[Route("clientes")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ClientesController : ControllerBase
{
    private readonly DbContexto _contexto;
    public ClientesController(DbContexto contexto)
    {
        _contexto = contexto;
    }

    [HttpGet("")]
    public async Task<IActionResult> Lista()
    {
        List<Cliente> clientes = await _contexto.Clientes.ToListAsync();
        return StatusCode(200, clientes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Detalhes([FromRoute] int id)
    {
        var cliente = await _contexto.Clientes.FindAsync(id);
        if (cliente is not null)
        {
            return StatusCode(200, cliente);
        }
        return StatusCode(404, new { Mensagem = "Cliente não encontrado" });
    }

    [HttpPost("")]
    public async Task<IActionResult> Novo([FromBody] ClienteDTO clienteNovo)
    {
        var cliente = DtoBuilder<Cliente>.Builder(clienteNovo);
        _contexto.Add(cliente);
        await _contexto.SaveChangesAsync();
        return StatusCode(201, clienteNovo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualiza([FromRoute] int id, [FromBody] Cliente clienteAtualizado)
    {
        if (id != clienteAtualizado.Id)
        {
            return StatusCode(404, new { Mensagem = "Cliente não encontrado"});
        }

        _contexto.Entry(clienteAtualizado).State = EntityState.Modified;
        await _contexto.SaveChangesAsync();

        return StatusCode(200, clienteAtualizado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deleta([FromRoute] int id)
    {
        var cliente = await _contexto.Clientes.FindAsync(id);
        if (cliente is not null)
        {
            _contexto.Clientes.Remove(cliente);
            await _contexto.SaveChangesAsync();
            return StatusCode(200, new { Mensagem = $"Cliente {cliente?.Nome} apagado com sucesso" });
        }
        return StatusCode(404, new { Mensagem = "Cliente não encontrado" });
    }


}