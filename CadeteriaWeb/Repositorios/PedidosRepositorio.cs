using CadeteriaWeb.Interfaces;
using CadeteriaWeb.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Internal;
using System.Transactions;

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
                                Id = Convert.ToInt32(reader["id_pedido"]),
                                IdCadete = Convert.ToInt32(reader["id_cadete"]),
                                IdCliente = Convert.ToInt32(reader["id_cliente"]),
                                Observacion = reader["observacion"].ToString(),
                                EstadoId = Convert.ToInt32(reader["estado"])
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
                throw;
            }
        }
        public List<Pedido> GetPedidos(int? idCadete)
        {
            try
            {
                var pedidos = new List<Pedido>();
                var id = idCadete == null ? -1 : Convert.ToInt32(idCadete);
                var query = "";
                if (id == -1)
                {
                    query = "SELECT * FROM Pedidos";
                }
                else
                {
                    query = "SELECT * FROM Pedidos WHERE id_cadete = @idCadete";
                }
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
                                Id = Convert.ToInt32(reader["id_pedido"]),
                                IdCadete = Convert.ToInt32(reader["id_cadete"]),
                                IdCliente = Convert.ToInt32(reader["id_cliente"]),
                                Observacion = reader["observacion"].ToString()
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
                throw;
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
                            pedido.EstadoId = Convert.ToInt32(reader["estado"]);
                        }
                    }
                    connection.Close();
                }
                return pedido;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Pedido> GetPedidosPorIdCadete(int idCadete)
        {
            try
            {
                var pedidos = new List<Pedido>();
                var query = "SELECT * FROM Pedidos WHERE id_cadete = @idCadete";
                using (var connection = new SqliteConnection(cadenaConexion))
                {
                    connection.Open();
                    var command = new SqliteCommand(query, connection);
                    command.Parameters.Add(new SqliteParameter("@idCadete", idCadete));

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var pedido = new Pedido();
                            pedido.Id = Convert.ToInt32(reader["id_pedido"]);
                            pedido.IdCadete = Convert.ToInt32(reader["id_cadete"]);
                            pedido.IdCliente = Convert.ToInt32(reader["id_cliente"]);
                            pedido.Observacion = reader["observacion"].ToString();
                            pedido.EstadoId = Convert.ToInt32(reader["estado"]);
                            pedidos.Add(pedido);
                        }
                    }
                    connection.Close();
                }
                return pedidos;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Pedido> GetPedidosPorIdCliente(int idCliente)
        {
            try
            {
                var pedidos = new List<Pedido>();
                var query = "SELECT * FROM Pedidos WHERE id_cliente = @idCliente";
                using (var connection = new SqliteConnection(cadenaConexion))
                {
                    connection.Open();
                    var command = new SqliteCommand(query, connection);
                    command.Parameters.Add(new SqliteParameter("@idCliente", idCliente));

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var pedido = new Pedido();
                            pedido.Id = Convert.ToInt32(reader["id_pedido"]);
                            pedido.IdCadete = Convert.ToInt32(reader["id_cadete"]);
                            pedido.IdCliente = Convert.ToInt32(reader["id_cliente"]);
                            pedido.Observacion = reader["observacion"].ToString();
                            pedido.EstadoId = Convert.ToInt32(reader["estado"]);
                            pedidos.Add(pedido);
                        }
                    }
                    connection.Close();
                }
                return pedidos;
            }
            catch (Exception)
            {
                throw;
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
                throw;
            }
        }
        public void AltaPedido(Pedido pedido)
        {
            try
            {
                var query = $"INSERT INTO Pedidos (id_cadete, id_cliente, observacion) VALUES (@idCadete,@idCliente,@observacion)";
                using (var connection = new SqliteConnection(cadenaConexion))
                {

                    connection.Open();

                    var command = new SqliteCommand(query, connection);

                    command.Parameters.Add(new SqliteParameter("@idCadete", pedido.IdCadete));
                    command.Parameters.Add(new SqliteParameter("@idCliente", pedido.IdCliente));
                    command.Parameters.Add(new SqliteParameter("@observacion", pedido.Observacion));

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void EditarPedido(Pedido pedidoDatos)
        {
            try
            {
                var query = $"UPDATE Pedidos SET id_cadete = @idCadete, id_cliente = @idCliente, observacion = @observacion, estado = @estado  WHERE id_pedido = @idPedido";
                using (var connection = new SqliteConnection(cadenaConexion))
                {

                    connection.Open();

                    var command = new SqliteCommand(query, connection);

                    command.Parameters.Add(new SqliteParameter("@idPedido", pedidoDatos.Id));
                    command.Parameters.Add(new SqliteParameter("@idCadete", pedidoDatos.IdCadete));
                    command.Parameters.Add(new SqliteParameter("@idCliente", pedidoDatos.IdCliente));
                    command.Parameters.Add(new SqliteParameter("@observacion", pedidoDatos.Observacion));
                    command.Parameters.Add(new SqliteParameter("@estado", pedidoDatos.EstadoId));

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void BajaPedido(int idPedido)
        {
            try
            {
                var query = $"UPDATE Pedidos SET estado = 5  WHERE id_pedido = @idPedido";
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
                throw;
            }
        }
        public void LiberarPedidos(int idCadete)
        {
            try
            {
                var query = $"UPDATE Pedidos SET estado = 1  WHERE id_cadete = @idCadete";
                using (var connection = new SqliteConnection(cadenaConexion))
                {
                    connection.Open();

                    var command = new SqliteCommand(query, connection);

                    command.Parameters.Add(new SqliteParameter("@idCadete", idCadete));

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void CancelarPedidos(int idCliente)
        {
            try
            {
                var query = $"UPDATE Pedidos SET estado = 5  WHERE id_cliente = @idCliente";
                using (var connection = new SqliteConnection(cadenaConexion))
                {
                    connection.Open();

                    var command = new SqliteCommand(query, connection);

                    command.Parameters.Add(new SqliteParameter("@idCliente", idCliente));

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<EstadoPedidos> GetEstados()
        {
            try
            {
                var estados = new List<EstadoPedidos>();
                var query = "SELECT * FROM Estados_pedidos";
                using (var connection = new SqliteConnection(cadenaConexion))
                {
                    connection.Open();
                    var command = new SqliteCommand(query, connection);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var estado = new EstadoPedidos
                            {
                                IdEstado = Convert.ToInt32(reader["id_estado"]),
                                DescripcionEstado = reader["descripcion_estado"].ToString()
                            };
                            estados.Add(estado);
                        }
                    }
                    connection.Close();
                }
                return estados;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
