using System.ComponentModel.DataAnnotations;

namespace Organa.Models
{
    public class PlatoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(500, ErrorMessage = "No puede ser mayor a {1} caracteres")]

        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "No puede ser mayor a {1} caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Cantidad { get; set; }

        public string Imagen { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Tipo De Carne")]
        public int CategoriaCarneId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Gramos de carne por plato")]
        public int CantidadCarne { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Tipo De Grano")]
        public int CategoriaGranoId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Gramos de grano por plato")]
        public int CantidadGrano { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Tipo De Arroz")]
        public int CategoriaArrozId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Gramos de arroz por plato")]
        public int CantidadArroz { get; set; }


    }
}
