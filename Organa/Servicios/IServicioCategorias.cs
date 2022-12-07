using Organa.Models;

namespace Organa.Servicios
{
    public interface IServicioCategorias
    {
        Task Actualizar(CategoriaViewModel categoria);
        Task Crear(CategoriaViewModel categoria);
        Task Eliminar(int id);
        Task<bool> Existe(int id, string nombre);
        Task<bool> ExisteCrear(string nombre);
        Task<IEnumerable<CategoriaViewModel>> Obtener();
        Task<CategoriaViewModel> ObtenerPorId(int id);
    }
}
