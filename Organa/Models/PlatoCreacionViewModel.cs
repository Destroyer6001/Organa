using Microsoft.AspNetCore.Mvc.Rendering;

namespace Organa.Models
{
    public class PlatoCreacionViewModel : PlatoViewModel
    {
        public IEnumerable<SelectListItem> CategoriaCarne { get; set; }

        public IEnumerable<SelectListItem> Categoriagrano { get; set; }

        public IEnumerable<SelectListItem> CategoriaArroz { get; set; }

        public IFormFile SeleccionarImagen { get; set; }
    }
}
