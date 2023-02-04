using CadeteriaWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadeteriaWeb.Repositorios
{
    public interface ICadeteRepositorio
    {
        List<Cadete> GetCadetes();
        Cadete GetCadetePorId(int idCadete);
        void AltaCadete(string nombre, string direccion, string telefono);
        void AltaCadete(Cadete cadete);
        void EditarCadete(Cadete cadeteDatos);
        void BajaCadete(int idCadete);
    }
}
