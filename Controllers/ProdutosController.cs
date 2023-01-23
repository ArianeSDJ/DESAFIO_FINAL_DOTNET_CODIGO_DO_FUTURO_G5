using desafio_dotnet.Contexto;
using desafio_dotnet.Models;
using desafio_dotnet.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace desafio_dotnet.Controllers;

[Route("produtos")]
public class ProdutosController : ControllerBase
{
    private readonly DbContexto _contexto;
    public ProdutosController(DbContexto contexto)
    {
        _contexto = contexto;
    }

    [HttpGet("")]
    public async Task<IActionResult> Lista([FromQuery] int page = 1, [FromQuery] int take = 20)
    {
        int total = await _contexto.Produtos.CountAsync();
        if (total == 0) return StatusCode(200, new ListarRetorno<Produto> { Mensagem = "Ainda não há Produtos cadastrados", TotalRegistros=total});

        int maximoPaginas = (total / take) + 1;
        if(page>maximoPaginas) return StatusCode(404, new ListarRetorno<Produto> {Mensagem="Essa pagina não existe", MaximoPaginas=maximoPaginas});
        
        try
        {
            int pagina = (page - 1) * take;
            List<Produto> produtos = await _contexto.Produtos
                .Skip(pagina)
                .Take(take)
                .ToListAsync();
            return StatusCode(200, new ListarRetorno<Produto> { TotalRegistros = total, PaginaAtual = page, MaximoPaginas = maximoPaginas, Dados = produtos });
        }
        catch
        {
            return StatusCode(400, new ListarRetorno<Produto> {Mensagem = "ALgo deu errado"});
        }
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
        _contexto.Add(produtoNovo);
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