using CadeteriaWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadeteriaWeb.Repositorios
{
    public interface IPedidosRepositorio
    {
        List<Pedido> GetPedidos();
        Pedido GetPedidoPorId(int idPedido);
        void AltaPedido(int idCadete, int idCliente, string observacion, int estado);
        void AltaPedido(Pedido pedido);
        void EditarPedido(Pedido pedidoDatos);
        void BajaPedido(int idPedido);
    }
}
