using Organa.Models;

namespace Organa.Servicios
{
    public interface IServicioIngredientes
    {
        Task Actualizar(IngredienteCreacionViewModel ingrediente);
        Task Crear(IngredienteViewModel ingrediente);
        Task Eliminar(int id);
        Task<bool> Existe(string Nombre, int Id);
        Task<bool> ExisteCrear(string Nombre);
        Task<IEnumerable<IngredienteViewModel>> Obtener();
        Task<IEnumerable<IngredienteViewModel>> ObtenerPorCategoriaId(int CategoriaId);
        Task<IngredienteViewModel> ObtenerPorId(int id);
    }
}
