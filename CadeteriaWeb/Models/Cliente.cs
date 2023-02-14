namespace CadeteriaWeb.Models
{
    public class Cliente
    {
        private int id;
        private string nombre;
        private string direccion;
        private string telefono;
        private string referenciaDireccion;
        private int estado;
        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set =>  nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public string ReferenciaDireccion { get => referenciaDireccion; set => referenciaDireccion = value; }
        public int Estado { get => estado; set => estado = value; }

        public Cliente() { }
        public Cliente(int id, int idPedido, string nombre, string direccion, string telefono, string referenciaDireccion, int estado)
        {
            Id = id;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            ReferenciaDireccion = referenciaDireccion;
            Estado = estado;
        }
    }
}
