using Dapper;
using Microsoft.Data.SqlClient;
using Organa.Models;

namespace Organa.Servicios
{
    public class ServicioPlato : IServicioPlato
    {
        private readonly string ConnectionString;
        public ServicioPlato(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(PlatoViewModel plato)
        {
            using var connection = new SqlConnection(ConnectionString);
            var id = await connection.QuerySingleAsync<int>("SP_REGISTRARPLATO",new
            {
                plato.Descripcion,
                plato.Cantidad,
                plato.Nombre,
                plato.Imagen,
                plato.CategoriaCarneId,
                plato.CantidadCarne,
                plato.CategoriaGranoId,
                plato.CantidadGrano,
                plato.CategoriaArrozId,
                plato.CantidadArroz
            },commandType:System.Data.CommandType.StoredProcedure);

            plato.Id = id;
        }

        public async Task<bool> ExisteCrear(string Nombre)
        {
            using var connection = new SqlConnection(ConnectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(@"select 1 from plato where Nombre = @NOMBRE", new { Nombre });

            return existe == 1; 
        }

        public async Task<IEnumerable<PlatoViewModel>> Obtener()
        {
            using var connection = new SqlConnection(ConnectionString);
            return await connection.QueryAsync<PlatoViewModel>(@"select Id, Descripcion, Cantidad, Nombre, Imagen, CategoriaCarneId, 
                                                        CantidadCarne, CategoriaGranoId, CantidadGrano, CategoriaArrozId, 
                                                        CantidadArroz from Plato");
        }
    }
}
