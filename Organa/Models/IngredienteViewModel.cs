using System.ComponentModel.DataAnnotations;

namespace Organa.Models
{
    public class IngredienteViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Cantidad { get; set; }

        [Display(Name = "Categoria")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int CategoriaId { get; set; }

        public string NombreCategoria { get; set; }
    }
}
