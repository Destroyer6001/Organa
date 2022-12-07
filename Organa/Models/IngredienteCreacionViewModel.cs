using Microsoft.AspNetCore.Mvc.Rendering;

namespace Organa.Models
{
    public class IngredienteCreacionViewModel : IngredienteViewModel
    {
        public IEnumerable<SelectListItem> CategoriaViewModel { get; set; }
    }
}
