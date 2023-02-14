using CadeteriaWeb.Models;

namespace CadeteriaWeb.Interfaces
{
    public interface IClienteRepositorio
    {
        List<Cliente> GetClientes();
        Cliente GetClientePorId(int idCliente);
        void AltaCliente(string nombre, string direccion, string telefono, string referencia);
        void AltaCliente(Cliente cliente);
        void EditarCliente(Cliente clienteDatos);
        void BajaCliente(int idCliente);
    }
}
