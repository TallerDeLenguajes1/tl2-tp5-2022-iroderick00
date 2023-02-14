using AutoMapper;
using CadeteriaWeb.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CadeteriaWeb.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ILogger<UsuariosController> _logger;
        private IUsuarioRepositorio _usuarioRepositorio;
        private ICadeteRepositorio _cadeteRepositorio;

        public UsuariosController(ILogger<UsuariosController> logger, IUsuarioRepositorio usuarioRepositorio, ICadeteRepositorio cadeteRepositorio)
        {
            _logger = logger;
            _usuarioRepositorio = usuarioRepositorio;
            _cadeteRepositorio = cadeteRepositorio;
        }
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("Usuario") != null) { return RedirectToAction("Bienvenida"); }

            return View();
        }
        [HttpPost]
        public IActionResult LogIn(string user, string pass)
        {
            try
            {
                var usuario = _usuarioRepositorio.GetUsuario(user, pass);
                if (!string.IsNullOrEmpty(usuario.Nombre))
                {
                    HttpContext.Session.SetInt32("IdUsuario", usuario.IdUsuario);
                    HttpContext.Session.SetString("Usuario", usuario.User);
                    HttpContext.Session.SetString("Nombre", usuario.Nombre);
                    HttpContext.Session.SetInt32("Rol", usuario.Rol);
                    if (usuario.Rol == 2)
                    {
                        var idUsuario = usuario.IdUsuario;
                        var cadete = _cadeteRepositorio.GetCadetePorIdUsuario(idUsuario);
                        HttpContext.Session.SetInt32("IdCadete", cadete.Id);
                    }
                    _logger.LogInformation($"Inicio de sesión exitoso. Usuario {HttpContext.Session.GetString("Usuario")}");
                    return RedirectToAction("Bienvenida");
                }
                else
                {
                    _logger.LogInformation("Inicio de sesión fallido");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al intentar iniciar sesión. Mensaje de error: {e.Message}");
                return RedirectToAction("Error", new { error = 1 });
            }
        }
        public IActionResult Bienvenida()
        {
            return View();
        }
        public IActionResult LogOut()
        {
            _logger.LogInformation($"Cierre de sesión exitoso. Usuario {HttpContext.Session.GetString("Usuario")}");
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        public IActionResult Error(int error)
       {
            switch (error)
            {
                case 1:
                    ViewBag.mensaje = "Error intentando iniciar sesión.";
                    break;
                case 2:
                    ViewBag.mensaje = "Error relacionado con los roles del usuario. No tiene permisos para realizar esa acción.";
                    break;
                case 3:
                    ViewBag.mensaje = "Error intentando listar los cadetes.";
                    break;
                case 4:
                    ViewBag.mensaje = "Error intentar dar de alta un cadete.";
                    break;
                case 5:
                    ViewBag.mensaje = "Error intentando editar los datos de un cadete.";
                    break;
                case 6:
                    ViewBag.mensaje = "Error intentando eliminar un cadete.";
                    break;
                case 7:
                    ViewBag.mensaje = "Error intentando listar los clientes.";
                    break;
                case 8:
                    ViewBag.mensaje = "Error intentando dar de alta un cliente.";
                    break;
                case 9:
                    ViewBag.mensaje = "Error intentando editar los datos de un cliente.";
                    break;
                case 10:
                    ViewBag.mensaje = "Error intentando dar de baja un cliente.";
                    break;
                case 11:
                    ViewBag.mensaje = "Error intentando listar los pedidos.";
                    break;
                case 12:
                    ViewBag.mensaje = "Error intentando dar de alta un pedido.";
                    break;
                case 13:
                    ViewBag.mensaje = "Error intentando editar los datos de un pedido.";
                    break;
                case 14:
                    ViewBag.mensaje = "Error intentando eliminar un pedido.";
                    break;
                default:
                    ViewBag.mensaje = "Error desconocido.";
                    break;
            }
            return View();
        }
    }
}
