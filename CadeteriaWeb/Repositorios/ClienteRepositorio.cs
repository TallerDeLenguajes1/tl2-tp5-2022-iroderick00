using CadeteriaWeb.Interfaces;
using CadeteriaWeb.Models;
using Microsoft.Data.Sqlite;

namespace CadeteriaWeb.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly string cadenaConexion = "Data Source=DB/CadeteriaDB.db;Cache=Shared";
        public List<Cliente> GetClientes()
        {
            try
            {
                List<Cliente> clientes = new List<Cliente>();
                var query = @"SELECT * FROM Clientes";
                using (var connection = new SqliteConnection(cadenaConexion))
                {
                    connection.Open();
                    var command = new SqliteCommand(query, connection);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cliente = new Cliente();
                            cliente.Id = Convert.ToInt32(reader["id_cliente"]);
                            cliente.Nombre = reader["nombre"].ToString();
                            cliente.Direccion = reader["direccion"].ToString();
                            cliente.Telefono = reader["telefono"].ToString();
                            cliente.ReferenciaDireccion = reader["referencia_direccion"].ToString();
                            cliente.Estado = Convert.ToInt32(reader["estado"]);
                            clientes.Add(cliente);
                        }
                    }
                    connection.Close();
                }
                return clientes;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Cliente GetClientePorId(int idCliente)
        {
            try
            {
                var cliente = new Cliente();
                var query = @"SELECT * FROM Clientes WHERE id_cliente = @idCliente";
                using (var connection = new SqliteConnection(cadenaConexion))
                {
                    connection.Open();
                    var command = new SqliteCommand(query, connection);
                    command.Parameters.Add(new SqliteParameter("@idCliente",idCliente));

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cliente.Id = Convert.ToInt32(reader["id_cliente"]);
                            cliente.Nombre = reader["nombre"].ToString();
                            cliente.Direccion = reader["direccion"].ToString();
                            cliente.Telefono = reader["telefono"].ToString();
                            cliente.ReferenciaDireccion = reader["referencia_direccion"].ToString();
                            cliente.Estado = Convert.ToInt32(reader["estado"]);
                        }
                    }
                    connection.Close();
                }
                return cliente;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void AltaCliente(string nombre, string direccion, string telefono, string referencia)
        {
            try
            {
                var query = $"INSERT INTO Clientes (nombre, direccion, telefono, referencia_direccion) VALUES (@nombre,@direccion,@telefono,@referencia)";
                using (var connection = new SqliteConnection(cadenaConexion))
                {

                    connection.Open();

                    var command = new SqliteCommand(query, connection);

                    command.Parameters.Add(new SqliteParameter("@nombre", nombre));
                    command.Parameters.Add(new SqliteParameter("@direccion", direccion));
                    command.Parameters.Add(new SqliteParameter("@telefono", telefono));
                    command.Parameters.Add(new SqliteParameter("@referencia", referencia));

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void AltaCliente(Cliente cliente)
        {
            try
            {
                var query = $"INSERT INTO Clientes (nombre, direccion, telefono, referencia_direccion) VALUES (@nombre,@direccion,@telefono,@referencia)";
                using (var connection = new SqliteConnection(cadenaConexion))
                {

                    connection.Open();

                    var command = new SqliteCommand(query, connection);

                    command.Parameters.Add(new SqliteParameter("@nombre", cliente.Nombre));
                    command.Parameters.Add(new SqliteParameter("@direccion", cliente.Direccion));
                    command.Parameters.Add(new SqliteParameter("@telefono", cliente.Telefono));
                    command.Parameters.Add(new SqliteParameter("@referencia", cliente.ReferenciaDireccion));

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void EditarCliente(Cliente clienteDatos)
        {
            try
            {
                var query = $"UPDATE Clientes SET nombre = @nombre, direccion = @direccion, telefono = @telefono, referencia_direccion = @referenciaDireccion WHERE id_cliente = @idCliente";
                using (var connection = new SqliteConnection(cadenaConexion))
                {

                    connection.Open();

                    var command = new SqliteCommand(query, connection);

                    command.Parameters.Add(new SqliteParameter("@idCliente", clienteDatos.Id));
                    command.Parameters.Add(new SqliteParameter("@nombre", clienteDatos.Nombre));
                    command.Parameters.Add(new SqliteParameter("@direccion", clienteDatos.Direccion));
                    command.Parameters.Add(new SqliteParameter("@telefono", clienteDatos.Telefono));
                    command.Parameters.Add(new SqliteParameter("@referenciaDireccion", clienteDatos.ReferenciaDireccion));

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void BajaCliente(int idCliente)
        {
            try
            {
                var query = $"UPDATE Clientes SET estado = 2  WHERE id_cliente = @idCliente";
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
    }
}
