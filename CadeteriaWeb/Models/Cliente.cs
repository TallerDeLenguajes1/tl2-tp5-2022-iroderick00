namespace CadeteriaWeb.Models
{
    public class Cliente
    {
        private int id;
        private int idPedido;
        private string nombre;
        private string direccion;
        private int telefono;
        private string referenciaDireccion;
        public int Id { get => id; set => id = value; }
        public int IdPedido { get => idPedido; set => idPedido = value; }
        public string Nombre { get => nombre; set =>  nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public int Telefono { get => telefono; set => telefono = value; }
        public string ReferenciaDireccion { get => referenciaDireccion; set => referenciaDireccion = value; }
        public Cliente() { }
        public Cliente(int id, int idPedido, string nombre, string direccion, int telefono, string referenciaDireccion)
        {
            Id = id;
            IdPedido = idPedido;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            ReferenciaDireccion = referenciaDireccion;
        }
    }
}
