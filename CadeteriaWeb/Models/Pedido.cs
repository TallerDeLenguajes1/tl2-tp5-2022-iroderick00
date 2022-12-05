namespace CadeteriaWeb.Models
{
    public class Pedido
    {
        private int id;
        private int idCadete;
        private int idCliente;
        private string observacion;
        private string estado;
        public int Id { get => id; set => id = value; }
        public int IdCadete { get => idCadete; set => idCadete = value; }
        public string Observacion { get => observacion; set => observacion = value; }
        public int IdCliente { get => idCliente; set => idCliente = value; }
        public string Estado { get => estado; set => estado = value; }
        public Pedido() { }
        public Pedido(int id, int idCadete, int idCliente, string observacion, string estado)
        {
            Id = id;
            IdCadete = idCadete;
            IdCliente = idCliente;
            Observacion = observacion;
            Estado = estado;
        }
    }
}
