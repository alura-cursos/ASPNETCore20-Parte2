using CasaDoCodigo.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Queries
{
    public interface IPedidoQueries
    {
        Task<CarrinhoViewModel> GetCarrinho(int pedidoId);
    }
}
