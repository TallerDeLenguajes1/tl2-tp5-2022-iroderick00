using CadeteriaWeb.Models;
using Microsoft.Data.Sqlite;

namespace CadeteriaWeb.Repositorios
{
    public class PedidosRepositorio : IPedidosRepositorio
    {
        private readonly string cadenaConexion = "Data Source=DB/CadeteriaDB.db;Cache=Shared";

        public List<Pedido> GetPedidos() 
        {
            var pedidos = new List<Pedido>();
            var query = "SELECT * FROM Pedidos";
            using (var connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SqliteCommand(query, connection);

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var pedido = new Pedido
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            IdCadete = Convert.ToInt32(reader["id_cadete"]),
                            IdCliente = Convert.ToInt32(reader["id_cliente"]),
                            Observacion = reader["observacion"].ToString(),
                            Estado = Convert.ToInt32(reader["id_cliente"])
                        };
                        pedidos.Add(pedido);
                    }
                }
                connection.Close();
            }
            return pedidos;
        }
        public void AltaPedido(int idCadete, string observacion, int idCliente, string estado)
        {

        }
        public void AltaPedido(Pedido pedido)
        {

        }
        //public void EditarPedido(Pedido pedidoDatos)
        //{
        //    var pedidoAEditar = pedidos.Find(x => x.Id == pedidoDatos.Id);
        //    if (pedidoAEditar != null)
        //    {
        //        pedidoAEditar.IdCadete = pedidoDatos.IdCadete;
        //        pedidoAEditar.IdCliente = pedidoDatos.IdCliente;
        //        pedidoAEditar.Estado = pedidoDatos.Estado;
        //        pedidoAEditar.Observacion = pedidoDatos.Observacion;
        //    }
        //}
        //public void BajaPedido(int id)
        //{
        //    var PedidoABorrar = pedidos.Find(x => x.Id == id);
        //    if (PedidoABorrar != null)
        //    {
        //        pedidos.Remove(PedidoABorrar);
        //    }
        //}
    }
}
