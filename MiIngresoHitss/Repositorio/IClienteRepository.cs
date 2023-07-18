using MiIngresoHitss.Models;

namespace MiIngresoHitss.Repositorio
{
    public interface IClienteRepository
    {
        Cliente ObtenerPorId(int id);
        IEnumerable<Cliente> ObtenerTodos();
        void Crear(Cliente cliente);
        void Actualizar(Cliente cliente);
        void Eliminar(int id);
    }
}
