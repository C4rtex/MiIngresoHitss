using Dapper;
using MiIngresoHitss.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace MiIngresoHitss.Repositorio
{
    public class CompraRepository : ICompraRepository
    {
        private readonly IDbConnection _dbConnection;

        public CompraRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Compra ObtenerPorId(int id)
        {
            string query = "SELECT * FROM Compras WHERE Id = @Id";
            return _dbConnection.QuerySingleOrDefault<Compra>(query, new { Id = id });
        }

        public IEnumerable<Compra> ObtenerTodos()
        {
            string query = "SELECT * FROM Compras";
            return _dbConnection.Query<Compra>(query);
        }

        public void Crear(Compra compra)
        {
            string query = "INSERT INTO Compras (Id, ClienteId, ProductoId, FechaCompra) VALUES (@Id, @ClienteId, @ProductoId, @FechaCompra)";
            _dbConnection.Execute(query, compra);
        }

        public void Actualizar(Compra compra)
        {
            string query = "UPDATE Compras SET ClienteId = @ClienteId, ProductoId = @ProductoId, FechaCompra = @FechaCompra WHERE Id = @Id";
            _dbConnection.Execute(query, compra);
        }

        public void Eliminar(int id)
        {
            string query = "DELETE FROM Compras WHERE Id = @Id";
            _dbConnection.Execute(query, new { Id = id });
        }

        public IEnumerable<Compra> ObtenerProductosVendidosEnRangoDeFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            string query = "SELECT p.Nombre AS ProductoNombre, c.Nombre AS ClienteNombre FROM Compras AS co JOIN Productos AS p ON p.Id = co.ProductoId JOIN Clientes AS c ON c.Id = co.ClienteId WHERE co.FechaCompra BETWEEN @FechaInicio AND @FechaFin";
            return _dbConnection.Query<Compra>(query, new { FechaInicio = fechaInicio, FechaFin = fechaFin });
        }

        public void Agregar(Compra compra)
        {
            string query = "INSERT INTO Compras (ClienteId, ProductoId, FechaCompra) VALUES (@ClienteId, @ProductoId, @FechaCompra)";
            _dbConnection.Execute(query, new { compra.ClienteId, compra.ProductoId, compra.FechaCompra });
        }
    }
}
