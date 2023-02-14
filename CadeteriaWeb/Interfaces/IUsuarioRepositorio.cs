using CadeteriaWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CadeteriaWeb.Interfaces
{
    public interface IUsuarioRepositorio
    {
        Usuario GetUsuario(string user, string pass);
        void AltaUsuario(string user, string pass, int rol, string nombre);
        void BajaUsuario(int idUsuario);
        //bool CheckearLogIn();
    }
}
