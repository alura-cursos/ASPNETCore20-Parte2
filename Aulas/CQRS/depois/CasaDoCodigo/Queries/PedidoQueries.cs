using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CasaDoCodigo.Queries
{
    public class PedidoQueries : IPedidoQueries
    {
        private readonly ConnectionStringsConfiguration databaseOptions;

        public PedidoQueries(IOptions<ConnectionStringsConfiguration> options)
        {
            this.databaseOptions = options.Value;
        }

        public async Task<CarrinhoViewModel> GetCarrinho(int pedidoId)
        {
            using (var connection = new SqlConnection(databaseOptions.Default))
            {
                connection.Open();

                var itens = await connection.QueryAsync<ItemPedidoViewModel>(
                    @"select
                    p.Id,
                    p.Nome as ProdutoNome,
                    PrecoUnitario,
                    Quantidade,
                    PrecoUnitario * Quantidade as Subtotal
                    from ItemPedido ip (nolock)
                    join Produto p (nolock)
	                    on p.Id = ip.ProdutoId
                    where ip.PedidoId = @pedidoId
                    order by ip.Id"
                    , new { pedidoId }
                );

                return new CarrinhoViewModel(itens.AsList());
            }
        }
    }
}
