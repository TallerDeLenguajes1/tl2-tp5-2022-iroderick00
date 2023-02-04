using CadeteriaWeb.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Internal;

namespace CadeteriaWeb.Repositorios
{
    public class PedidosRepositorio : IPedidosRepositorio
    {
        private readonly string cadenaConexion = "Data Source=DB/CadeteriaDB.db;Cache=Shared";

        public List<Pedido> GetPedidos() 
        {
            try
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
            catch (Exception)
            {
                return new List<Pedido>();
            }

        }
        public Pedido GetPedidoPorId(int idPedido)
        {
            try
            {
                var pedido = new Pedido();
                var query = "SELECT * FROM Pedidos WHERE id_pedido = @idPedido";
                using (var connection = new SqliteConnection(cadenaConexion))
                {
                    connection.Open();
                    var command = new SqliteCommand(query, connection);
                    command.Parameters.Add(new SqliteParameter("@idPedido", idPedido));

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            pedido.Id = Convert.ToInt32(reader["id_pedido"]);
                            pedido.IdCadete = Convert.ToInt32(reader["id_cadete"]);
                            pedido.IdCliente = Convert.ToInt32(reader["id_cliente"]);
                            pedido.Observacion = reader["observacion"].ToString();
                            pedido.Estado = Convert.ToInt32(reader["estado"]);
                        }
                    }
                    connection.Close();
                }
                return pedido;
            }
            catch (Exception)
            {
                return new Pedido();
            }
        }
        public void AltaPedido(int idCadete, int idCliente, string observacion, int estado)
        {
            try
            {
                var query = $"INSERT INTO Pedidos (id_cadete, id_cliente, observacion, estado) VALUES (@idCadete,@idCliente,@observacion,@estado)";
                using (var connection = new SqliteConnection(cadenaConexion))
                {

                    connection.Open();

                    var command = new SqliteCommand(query, connection);

                    command.Parameters.Add(new SqliteParameter("@idCadete", idCadete));
                    command.Parameters.Add(new SqliteParameter("@idCliente", idCliente));
                    command.Parameters.Add(new SqliteParameter("@observacion", observacion));
                    command.Parameters.Add(new SqliteParameter("@estado", estado));

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception)
            {

            }
        }
        public void AltaPedido(Pedido pedido)
        {
            try
            {
                var query = $"INSERT INTO Pedidos (id_cadete, id_cliente, observacion, estado) VALUES (@idCadete,@idCliente,@observacion,@estado)";
                using (var connection = new SqliteConnection(cadenaConexion))
                {

                    connection.Open();

                    var command = new SqliteCommand(query, connection);

                    command.Parameters.Add(new SqliteParameter("@idCadete", pedido.IdCadete));
                    command.Parameters.Add(new SqliteParameter("@idCliente", pedido.IdCliente));
                    command.Parameters.Add(new SqliteParameter("@observacion", pedido.Observacion));
                    command.Parameters.Add(new SqliteParameter("@estado", pedido.Estado));

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception)
            {

            }
        }
        public void EditarPedido(Pedido pedidoDatos)
        {
            try
            {
                var query = $"UPDATE Pedidos SET nombre = @nombre, direccion = @direccion, telefono = @telefono  WHERE id_pedido = @idPedido";
                using (var connection = new SqliteConnection(cadenaConexion))
                {

                    connection.Open();

                    var command = new SqliteCommand(query, connection);

                    command.Parameters.Add(new SqliteParameter("@idPedido", pedidoDatos.Id));
                    command.Parameters.Add(new SqliteParameter("@idCadete", pedidoDatos.IdCadete));
                    command.Parameters.Add(new SqliteParameter("@idCliente", pedidoDatos.IdCliente));
                    command.Parameters.Add(new SqliteParameter("@observacion", pedidoDatos.Observacion));
                    command.Parameters.Add(new SqliteParameter("@estado", pedidoDatos.Estado));
                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception)
            {

            }
        }
        public void BajaPedido(int idPedido)
        {
            try
            {
                var query = $"UPDATE Pedidos SET estado = 4  WHERE id_pedido = @idPedido";
                using (var connection = new SqliteConnection(cadenaConexion))
                {
                    connection.Open();

                    var command = new SqliteCommand(query, connection);

                    command.Parameters.Add(new SqliteParameter("@idPedido", idPedido));

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
