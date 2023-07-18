using Dapper;
using MiIngresoHitss.Models;
using System.Data;

namespace MiIngresoHitss.Repositorio
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IDbConnection _dbConnection;

        public ClienteRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public Cliente ObtenerPorId(int id)
        {
            string query = "SELECT * FROM Clientes WHERE Id = @Id";
            return _dbConnection.QuerySingleOrDefault<Cliente>(query, new { Id = id });
        }

        public IEnumerable<Cliente> ObtenerTodos()
        {
            string query = "SELECT * FROM Clientes";
            return _dbConnection.Query<Cliente>(query);
        }

        public void Crear(Cliente cliente)
        {
            string query = "INSERT INTO Clientes (Nombre, Direccion, Telefono) VALUES (@Nombre, @Direccion, @Telefono)";
            _dbConnection.Execute(query, cliente);
        }

        public void Actualizar(Cliente cliente)
        {
            string query = "UPDATE Clientes SET Nombre = @Nombre, Direccion = @Direccion, Telefono = @Telefono WHERE Id = @Id";
            _dbConnection.Execute(query, cliente);
        }

        public void Eliminar(int id)
        {
            string query = "DELETE FROM Clientes WHERE Id = @Id";
            _dbConnection.Execute(query, new { Id = id });
        }
    }
}
