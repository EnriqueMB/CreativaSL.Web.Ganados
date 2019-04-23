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
        public int Orden { get; set; }
    }
}