using desafio_dotnet.Contexto;
using desafio_dotnet.Models;
using desafio_dotnet.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace desafio_dotnet.Controllers;

[Route("clientes")]
public class ClientesController : ControllerBase
{
    private readonly DbContexto _contexto;
    public ClientesController(DbContexto contexto)
    {
        _contexto = contexto;
    }

    [HttpGet("")]
    public async Task<IActionResult> Lista([FromQuery] int page = 1, [FromQuery] int take = 20)
    {
        int total = await _contexto.Clientes.CountAsync();
        if (total == 0) return StatusCode(200, new ListarRetorno<Cliente> { Mensagem = "Ainda não há campanhas cadastradas", TotalRegistros=total});

        int maximoPaginas = (total / take) + 1;
        if(page>maximoPaginas) return StatusCode(404, new ListarRetorno<Cliente> {Mensagem="Essa pagina não existe", MaximoPaginas=maximoPaginas});
        
        try
        {
            int pagina = (page - 1) * take;
            List<Cliente> clientes = await _contexto.Clientes
                .Skip(pagina)
                .Take(take)
                .ToListAsync();
            return StatusCode(200, new ListarRetorno<Cliente> { TotalRegistros = total, PaginaAtual = page, MaximoPaginas = maximoPaginas, Dados = clientes });
        }
        catch
        {
            return StatusCode(400, new ListarRetorno<Campanha> {Mensagem = "ALgo deu errado"});
        }
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
    public async Task<IActionResult> Novo([FromBody] Cliente clienteNovo)
    {
        _contexto.Add(clienteNovo);
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