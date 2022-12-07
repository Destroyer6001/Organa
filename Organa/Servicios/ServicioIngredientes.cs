using Dapper;
using Microsoft.Data.SqlClient;
using Organa.Models;
using System.Linq.Expressions;

namespace Organa.Servicios
{
    public class ServicioIngredientes : IServicioIngredientes
    {
        private readonly string ConnectionString;
        public ServicioIngredientes(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(IngredienteViewModel ingrediente)
        {
            using var connection = new SqlConnection(ConnectionString);
            var id = await connection.QuerySingleAsync<int>(@"INSERT INTO INGREDIENTE (Nombre,Cantidad,CategoriaId) 
                                                            VALUES
                                                            (@NOMBRE,@CANTIDAD,@CATEGORIAID);
                                                            SELECT SCOPE_IDENTITY();",ingrediente);

            ingrediente.Id= id;
        }

        public async Task<bool> ExisteCrear(string Nombre)
        {
            using var connection = new SqlConnection(ConnectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM INGREDIENTE WHERE Nombre = @NOMBRE", new {Nombre});

            return existe == 1; 
        }

        public async Task<bool> Existe(string Nombre,int Id)
        {
            using var connection = new SqlConnection(ConnectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(@"SELECT 1 FROM INGREDIENTE WHERE Nombre = @NOMBRE AND Id != @ID", new { Nombre, Id });

            return existe == 1;
        }

        public async Task Actualizar(IngredienteCreacionViewModel ingrediente)
        {
            using var connection = new SqlConnection(ConnectionString);
            await connection.ExecuteAsync(@"UPDATE INGREDIENTE SET
                                            Nombre = @NOMBRE,
                                            Cantidad = @CANTIDAD,
                                            CategoriaId = @CATEGORIAID
                                            WHERE Id = @ID", ingrediente);
        }

        public async Task<IngredienteViewModel> ObtenerPorId (int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            return await connection.QueryFirstOrDefaultAsync<IngredienteViewModel>(@"SELECT ING.Id, ING.Nombre, 
                                                                                ING.Cantidad, ING.CategoriaId 
                                                                                FROM INGREDIENTE ING
                                                                                INNER JOIN CATEGORIA CAT
                                                                                ON CAT.Id = ING.CategoriaId
                                                                                WHERE ING.Id = @ID", new {id});
        }

        public async Task Eliminar(int id)
        {
            using var connection = new SqlConnection(ConnectionString);
            await connection.ExecuteAsync(@"DELETE INGREDIENTE WHERE Id = @ID", new { id });
        }

        public async Task<IEnumerable<IngredienteViewModel>> Obtener()
        {
            using var connection = new SqlConnection(ConnectionString);
            return await connection.QueryAsync<IngredienteViewModel>(@"SELECT ING.Id, ING.Nombre, ING.Cantidad, ING.CategoriaId, 
                                                                        CAT.Nombre as NombreCategoria
                                                                        FROM INGREDIENTE ING
                                                                        INNER JOIN CATEGORIA CAT
                                                                        ON CAT.Id = ING.CategoriaId");
        }

        public async Task<IEnumerable<IngredienteViewModel>> ObtenerPorCategoriaId(int CategoriaId)
        {
            using var connection = new SqlConnection(ConnectionString);
            return await connection.QueryAsync<IngredienteViewModel>(@"SELECT ING.Id, ING.Nombre, ING.Cantidad, ING.CategoriaId, 
                                                                        CAT.Nombre as NombreCategoria
                                                                        FROM INGREDIENTE ING
                                                                        INNER JOIN CATEGORIA CAT
                                                                        ON CAT.Id = ING.CategoriaId
																		WHERE ING.CategoriaId = @CATEGORIAID"
                                                                        , new {CategoriaId}
                                                                        );
        }
    }
}
