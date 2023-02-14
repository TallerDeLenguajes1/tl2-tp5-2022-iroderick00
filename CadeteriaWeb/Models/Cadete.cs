namespace CadeteriaWeb.Models
{
    public class Cadete
    {
        private int id;
        private string nombre;
        private string direccion;
        private string telefono;
        private int idUsuario;
        private int estado;
        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set=> nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public int Estado { get => estado; set => estado = value; }
        public Cadete() { }
        public Cadete(int id, string nombre, string direccion, string telefono, int idUsuario, int estado)
        {
            Id = id;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            IdUsuario = idUsuario;
            Estado = estado;
        }
    }
}
