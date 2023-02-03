using CadeteriaWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadeteriaWeb.Repositorios
{
    public interface IPedidosRepositorio
    {
        public List<Pedido> GetPedidos();
    }
}
