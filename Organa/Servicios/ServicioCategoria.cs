using Dapper;
using Microsoft.Data.SqlClient;
using Organa.Models;

namespace Organa.Servicios
{
    public class ServicioCategoria : IServicioCategorias
    {
        private readonly string ConnectionString;
        public ServicioCategoria(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(CategoriaViewModel categoria)
        {
            using var connection = new SqlConnection(ConnectionString);
            var Id = await connection.QuerySingleAsync<int>(@"INSERT INTO CATEGORIA (Nombre) VALUES
                                                            (@NOMBRE);
                                                            SELECT SCOPE_IDENTITY();",categoria);
            categoria.Id= Id;
        }

        public async Task<bool> Existe (int id, string nombre)
        {
            using var connection = new SqlConnection(ConnectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM CATEGORIA 
                                            WHERE Id != @Id AND Nombre = @NOMBRE", new {id,nombre});

            return existe == 1;
        }

        public async Task<bool> ExisteCrear(string nombre)
        {
            using var connection = new SqlConnection(ConnectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM CATEGORIA 
                                            WHERE Nombre = @NOMBRE", new { nombre });

            return existe == 1;
        }

        public async Task<IEnumerable<CategoriaViewModel>> Obtener()
        {
            using var connection = new SqlConnection(ConnectionString);
            return await connection.QueryAsync<CategoriaViewModel>(@"SELECT ID, NOMBRE FROM CATEGORIA");
        }

        public async Task<CategoriaViewModel> ObtenerPorId(int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            return await connection.QueryFirstOrDefaultAsync<CategoriaViewModel>(@"SELECT ID, NOMBRE FROM CATEGORIA
                                                                                 WHERE ID=@ID", new {id});
        }

        public async Task Actualizar(CategoriaViewModel categoria)
        {
            using var connection = new SqlConnection(ConnectionString);
            await connection.ExecuteAsync(@"UPDATE CATEGORIA SET 
                                            Nombre = @NOMBRE 
                                            WHERE ID = @ID", categoria);
        }

        public async Task Eliminar(int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            await connection.ExecuteAsync("DELETE CATEGORIA WHERE ID = @ID", new {id});
        }
    }
}
