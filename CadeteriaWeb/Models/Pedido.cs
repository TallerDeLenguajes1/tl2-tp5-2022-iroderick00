namespace CadeteriaWeb.Models
{
    public class Pedido
    {
        private int id;
        private int idCadete;
        private int idCliente;
        private string observacion;
        private int estadoId;
        public int Id { get => id; set => id = value; }
        public int IdCadete { get => idCadete; set => idCadete = value; }
        public int IdCliente { get => idCliente; set => idCliente = value; }
        public string Observacion { get => observacion; set => observacion = value; }
        public int EstadoId { get => estadoId; set => estadoId = value; }
        public Pedido() { }
        public Pedido(int id, int idCadete, int idCliente, string observacion, int estadoId)
        {
            Id = id;
            IdCadete = idCadete;
            IdCliente = idCliente;
            Observacion = observacion;
            EstadoId = estadoId;
        }
    }
}
