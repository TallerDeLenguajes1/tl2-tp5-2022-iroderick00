using AutoMapper;
using CadeteriaWeb.Models;
using CadeteriaWeb.ViewModels;

namespace CadeteriaWeb
{
    public class PerfilDeMapeo : Profile
    {
        public PerfilDeMapeo()
        {
            CreateMap<Cadete, CadeteViewModel>().ReverseMap();
            CreateMap<Pedido, PedidoViewModel>().ReverseMap();
        }
    }
}
