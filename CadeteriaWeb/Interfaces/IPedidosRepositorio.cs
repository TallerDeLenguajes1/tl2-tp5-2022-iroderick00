using CadeteriaWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadeteriaWeb.Interfaces
{
    public interface IPedidosRepositorio
    {
        List<Pedido> GetPedidos();
        Pedido GetPedidoPorId(int idPedido);
        List<Pedido> GetPedidosPorIdCadete(int idCadete);
        List<Pedido> GetPedidosPorIdCliente(int idCliente);
        void AltaPedido(int idCadete, int idCliente, string observacion, int estado);
        void AltaPedido(Pedido pedido);
        void EditarPedido(Pedido pedidoDatos);
        void BajaPedido(int idPedido);
        void LiberarPedidos(int idCadete);
        void CancelarPedidos(int idCliente);
        List<EstadoPedidos> GetEstados();
    }
}
