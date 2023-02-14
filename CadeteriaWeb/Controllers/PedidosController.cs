using AutoMapper;
using CadeteriaWeb.Interfaces;
using CadeteriaWeb.Models;
using CadeteriaWeb.Repositorios;
using CadeteriaWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CadeteriaWeb.Controllers
{
    public class PedidosController : Controller
    {
        private IPedidosRepositorio _pedidosRepositorio;
        private ICadeteRepositorio _cadeteRepositorio;
        private IClienteRepositorio _clienteRepositorio;
        private IUsuarioRepositorio _usuarioRepositorio;
        private readonly ILogger<PedidosController> _logger;
        private IMapper _mapper;

        public PedidosController(IPedidosRepositorio pedidosRepositorio, ICadeteRepositorio cadeteRepositorio, IClienteRepositorio clienteRepositorio, IUsuarioRepositorio usuarioRepositorio, ILogger<PedidosController> logger, IMapper mapper)
        {
            _pedidosRepositorio = pedidosRepositorio;
            _cadeteRepositorio = cadeteRepositorio;
            _clienteRepositorio = clienteRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Listar()
        {
            try
            {
                var rol = HttpContext.Session.GetInt32("Rol");
                if (rol == null) { return RedirectToAction("Index", "Usuarios"); }

                var id = HttpContext.Session.GetInt32("IdCadete") != null ? Convert.ToInt32(HttpContext.Session.GetInt32("IdCadete")) : -1;
                var pedidos = id == -1 ? _pedidosRepositorio.GetPedidos() : _pedidosRepositorio.GetPedidosPorIdCadete(id);
                var estados = _pedidosRepositorio.GetEstados();
                var clientes = _clienteRepositorio.GetClientes();
                var listaPedidosVM = _mapper.Map<List<PedidoViewModel>>(pedidos);
                if (id == -1)
                {
                    var cadetes = _cadeteRepositorio.GetCadetes();
                    listaPedidosVM.ForEach(pedido => pedido.NombreCadete = cadetes.Where(cadete => cadete.Id == pedido.IdCadete).FirstOrDefault().Nombre);
                }
                else
                {
                    var cadeteNombre = HttpContext.Session.GetString("Nombre");
                    listaPedidosVM.ForEach(pedido => pedido.NombreCadete = cadeteNombre);
                }
                listaPedidosVM.ForEach(pedido => pedido.EstadoDescripcion = estados.Where(estado => estado.IdEstado == pedido.EstadoId).FirstOrDefault().DescripcionEstado);
                listaPedidosVM.ForEach(pedido => pedido.NombreCliente = clientes.Where(cliente => cliente.Id == pedido.IdCliente).FirstOrDefault().Nombre);
                return View(listaPedidosVM);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al intentar cargar listado de pedidos. Mensaje de error: {e.Message}");
                return RedirectToAction("Error", "Usuarios", new { error = 11 });
            }
        }
        public IActionResult ListarPor(int entidad, int id)
        {
            try
            {
                var rol = HttpContext.Session.GetInt32("Rol");
                if (rol == null) { return RedirectToAction("Index", "Usuarios", new { error = 2 }); }
                var pedidos = new List<Pedido>();
                var listaPedidosVM = new List<PedidoViewModel>();
                if (entidad == 1)
                {
                    pedidos = _pedidosRepositorio.GetPedidosPorIdCadete(id);
                    var cadete = _cadeteRepositorio.GetCadetePorId(id);
                    var clientes = _clienteRepositorio.GetClientes();
                    listaPedidosVM.ForEach(pedido => pedido.NombreCadete = cadete.Nombre);
                    listaPedidosVM.ForEach(pedido => pedido.NombreCadete = cadete.Nombre);
                    listaPedidosVM.ForEach(pedido => pedido.NombreCliente = clientes.Where(cliente => cliente.Id == pedido.IdCliente).FirstOrDefault().Nombre);
                    ViewBag.nombre = "cadete " + cadete.Nombre;
                }
                else
                {
                    pedidos = _pedidosRepositorio.GetPedidosPorIdCliente(id);
                    var cliente = _clienteRepositorio.GetClientePorId(id);
                    var cadetes = _cadeteRepositorio.GetCadetes().Where(cadete => cadete.Estado != 2);
                    listaPedidosVM.ForEach(pedido => pedido.NombreCadete = cliente.Nombre);
                    listaPedidosVM.ForEach(pedido => pedido.NombreCadete = cadetes.Where(cadete => cadete.Id == pedido.IdCadete).FirstOrDefault().Nombre);
                    ViewBag.nombre = "cliente " + cliente.Nombre;

                }
                var estados = _pedidosRepositorio.GetEstados();
                listaPedidosVM = _mapper.Map<List<PedidoViewModel>>(pedidos);
                listaPedidosVM.ForEach(pedido => pedido.EstadoDescripcion = estados.Where(estado => estado.IdEstado == pedido.EstadoId).FirstOrDefault().DescripcionEstado);
                return View(listaPedidosVM);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al intentar cargar listado de pedidos por cadete o cliente. Mensaje de error: {e.Message}");
                return RedirectToAction("Error", "Usuarios", new { error = 11 });
            }
        }
        public IActionResult DarAltaPedido()
        {
            try
            {
                var rol = HttpContext.Session.GetInt32("Rol");
                if (rol == null) { return RedirectToAction("Index", "Usuarios"); }
                var cadetes = _cadeteRepositorio.GetCadetes().Where(cadete => cadete.Estado != 2).ToList();
                var clientes = _clienteRepositorio.GetClientes().Where(cliente => cliente.Estado != 2).ToList<Cliente>();

                var itemsCadetes = cadetes.ConvertAll(x =>
                {
                    return new SelectListItem()
                    {
                        Text = x.Nombre.ToString(),
                        Value = x.Id.ToString()
                    };
                });
                var itemsClientes = clientes.ConvertAll(x =>
                {
                    return new SelectListItem()
                    {
                        Text = x.Nombre.ToString(),
                        Value = x.Id.ToString()
                    };
                });

                ViewBag.cadetesDisponibles = itemsCadetes;
                ViewBag.clientesDisponibles = itemsClientes;

                return View(new PedidoViewModel());
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al intentar dar de alta un pedido. Mensaje de error: {e.Message}");
                return RedirectToAction("Error", "Usuarios", new { error = 12 });
            }
        }

        [HttpPost]
        public IActionResult DarAltaPedido(PedidoViewModel pedidoVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var pedido = _mapper.Map<Pedido>(pedidoVM);
                    _pedidosRepositorio.AltaPedido(pedido);

                    return RedirectToAction("Listar");
                }
                else
                {
                    return View("DarAltaPedido", pedidoVM);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al intentar dar de alta un pedido. Mensaje de error: {e.Message}");
                return RedirectToAction("Error", "Usuarios", new { error = 12 });
            }
        }
        public IActionResult EditarPedido(int id)
        {
            try
            {
                var rol = HttpContext.Session.GetInt32("Rol");
                if (rol == null) { return RedirectToAction("Index", "Usuarios", new { error = 2 }); }
                var pedido = _pedidosRepositorio.GetPedidoPorId(id);
                var cadetes = _cadeteRepositorio.GetCadetes().Where(cadete => cadete.Estado != 2).ToList();
                var clientes = _clienteRepositorio.GetClientes();

                var itemsCadetes = cadetes.ConvertAll(x =>
                {
                    return new SelectListItem()
                    {
                        Text = x.Nombre.ToString(),
                        Value = x.Id.ToString(),
                        Selected = x.Id == pedido.IdCadete
                    };
                });
                var itemsClientes = clientes.ConvertAll(x =>
                {
                    return new SelectListItem()
                    {
                        Text = x.Nombre.ToString(),
                        Value = x.Id.ToString(),
                        Selected = x.Id == pedido.IdCliente
                    };
                });
                ViewBag.cadetesDisponibles = itemsCadetes;
                ViewBag.clientesDisponibles = itemsClientes;
                var pedidoVM = _mapper.Map<PedidoViewModel>(pedido);
                if (ModelState.IsValid)
                {
                    return View(pedidoVM);
                }
                else
                {
                    return RedirectToAction("Listar");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al intentar editar un pedido. Mensaje de error: {e.Message}");
                return RedirectToAction("Error", "Usuarios", new { error = 13 });
            }
        }
        [HttpPost]
        public IActionResult EditarPedido(PedidoViewModel pedidoVM)
        {
            try
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
            catch (Exception e)
            {
                _logger.LogError($"Error al intentar editar un pedido. Mensaje de error: {e.Message}");
                return RedirectToAction("Error", "Usuarios", new { error = 13 });
            }
        }
        public IActionResult DarBajaPedido(int id)
        {
            try
            {
                var rol = HttpContext.Session.GetInt32("Rol");
                if (rol == null) { return RedirectToAction("Index", "Usuarios"); }
                if (rol != 1) { return RedirectToAction("Error", "Usuarios", new { error = 2 }); }
                var pedido = _pedidosRepositorio.GetPedidoPorId(id);
                var pedidoVM = _mapper.Map<PedidoViewModel>(pedido);
                if (ModelState.IsValid)
                {
                    return View(pedidoVM);
                }
                else
                {
                    return View("Listar");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al intentar cancelar un pedido. Mensaje de error: {e.Message}");
                return RedirectToAction("Error", "Usuarios", new { error = 13 });
            }
        }
        public IActionResult ConfirmarBaja(int id)
        {
            try
            {
                var rol = HttpContext.Session.GetInt32("Rol");
                if (rol == null) { return RedirectToAction("Index", "Usuarios"); }
                if (rol != 1) { return RedirectToAction("Error", "Usuarios", new { error = 2 }); }
                _pedidosRepositorio.BajaPedido(id);

                return Redirect("Listar");
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al intentar cancelar un pedido. Mensaje de error: {e.Message}");
                return RedirectToAction("Error", "Usuarios", new { error = 14 });
            }
        }
    }
}
