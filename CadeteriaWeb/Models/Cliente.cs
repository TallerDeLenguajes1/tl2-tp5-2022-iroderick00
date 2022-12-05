namespace CadeteriaWeb.Models
{
    public class Cliente
    {
        private static int numeroCliente = 0;

        private int id;
        private string nombre;
        private string direccion;
        private int telefono;
        private string referenciaDireccion;
        public int Id { get => id; }
        public string Nombre { get => nombre; set =>  nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public int Telefono { get => telefono; set => telefono = value; }
        public string ReferenciaDireccion { get => referenciaDireccion; set => referenciaDireccion = value; }
        public Cliente() 
        {
            id = numeroCliente++;
        }
        public Cliente(string nombre, string direccion, int telefono, string referenciaDireccion)
        {
            id = numeroCliente++;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            ReferenciaDireccion = referenciaDireccion;
        }
    }
}
