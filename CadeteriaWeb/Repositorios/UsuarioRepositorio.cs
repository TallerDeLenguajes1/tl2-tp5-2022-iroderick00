using CadeteriaWeb.Interfaces;
using CadeteriaWeb.Models;
using Microsoft.Data.Sqlite;

namespace CadeteriaWeb.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly string cadenaConexion = "Data Source=DB/CadeteriaDB.db;Cache=Shared";
        public Usuario GetUsuario(string user, string pass)
        {
            try
            {
                var usuario = new Usuario();
                var query = "SELECT * FROM Usuarios WHERE user = @user AND pass = @pass";
                using (var connection = new SqliteConnection(cadenaConexion))
                {
                    connection.Open();
                    var command = new SqliteCommand(query, connection);
                    command.Parameters.Add(new SqliteParameter("@user", user));
                    command.Parameters.Add(new SqliteParameter("@pass", pass));

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usuario.IdUsuario = Convert.ToInt32(reader["id_usuario"]);
                            usuario.User = reader["user"].ToString();
                            usuario.Rol = Convert.ToInt32(reader["rol"]);
                            usuario.Nombre = reader["nombre"].ToString();
                        }
                    }
                    connection.Close();
                }
                return usuario;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void AltaUsuario(string user, string pass, int rol, string nombre)
        {
            try
            {
                var query = $"INSERT INTO Usuarios (user, pass, rol, nombre) VALUES (@user, @pass, @rol, @nombre)";
                using (var connection = new SqliteConnection(cadenaConexion))
                {

                    connection.Open();

                    var command = new SqliteCommand(query, connection);

                    command.Parameters.Add(new SqliteParameter("@user", user));
                    command.Parameters.Add(new SqliteParameter("@pass", pass));
                    command.Parameters.Add(new SqliteParameter("@rol", rol));
                    command.Parameters.Add(new SqliteParameter("@nombre", nombre));

                    command.ExecuteNonQuery();

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void BajaUsuario(int idUsuario)
        {
            try
            {
                var query = $"UPDATE Usuarios SET estado = 2  WHERE id_usuario = @idUsuario";
                using (var connection = new SqliteConnection(cadenaConexion))
                {
                    connection.Open();

                    var command = new SqliteCommand(query, connection);

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
    }
}
