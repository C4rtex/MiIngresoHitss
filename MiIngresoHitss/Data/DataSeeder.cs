using Dapper;
using MiIngresoHitss.Models;
using System.Data;

namespace MiIngresoHitss.Data
{
    public class DataSeeder
    {
        private readonly IDbConnection _dbConnection;

        public DataSeeder(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void SeedData()
        {
            _dbConnection.Execute(@"DELETE FROM Compras;");
            _dbConnection.Execute(@"DELETE FROM Productos;
                                    DELETE FROM Clientes;
                                    ");
            
            // Insertar datos en la tabla Productos
            _dbConnection.Execute("INSERT INTO Productos (Nombre, Descripcion, Precio) VALUES (@Nombre, @Descripcion, @Precio)",
                new
                {
                    Nombre = "Producto 1",
                    Descripcion = "Descripción del Producto 1",
                    Precio = 10.99m
                });

            _dbConnection.Execute("INSERT INTO Productos (Nombre, Descripcion, Precio) VALUES ( @Nombre, @Descripcion, @Precio)",
                new
                {
                    Nombre = "Producto 2",
                    Descripcion = "Descripción del Producto 2",
                    Precio = 19.99m
                });

            // Insertar datos en la tabla Clientes
            _dbConnection.Execute("INSERT INTO Clientes (Nombre, Direccion, Telefono) VALUES ( @Nombre, @Direccion, @Telefono)",
                new
                {
                    Nombre = "Cliente 1",
                    Direccion = "Dirección del Cliente 1",
                    Telefono = "123456789"
                });

            _dbConnection.Execute("INSERT INTO Clientes (Nombre, Direccion, Telefono) VALUES (@Nombre, @Direccion, @Telefono)",
                new
                {
                    Nombre = "Cliente 2",
                    Direccion = "Dirección del Cliente 2",
                    Telefono = "987654321"
                });
        }
    }
}
