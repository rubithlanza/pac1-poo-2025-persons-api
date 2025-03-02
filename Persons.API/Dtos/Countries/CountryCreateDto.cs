using System.ComponentModel.DataAnnotations;

namespace Persons.API.Dtos.Countries
{
    public class CountryCreateDto
    {
        [Display(Name ="Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El campo {0} debe tener un minimo de {2} y una maximo de {1} caracteres.")]
        public string Name { get; set; }

        [Display(Name = "Codigo Alfa")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "El campo {0} debe tener un minimo de {2} y una maximo de {1} caracteres.")]
        public string AlphaCode3 { get; set; }
    }
}
