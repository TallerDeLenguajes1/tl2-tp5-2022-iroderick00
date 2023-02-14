using Microsoft.Build.Framework;

namespace CadeteriaWeb.ViewModels
{
    public class ListarPedidosViewModel : PedidoViewModel
    {
        private string cadete;
        private string cliente;
        private string estadoDesc;

        [Required]
        public string Cadete { get => cadete; set => cadete = value; }
        public string Cliente { get => cliente; set => cliente = value; }
        public string Estado { get => estadoDesc; set => estadoDesc = value; }
    }
}
