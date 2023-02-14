using AutoMapper;
using CadeteriaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using CadeteriaWeb.Repositorios;
using CadeteriaWeb.ViewModels;
using Microsoft.AspNetCore.Session;
using CadeteriaWeb.Interfaces;

namespace CadeteriaWeb.Controllers
{
    public class CadeteController : Controller
    {
        private ICadeteRepositorio _cadeteRepositorio;
        private IPedidosRepositorio _pedidosRepositorio;
        private IUsuarioRepositorio _usuarioRepositorio;
        private readonly ILogger<CadeteController> _logger;
        private IMapper _mapper;

        public CadeteController(ICadeteRepositorio cadeteRepositorio, IPedidosRepositorio pedidosRepositorio, IUsuarioRepositorio usuarioRepositorio, ILogger<CadeteController> logger, IMapper mapper)
        {
            _cadeteRepositorio = cadeteRepositorio;
            _pedidosRepositorio = pedidosRepositorio;
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
                if (rol != 1) { return RedirectToAction("Error", "Usuarios", new { error = 2 }); }
                var cadetes = _cadeteRepositorio.GetCadetes().Where(cadete => cadete.Estado != 2);
                var listaCadetesVM = _mapper.Map<List<CadeteViewModel>>(cadetes);
                return View(listaCadetesVM);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al intentar cargar listado de cadetes. Mensaje de error: {e.Message}");
                return RedirectToAction("Error", "Usuarios", new { error = 3 });
            }

        }
        public IActionResult DarAltaCadete()
        {
            var rol = HttpContext.Session.GetInt32("Rol");
            if (rol == null) { return RedirectToAction("Index", "Usuarios"); }
            if (rol != 1) { return RedirectToAction("Error", "Usuarios", new { error = 2 }); }

            return View(new CadeteViewModel());
        }

        [HttpPost]
        public IActionResult DarAltaCadete(CadeteViewModel cadeteVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cadete = _mapper.Map<Cadete>(cadeteVM);
                    var aux = cadete.Nombre.Replace(" ", string.Empty).ToLower();
                    var nuevoUsuario = aux + "Cdt";
                    var nuevaContrasenia = aux + cadete.Telefono;
                    var nuevoRol = 2;
                    var nuevoNombre = cadete.Nombre;
                    _usuarioRepositorio.AltaUsuario(nuevoUsuario, nuevaContrasenia, nuevoRol, nuevoNombre);
                    var usuarioCreado = _usuarioRepositorio.GetUsuario(nuevoUsuario, nuevaContrasenia);
                    cadete.IdUsuario = usuarioCreado.IdUsuario;
                    _cadeteRepositorio.AltaCadete(cadete);
                    return RedirectToAction("Listar");
                }
                else
                {
                    return View("DarAltaCadete", cadeteVM);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al intentar dar de alta un cadete. Mensaje de error: {e.Message}");
                return RedirectToAction("Error", "Usuarios", new { error = 4 });
            }
        }
        public IActionResult EditarCadete(int id)
        {
            try
            {
                var rol = HttpContext.Session.GetInt32("Rol");
                if (rol == null) { return RedirectToAction("Index", "Usuarios"); }
                if (rol != 1) { return RedirectToAction("Error", "Usuarios", new { error = 2 }); }
                var cadeteAEditar = _cadeteRepositorio.GetCadetePorId(id);
                var cadeteVM = _mapper.Map<CadeteViewModel>(cadeteAEditar);
                if (ModelState.IsValid)
                {
                    return View(cadeteVM);
                }
                else
                {
                    return RedirectToAction("Listar");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al intentar editar un cadete. Mensaje de error: {e.Message}");
                return RedirectToAction("Error", "Usuarios", new { error = 5 });
            }
        }
        [HttpPost]
        public IActionResult EditarCadete(CadeteViewModel cadeteVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cadete = _mapper.Map<Cadete>(cadeteVM);
                    _cadeteRepositorio.EditarCadete(cadete);
                    return RedirectToAction("Listar");
                }
                else
                {
                    return View("EditarCadete", cadeteVM);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al intentar editar un cadete. Mensaje de error: {e.Message}");
                return RedirectToAction("Error", "Usuarios", new { error = 5 });
            }
        }
        public IActionResult DarBajaCadete(int id)
        {
            try
            {
                var rol = HttpContext.Session.GetInt32("Rol");
                if (rol == null) { return RedirectToAction("Index", "Usuarios"); }
                if (rol != 1) { return RedirectToAction("Error", "Usuarios", new { error = 2 }); }
                var cadeteABorrar = _cadeteRepositorio.GetCadetePorId(id);
                var cadeteVM = _mapper.Map<CadeteViewModel>(cadeteABorrar);
                if (ModelState.IsValid)
                {
                    return View(cadeteVM);
                }
                else
                {
                    return RedirectToAction("Listar");
                }
            }
            catch (Exception e) 
            { 
                _logger.LogError($"Error al intentar eliminar un cadete. Mensaje de error: {e.Message}"); 
                return RedirectToAction("Error", "Usuarios", new { error = 6 }); 
            }
        }
        public IActionResult ConfirmarBaja(int id)
        {
            try
            {
                var rol = HttpContext.Session.GetInt32("Rol");
                if (rol == null) { return RedirectToAction("Index", "Usuarios"); }
                if (rol != 1) { return RedirectToAction("Error", "Usuarios", new { error = 2 }); }
                var cadete = _cadeteRepositorio.GetCadetePorId(id);
                var idUsuario = cadete.IdUsuario;
                _cadeteRepositorio.BajaCadete(id);
                _pedidosRepositorio.LiberarPedidos(id);
                _usuarioRepositorio.BajaUsuario(idUsuario);
                return Redirect("Listar");
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al intentar eliminar un cadete. Mensaje de error: {e.Message}");
                return RedirectToAction("Error", "Usuarios", new { error = 6 });
            }
        }
    }
}
