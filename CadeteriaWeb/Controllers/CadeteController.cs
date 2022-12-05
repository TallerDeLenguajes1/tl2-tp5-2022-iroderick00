using AutoMapper;
using CadeteriaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using CadeteriaWeb.Repositorios;
using CadeteriaWeb.ViewModels;

namespace CadeteriaWeb.Controllers
{
    public class CadeteController : Controller
    {
        private static CadeteRepositorio _cadeteRepositorio = new CadeteRepositorio();
        private readonly ILogger<CadeteController> _logger;
        private IMapper _mapper;

        public CadeteController(ILogger<CadeteController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }
        public IActionResult Index()
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
                return RedirectToAction("Index");
            }
            else
            {
                return View("DarAltaCadete", cadeteVM);
            }
        }
        public IActionResult EditarCadete(int id)
        {
            var cadetes = _cadeteRepositorio.GetCadetes();
            var cadeteAEditar = cadetes.Find(x => x.Id == id);
            var cadeteVM = _mapper.Map<CadeteViewModel>(cadeteAEditar);
            if (cadeteAEditar != null)
            {
                return View(cadeteVM);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult EditarCadete(CadeteViewModel cadeteVM) 
        {
            if (ModelState.IsValid)
            {
                var cadete = _mapper.Map<Cadete>(cadeteVM);
                _cadeteRepositorio.EditarCadete(cadete);
                return RedirectToAction("Index");
            }
            else
            {
                return View("EditarCadete", cadeteVM);
            }
        }
        public IActionResult DarBajaCadete(int id)
        {
            var cadetes = _cadeteRepositorio.GetCadetes();
            var cadeteADarDeBaja = cadetes.Find(x => x.Id == id);
            var cadeteVM = _mapper.Map<CadeteViewModel>(cadeteADarDeBaja);
            if (cadeteADarDeBaja != null)
            {
                return View(cadeteVM);
            }
            else
            {
                return View("Index");
            }
        }
        public RedirectResult ConfirmarBaja(int id)
        {
            _cadeteRepositorio.BajaCadete(id);
            return Redirect("Index");
        }
    }
}
