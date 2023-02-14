namespace CadeteriaWeb.Models
{
    public class Usuario
    {
        private int idUsuario;
        private string user;
        private int rol;
        private string nombre;
        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public string User { get => user; set => user = value; }
        public int Rol { get => rol;set => rol = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public Usuario() { }
        public Usuario(int idUsuario, string user, int rol, string nombre) 
        {
            IdUsuario = idUsuario;
            User = user;
            Rol = rol;
            Nombre = nombre;
        }
    }
}
