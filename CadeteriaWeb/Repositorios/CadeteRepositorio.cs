using CadeteriaWeb.Interfaces;
using CadeteriaWeb.Models;
using Microsoft.Data.Sqlite;

namespace CadeteriaWeb.Repositorios
{
    public class CadeteRepositorio : ICadeteRepositorio
    {
        private readonly string cadenaConexion = "Data Source=DB/CadeteriaDB.db;Cache=Shared";
        public CadeteRepositorio() { }
        public List<Cadete> GetCadetes()
        {
            try
            {
                var cadetes = new List<Cadete>();
                var query = "SELECT * FROM Cadetes";
                using (var connection = new SqliteConnection(cadenaConexion))
                {
                    connection.Open();
                    var command = new SqliteCommand(query, connection);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var cadete = new Cadete();
                            cadete.Id = Convert.ToInt32(reader["id_cadete"]);
                            cadete.Nombre = reader["nombre"].ToString();
                            cadete.Direccion = reader["direccion"].ToString();
                            cadete.Telefono = reader["telefono"].ToString();
                            cadete.IdUsuario = Convert.ToInt32(reader["id_usuario"]);
                            cadete.Estado = Convert.ToInt32(reader["estado"]);
                            cadetes.Add(cadete);
                        }
                    }
                    connection.Close();
                }
                return cadetes;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Cadete GetCadetePorId(int idCadete)
        {
            try
            {
                var cadete = new Cadete();
                var query = "SELECT * FROM Cadetes WHERE id_cadete = @idCadete";
                using (var connection = new SqliteConnection(cadenaConexion))
                {
                    connection.Open();
                    var command = new SqliteCommand(query, connection);
                    command.Parameters.Add(new SqliteParameter("@idCadete", idCadete));

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cadete.Id = Convert.ToInt32(reader["id_cadete"]);
                            cadete.Nombre = reader["nombre"].ToString();
                            cadete.Direccion = reader["direccion"].ToString();
                            cadete.Telefono = reader["telefono"].ToString();
                            cadete.IdUsuario = Convert.ToInt32(reader["id_usuario"]);
                        }
                    }
                    connection.Close();
                }
                return cadete;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Cadete GetCadetePorIdUsuario(int idUsuario)
        {
            try
            {
                var cadete = new Cadete();
                var query = "SELECT * FROM Cadetes WHERE id_usuario = @idUsuario";
                using (var connection = new SqliteConnection(cadenaConexion))
                {
                    connection.Open();
                    var command = new SqliteCommand(query, connection);
                    command.Parameters.Add(new SqliteParameter("@idUsuario", idUsuario));

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cadete.Id = Convert.ToInt32(reader["id_cadete"]);
                            cadete.Nombre = reader["nombre"].ToString();
                            cadete.Direccion = reader["direccion"].ToString();
                            cadete.Telefono = reader["telefono"].ToString();
                            cadete.IdUsuario = Convert.ToInt32(reader["id_usuario"]);
                        }
                    }
                    connection.Close();
                }
                return cadete;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void AltaCadete(string nombre, string direccion, string telefono, int idUsuario)
        {
            try
            {
                var query = $"INSERT INTO Cadetes (nombre, direccion, telefono, id_usuario) VALUES (@nombre,@direccion,@telefono,@idUsuario)";
                using (var connection = new SqliteConnection(cadenaConexion))
                {

                    connection.Open();

                    var command = new SqliteCommand(query, connection);

                    command.Parameters.Add(new SqliteParameter("@nombre", nombre));
                    command.Parameters.Add(new SqliteParameter("@direccion", direccion));
                    command.Parameters.Add(new SqliteParameter("@telefono", telefono));
                    command.Parameters.Add(new SqliteParameter("@idUsuario", idUsuario));

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void AltaCadete(Cadete cadete)
        {
            try
            {
                var query = $"INSERT INTO Cadetes (nombre, direccion, telefono, id_usuario) VALUES (@nombre,@direccion,@telefono,@idUsuario)";
                using (var connection = new SqliteConnection(cadenaConexion))
                {

                    connection.Open();

                    var command = new SqliteCommand(query, connection);

                    command.Parameters.Add(new SqliteParameter("@nombre", cadete.Nombre));
                    command.Parameters.Add(new SqliteParameter("@direccion", cadete.Direccion));
                    command.Parameters.Add(new SqliteParameter("@telefono", cadete.Telefono));
                    command.Parameters.Add(new SqliteParameter("@idUsuario", cadete.IdUsuario));

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void EditarCadete(Cadete cadeteDatos)
        {
            try
            {
                var query = $"UPDATE Cadetes SET nombre = @nombre, direccion = @direccion, telefono = @telefono  WHERE id_cadete = @idCadete";
                using (var connection = new SqliteConnection(cadenaConexion))
                {

                    connection.Open();

                    var command = new SqliteCommand(query, connection);

                    command.Parameters.Add(new SqliteParameter("@idCadete", cadeteDatos.Id));
                    command.Parameters.Add(new SqliteParameter("@nombre", cadeteDatos.Nombre));
                    command.Parameters.Add(new SqliteParameter("@direccion", cadeteDatos.Direccion));
                    command.Parameters.Add(new SqliteParameter("@telefono", cadeteDatos.Telefono));

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void BajaCadete(int idCadete)
        {
            try
            {
                var query = $"UPDATE Cadetes SET estado = 2  WHERE id_cadete = @idCadete";
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
    }
}
