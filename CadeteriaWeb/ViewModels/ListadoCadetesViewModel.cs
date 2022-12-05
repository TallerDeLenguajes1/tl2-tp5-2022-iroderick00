using CadeteriaWeb.Models;
using Microsoft.Build.Framework;

namespace CadeteriaWeb.ViewModels
{
    public class ListadoCadetesViewModel
    {
        private List<CadeteViewModel> cadetes;

        [Required]
        public List<CadeteViewModel> Cadetes { get => cadetes; set => cadetes = value; }
        public int cantidad
        {
            get => cadetes.Count();
        }
    }
}