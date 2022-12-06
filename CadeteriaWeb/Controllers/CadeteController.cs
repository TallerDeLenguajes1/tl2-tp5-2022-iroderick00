using AutoMapper;
using CadeteriaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using CadeteriaWeb.Repositorios;
using CadeteriaWeb.ViewModels;

namespace CadeteriaWeb.Controllers
{
    public class CadeteController : Controller
    {
        private readonly ILogger<CadeteController> _logger;
        private IMapper _mapper;
        private ICadeteRepositorio _cadeteRepositorio;

        public CadeteController(ILogger<CadeteController> logger, IMapper mapper, ICadeteRepositorio cadeteRepositorio)
        {
            _logger = logger;
            _mapper = mapper;
            _cadeteRepositorio = cadeteRepositorio;
        }
        public IActionResult Listar()
        {
            var cadetes = _cadeteRepositorio.GetCadetes();
            var listaCadetesVM = _mapper.Map<List<CadeteViewModel>>(cadetes);
            return View(listaCadetesVM);
        }
        public IActionResult DarAltaCadete()
        {
            return View(new CadeteViewModel());
        }

        [HttpPost]
        public IActionResult DarAltaCadete(CadeteViewModel cadeteVM)
        {
            if (ModelState.IsValid)
            {
                var cadete = _mapper.Map<Cadete>(cadeteVM);
                _cadeteRepositorio.AltaCadete(cadete);
                return RedirectToAction("Listar");
            }
            else
            {
                return View("DarAltaCadete", cadeteVM);
            }
        }
        public IActionResult EditarCadete(int id)
        {
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
        [HttpPost]
        public IActionResult EditarCadete(CadeteViewModel cadeteVM)
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
        public IActionResult DarBajaCadete(int id)
        {
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
        public RedirectResult ConfirmarBaja(int id)
        {
            _cadeteRepositorio.BajaCadete(id);
            return Redirect("Listar");
        }
    }
}
