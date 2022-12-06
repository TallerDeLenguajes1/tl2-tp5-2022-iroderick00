using CadeteriaWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadeteriaWeb.Repositorios
{
    public interface ICadeteRepositorio
    {
        public List<Cadete> GetCadetes();
        public void AltaCadete(string nombre, string direccion, string telefono);
        public void AltaCadete(Cadete cadete);
        public Cadete GetCadetePorId(int idCadete);
        public void EditarCadete(Cadete cadeteDatos);
        public void BajaCadete(int idCadete);
    }
}
