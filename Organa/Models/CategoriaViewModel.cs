using System.ComponentModel.DataAnnotations;

namespace Organa.Models
{
    public class CategoriaViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "No puede ser mayor a {1} caracteres")]
        [Display(Name = "Descripcion")]
        public string Nombre { get; set; }

    }
}
