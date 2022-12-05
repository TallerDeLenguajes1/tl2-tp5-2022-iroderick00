using CadeteriaWeb.Models;

namespace CadeteriaWeb.Repositorios
{
    public class PedidosRepositorio
    {
        private static int numeroPedido = 1;
        private static List<Pedido> pedidos = new List<Pedido>();

        public List<Pedido> GetPedidos() { return pedidos; }
        public void AltaPedido(int idCadete, string observacion, int idCliente, string estado)
        {
            var id = numeroPedido;
            numeroPedido++;
            var nuevoPedido = new Pedido(id, idCadete, idCliente, observacion, estado);
            pedidos.Add(nuevoPedido);
        }
        public void AltaPedido(Pedido pedido)
        {
            var id = numeroPedido;
            numeroPedido++;
            if (pedido != null)
            {
                pedido.Id = id;
                pedidos.Add(pedido);
            }
        }
        public void EditarPedido(Pedido pedidoDatos)
        {
            var pedidoAEditar = pedidos.Find(x => x.Id == pedidoDatos.Id);
            if (pedidoAEditar != null)
            {
                pedidoAEditar.IdCadete = pedidoDatos.IdCadete;
                pedidoAEditar.IdCliente = pedidoDatos.IdCliente;
                pedidoAEditar.Estado = pedidoDatos.Estado;
                pedidoAEditar.Observacion = pedidoDatos.Observacion;
            }
        }
        public void BajaPedido(int id)
        {
            var PedidoABorrar = pedidos.Find(x => x.Id == id);
            if (PedidoABorrar != null)
            {
                pedidos.Remove(PedidoABorrar);
            }
        }
    }
}
