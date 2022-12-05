using CadeteriaWeb.Models;

namespace CadeteriaWeb.Repositorios
{
    public class CadeteRepositorio
    {
        private static int numeroCadete = 1;
        private static List<Cadete> cadetes = new List<Cadete>();

        public List<Cadete> GetCadetes() { return cadetes; }
        public void AltaCadete(string nombre, string direccion, string telefono)
        {
            var id = numeroCadete;
            numeroCadete++;
            var nuevoCadete = new Cadete(id, nombre, direccion, telefono);
            cadetes.Add(nuevoCadete);
        }
        public void AltaCadete(Cadete cadete)
        {
            var id = numeroCadete;
            numeroCadete++;
            if (cadete != null)
            {
                cadete.Id = id;
                cadetes.Add(cadete);
            }
        }
        public void EditarCadete(Cadete cadeteDatos)
        {
            var cadeteAEditar = cadetes.Find(x=> x.Id == cadeteDatos.Id);
            if (cadeteAEditar != null)
            {
                cadeteAEditar.Nombre = cadeteDatos.Nombre;
                cadeteAEditar.Telefono = cadeteDatos.Telefono;
                cadeteAEditar.Direccion = cadeteDatos.Direccion;
            }
        }
        public void BajaCadete(int id)
        {
            var cadeteABorrar = cadetes.Find(cadete => cadete.Id == id);
            if (cadeteABorrar != null)
            {
                cadetes.Remove(cadeteABorrar);
            }
        }
    }
}
