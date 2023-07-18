using MiIngresoHitss.Models;

namespace MiIngresoHitss.Repositorio
{
    public interface ICompraRepository
    {
        Compra ObtenerPorId(int id);
        IEnumerable<Compra> ObtenerTodos();
        void Crear(Compra compra);
        void Actualizar(Compra compra);
        void Eliminar(int id);
        IEnumerable<Compra> ObtenerProductosVendidosEnRangoDeFecha(DateTime fechaInicio, DateTime fechaFin);
        void Agregar(Compra compra);
    }
}
