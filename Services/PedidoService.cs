
using desafio_dotnet.Contexto;
using desafio_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace desafio_dotnet.Services;

public class PedidoService
{


    public static async Task<List<Pedido>> BuscaPedidoPorAno(int ano, DbContexto contexto)
    {
       List<Pedido> pedidos = await contexto.Pedidos.ToListAsync();
       List<Pedido> novaLista = new List<Pedido>();
       foreach(Pedido pedido in pedidos)
       {
            DateTime dataParse = Convert.ToDateTime(pedido.Data);
            if( dataParse.Year == ano)
            {
                novaLista.Add(pedido);
            }
       }
       
       return novaLista;
    }

    public static async Task<int> TotalRegistros(DbContexto contexto)
    {
        return await contexto.Pedidos.CountAsync();
    }
}