using CreativaSL.Web.Ganados.Models.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace CreativaSL.Web.Ganados.Models
{
    public class Clasificacion
    {
        public int IdTipoClasificacion { get; set; }
        [Required(ErrorMessage = "Ingrese el nombre de la categoría.")]
        [Display(Name = "Clasificación")]
        public string Descripcion { get; set; }
        public bool SoloLectura { get; set; }

        [Required(ErrorMessage = "Ingrese el orden de la categoría.")]
        [Entero(ErrorMessage ="Ingrese un dato entero.")]
        [Display(Name = "Orden")]
        public int Orden { get; set; }

        public int ParentId { get; set; }
    }
}