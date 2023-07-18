using MiIngresoHitss.Models;

namespace MiIngresoHitss.Repositorio
{
    public interface IProductoRepository
    {
        Producto ObtenerPorId(int id);
        IEnumerable<Producto> ObtenerTodos();
        void Crear(Producto producto);
        void Actualizar(Producto producto);
        void Eliminar(int id);
        IEnumerable<ProductoComprado> ObtenerProductosComprados();
    }
}
