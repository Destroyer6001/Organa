using Organa.Models;

namespace Organa.Servicios
{
    public interface IServicioPlato
    {
        Task Crear(PlatoViewModel plato);
        Task<bool> ExisteCrear(string Nombre);
        Task<IEnumerable<PlatoViewModel>> Obtener();
    }
}
