using AutoMapper;
using CadeteriaWeb.Interfaces;
using CadeteriaWeb.Models;
using CadeteriaWeb.Repositorios;
using CadeteriaWeb.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CadeteriaWeb.Controllers
{
    public class ClienteController : Controller
    {
        private IClienteRepositorio _clienteRepositorio;
        private IPedidosRepositorio _pedidosRepositorio;
        private readonly ILogger<ClienteController> _logger;
        private IMapper _mapper;

        public ClienteController(IClienteRepositorio clienteRepositorio, IPedidosRepositorio pedidosRepositorio, ILogger<ClienteController> logger, IMapper mapper)
        {
            _clienteRepositorio = clienteRepositorio;
            _pedidosRepositorio = pedidosRepositorio;
            _logger = logger;
            _mapper = mapper;
        }
        public ActionResult Listar()
        {
            try
            {
                var rol = HttpContext.Session.GetInt32("Rol");
                if (rol == null) { return RedirectToAction("Index", "Usuarios"); }
                if (rol != 1) { return RedirectToAction("Error", "Usuarios", new { error = 2 }); }
                var clientes = _clienteRepositorio.GetClientes().Where(cliente => cliente.Estado != 2).ToList<Cliente>();
                var listaClientesVM = _mapper.Map<List<ClienteViewModel>>(clientes);
                return View(listaClientesVM);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al intentar cargar listado de clientes. Mensaje de error: {e.Message}");
                return RedirectToAction("Error", "Usuarios", new { error = 7 });
            }

        }
        public IActionResult DarAltaCliente()
        {
            try
            {
                var rol = HttpContext.Session.GetInt32("Rol");
                if (rol == null) { return RedirectToAction("Index", "Usuarios"); }
                return View(new ClienteViewModel());
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al intentar dar de alta un cliente. Mensaje de error: {e.Message}");
                return RedirectToAction("Error", "Usuarios", new { error = 8 });
            }
        }

        [HttpPost]
        public IActionResult DarAltaCliente(ClienteViewModel clienteVM)
        {
            try
            {
                var rol = HttpContext.Session.GetInt32("Rol");
                if (ModelState.IsValid)
                {
                    var cliente = _mapper.Map<Cliente>(clienteVM);
                    _clienteRepositorio.AltaCliente(cliente);
                    if (rol == 1)
                    {
                        return RedirectToAction("Listar");
                    }
                    else
                    {
                        return RedirectToAction("DarAltaPedido", "Pedidos");
                    }
                }
                else
                {
                    return View("DarAltaCliente", clienteVM);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al intentar dar de alta un cliente. Mensaje de error: {e.Message}");
                return RedirectToAction("Error", "Usuarios", new { error = 8 });
            }
        }
        public IActionResult EditarCliente(int id)
        {
            try
            {
                var rol = HttpContext.Session.GetInt32("Rol");
                if (rol == null) { return RedirectToAction("Index", "Usuarios"); }
                if (rol != 1) { return RedirectToAction("Error", "Usuarios" , new { error = 2 }); }
                var clienteAEditar = _clienteRepositorio.GetClientePorId(id);
                var clienteVM = _mapper.Map<ClienteViewModel>(clienteAEditar);
                if (ModelState.IsValid)
                {
                    return View(clienteVM);
                }
                else
                {
                    return RedirectToAction("Listar");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al intentar editar un cliente. Mensaje de error: {e.Message}");
                return RedirectToAction("Error", "Usuarios", new { error = 9 });
            }
        }
        [HttpPost]
        public IActionResult EditarCliente(ClienteViewModel clienteVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cliente = _mapper.Map<Cliente>(clienteVM);
                    _clienteRepositorio.EditarCliente(cliente);
                    return RedirectToAction("Listar");
                }
                else
                {
                    return View("EditarCliente", clienteVM);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al intentar editar un cliente. Mensaje de error: {e.Message}");
                return RedirectToAction("Error", "Usuarios", new { error = 9 });
            }
        }
        public IActionResult DarBajaCliente(int id)
        {
            try
            {
                var rol = HttpContext.Session.GetInt32("Rol");
                if (rol == null) { return RedirectToAction("Index", "Usuarios"); }
                if (rol != 1) { return RedirectToAction("Error", "Usuarios", new { error = 2 }); }
                var clienteABorrar = _clienteRepositorio.GetClientePorId(id);
                var clienteVM = _mapper.Map<ClienteViewModel>(clienteABorrar);
                if (ModelState.IsValid)
                {
                    return View(clienteVM);
                }
                else
                {
                    return RedirectToAction("Listar");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al intentar eliminar un cliente. Mensaje de error: {e.Message}");
                return RedirectToAction("Error", "Usuarios", new { error = 10 });
            }
        }
        public IActionResult ConfirmarBaja(int id)
        {
            try
            {
                var rol = HttpContext.Session.GetInt32("Rol");
                if (rol == null) { return RedirectToAction("Index", "Usuarios"); }
                if (rol != 1) { return RedirectToAction("Error", "Usuarios" , new { error = 2 }); }
                _clienteRepositorio.BajaCliente(id);
                _pedidosRepositorio.CancelarPedidos(id);
                return Redirect("Listar");
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al intentar eliminar un cliente. Mensaje de error: {e.Message}");
                return RedirectToAction("Error", "Usuarios", new { error = 10 });
            }
        }
    }
}
