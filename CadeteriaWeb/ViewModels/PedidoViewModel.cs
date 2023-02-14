using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using CadeteriaWeb.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CadeteriaWeb.ViewModels
{
    public class PedidoViewModel
    {
        private int id;
        private int? idCadete;
        private string nombreCadete;
        private int? idCliente;
        private string nombreCliente;
        private string observacion;
        private int estadoId;
        private string estadoDescripcion;

        [Required]
        [DisplayName("Pedido")]
        public int Id { get => id; set => id = value; }
        [Required]
        [DisplayName("Cadete")]
        public int? IdCadete { get => idCadete; set => idCadete = value; }
        [DisplayName("Nombre del cadete")]
        [ValidateNever]
        public string NombreCadete { get => nombreCadete; set => nombreCadete = value; }
        [Required]
        [DisplayName("Cliente")]
        public int? IdCliente { get => idCliente; set => idCliente = value; }
        [DisplayName("Nombre de cliente")]
        [ValidateNever]
        public string NombreCliente { get => nombreCliente; set => nombreCliente = value; }
        [Required]
        [DisplayName("Observación")]
        public string Observacion { get => observacion; set => observacion = value; }
        [Required]
        [DisplayName("Estado")]
        public int EstadoId { get => estadoId; set => estadoId = value; }
        [DisplayName("Estado")]
        [ValidateNever]
        public string EstadoDescripcion { get => estadoDescripcion; set => estadoDescripcion = value;}
    }
}