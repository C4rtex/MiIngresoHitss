using MiIngresoHitss.Models;
using System.Data;
using Dapper;

namespace MiIngresoHitss.Repositorio
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProductoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Producto ObtenerPorId(int id)
        {
            string query = "SELECT * FROM Productos WHERE Id = @Id";
            return _dbConnection.QuerySingleOrDefault<Producto>(query, new { Id = id });
        }

        public IEnumerable<Producto> ObtenerTodos()
        {
            string query = "SELECT * FROM Productos";
            return _dbConnection.Query<Producto>(query);
        }
        public IEnumerable<ProductoComprado> ObtenerProductosComprados()
        {
            string query = "SELECT p.Nombre AS NombreProducto, c.Nombre AS NombreCliente FROM Compras AS co JOIN Productos AS p ON p.Id = co.ProductoId JOIN Clientes AS c ON c.Id = co.ClienteId";
            return _dbConnection.Query<ProductoComprado>(query);
        }
        public void Crear(Producto producto)
        {
            string query = "INSERT INTO Productos (Nombre, Descripcion, Precio) VALUES (@Nombre, @Descripcion, @Precio)";
            _dbConnection.Execute(query, producto);
        }

        public void Actualizar(Producto producto)
        {
            string query = "UPDATE Productos SET Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio WHERE Id = @Id";
            _dbConnection.Execute(query, producto);
        }

        public void Eliminar(int id)
        {
            string query = "DELETE FROM Productos WHERE Id = @Id";
            _dbConnection.Execute(query, new { Id = id });
        }
    }
}
