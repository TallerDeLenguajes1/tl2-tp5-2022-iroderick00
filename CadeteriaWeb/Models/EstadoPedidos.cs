namespace CadeteriaWeb.Models
{
    public class EstadoPedidos
    {
        private int idEstado;
        private string descripcionEstado;

        public int IdEstado { get => idEstado; set => idEstado = value; }
        public string DescripcionEstado { get => descripcionEstado; set => descripcionEstado = value; }
        public EstadoPedidos() { }
        public EstadoPedidos(int idEstado, string descripcionEstado)
        {
            IdEstado = idEstado;
            DescripcionEstado = descripcionEstado;
        }
    }
}
