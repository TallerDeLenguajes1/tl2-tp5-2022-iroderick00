using CadeteriaWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadeteriaWeb.Interfaces
{
    public interface ICadeteRepositorio
    {
        List<Cadete> GetCadetes();
        Cadete GetCadetePorId(int idCadete);
        Cadete GetCadetePorIdUsuario(int idUsuario);
        void AltaCadete(string nombre, string direccion, string telefono, int idUsuario);
        void AltaCadete(Cadete cadete);
        void EditarCadete(Cadete cadeteDatos);
        void BajaCadete(int idCadete);
    }
}
