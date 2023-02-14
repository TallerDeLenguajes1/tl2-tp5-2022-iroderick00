using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace CadeteriaWeb.ViewModels
{
    public class ClienteViewModel
    {
        private int id;
        private string nombre;
        private string direccion;
        private string telefono;
        private string referenciaDireccion;
        private int estado;

        [Required]
        [DisplayName("Cliente")]
        public int Id { get => id; set => id = value; }
        [Required]
        [DisplayName("Nombre")]
        public string Nombre { get => nombre; set => nombre = value; }
        [Required]
        public string Direccion { get => direccion; set => direccion = value; }
        [Required]
        [Phone]
        public string Telefono { get => telefono; set => telefono = value; }
        [Required]
        [DisplayName("Referencia")]
        public string ReferenciaDireccion { get => referenciaDireccion; set => referenciaDireccion = value; }
        public int Estado { get => estado; set => estado = value; }
    }
}
 