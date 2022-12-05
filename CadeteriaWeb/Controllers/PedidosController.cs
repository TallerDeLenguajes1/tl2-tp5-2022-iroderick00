using AutoMapper;
using CadeteriaWeb.Models;
using CadeteriaWeb.Repositorios;
using CadeteriaWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CadeteriaWeb.Controllers
{
    public class PedidosController : Controller
    {
        private static PedidosRepositorio _pedidosRepositorio = new PedidosRepositorio();
        private readonly ILogger<PedidosController> _logger;
        private IMapper _mapper;

        public PedidosController(ILogger<PedidosController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }
        public IActionResult Listar()
        {
            var pedidos = _pedidosRepositorio.GetPedidos();
            var listaPedidosVM = _mapper.Map<List<PedidoViewModel>>(pedidos);
            return View(listaPedidosVM);
        }
        public IActionResult DarAltaPedido() 
        {
            return View(new PedidoViewModel()); 
        }

        [HttpPost]
        public IActionResult DarAltaPedido(PedidoViewModel pedidoVM)
        {
            if (ModelState.IsValid)
            {
                var pedido = _mapper.Map<Pedido>(pedidoVM);
                _pedidosRepositorio.AltaPedido(pedido);
                return RedirectToAction("Listar");
            }
            else
            {
                return View("DarAltaPedido",pedidoVM);
            }
        }
        public IActionResult EditarPedido(int id)
        {
            var pedidos = _pedidosRepositorio.GetPedidos();
            var pedidoAEditar = pedidos.Find(x => x.Id == id);
            var pedidoVM = _mapper.Map<PedidoViewModel>(pedidoAEditar);
            if (pedidoAEditar != null)
            {
                return View(pedidoVM);
            }
            else
            {
                return RedirectToAction("Listar");
            }
        }
        [HttpPost]
        public IActionResult EditarPedido(PedidoViewModel pedidoVM)
        {
            if (ModelState.IsValid)
            {
                var pedido = _mapper.Map<Pedido>(pedidoVM);
                _pedidosRepositorio.EditarPedido(pedido);
                return RedirectToAction("Listar");
            }
            else
            {
                return View("EditarPedido", pedidoVM);
            }
        }
        public IActionResult DarBajaPedido (int id)
        {
            var pedidos = _pedidosRepositorio.GetPedidos();
            var pedidoADarDeBaja = pedidos.Find(x => x.Id == id);
            var pedidoVM = _mapper.Map<PedidoViewModel>(pedidoADarDeBaja);
            if (ModelState.IsValid)
            {
                return View(pedidoVM);
            }
            else
            {
                return View("Listar");
            }
        }
        public RedirectResult ConfirmarBaja(int id)
        {
            _pedidosRepositorio.BajaPedido(id);
            return Redirect("Listar");
        }
    }
}
