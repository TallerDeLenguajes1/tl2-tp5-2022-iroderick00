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
            var cadetes = new List<Cadete>();
            var query = "SELECT * FROM Cadetes WHERE estado = 1";
            using (var connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SqliteCommand(query, connection);

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var cadete = new Cadete();
                        cadete.Id = Convert.ToInt32(reader["id"]);
                        cadete.Nombre = reader["nombre"].ToString();
                        cadete.Direccion = reader["direccion"].ToString();
                        cadete.Telefono = reader["telefono"].ToString();
                        cadetes.Add(cadete);
                    }
                }
                connection.Close();
            }
            return cadetes; 
        }
        public void AltaCadete(string nombre, string direccion, string telefono)
        {
            var query = $"INSERT INTO Cadetes (nombre, direccion, telefono) VALUES (@nombre,@direccion,@telefono)";
            using (var connection = new SqliteConnection(cadenaConexion))
            {

                connection.Open();

                var command = new SqliteCommand(query, connection);

                command.Parameters.Add(new SqliteParameter("@nombre", nombre));
                command.Parameters.Add(new SqliteParameter("@direccion", direccion));
                command.Parameters.Add(new SqliteParameter("@telefono", telefono));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public void AltaCadete(Cadete cadete)
        {
            var query = $"INSERT INTO Cadetes (nombre, direccion, telefono) VALUES (@nombre,@direccion,@telefono)";
            using (var connection = new SqliteConnection(cadenaConexion))
            {

                connection.Open();

                var command = new SqliteCommand(query, connection);

                command.Parameters.Add(new SqliteParameter("@nombre", cadete.Nombre));
                command.Parameters.Add(new SqliteParameter("@direccion", cadete.Direccion));
                command.Parameters.Add(new SqliteParameter("@telefono", cadete.Telefono));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public Cadete GetCadetePorId (int idCadete)
        {
            var cadete = new Cadete();
            var query = "SELECT * FROM Cadetes WHERE id = @idCadete";
            using (var connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();
                var command = new SqliteCommand(query, connection);
                command.Parameters.Add(new SqliteParameter("@idCadete", idCadete));

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cadete.Id = Convert.ToInt32(reader["id"]);
                        cadete.Nombre = reader["nombre"].ToString();
                        cadete.Direccion = reader["direccion"].ToString();
                        cadete.Telefono = reader["telefono"].ToString();
                    }
                }
                connection.Close();
            }
            return cadete;
        }
        public void EditarCadete(Cadete cadeteDatos)
        {
            var query = $"UPDATE Cadetes SET nombre = @nombre, direccion = @direccion, telefono = @telefono  WHERE id = @idCadete";
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
        public void BajaCadete(int idCadete)
        {
            var query = $"UPDATE Cadetes SET estado = 2  WHERE id = @idCadete";
            using (var connection = new SqliteConnection(cadenaConexion))
            {
                connection.Open();

                var command = new SqliteCommand(query, connection);

                command.Parameters.Add(new SqliteParameter("@idCadete", idCadete));

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
    }
}
