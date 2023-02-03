using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using CadeteriaWeb.Models;

namespace CadeteriaWeb.ViewModels
{
    public class PedidoViewModel
    {
        private Repositorios.CadeteRepositorio repositorio = new();
        private int id;
        private int? idCadete;
        private int? idCliente;
        private string observacion;
        private int estado;

        [Required]
        public int Id { get => id; set => id = value; }
        [Required]
        public int? IdCadete
        {
            get { return idCadete; }
            set
            {
                var cadetes = repositorio.GetCadetes();
                if (cadetes.Exists(x => x.Id == value))
                {
                    idCadete = value;
                }
                else
                {
                    idCadete = null;
                }
            }
        }
        [Required]
        public int? IdCliente { get => idCliente; set => idCliente = value; }
        [Required]
        public string Observacion { get => observacion; set => observacion = value; }
        [Required]
        public int Estado { get => estado; set => estado = value; }
    }
}