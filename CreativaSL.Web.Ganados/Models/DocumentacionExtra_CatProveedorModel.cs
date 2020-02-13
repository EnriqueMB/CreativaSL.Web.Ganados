using System.ComponentModel.DataAnnotations;

namespace CreativaSL.Web.Ganados.Models
{
    public class DocumentacionExtra_CatProveedorModel
    {
        public string IdDocumentacionExtra { get; set; }
 
        public int IdTipoDocumentacionExtra { get; set; }
        
        public string IdProveedor { get; set; }

        [Required(ErrorMessage = "Por favor seleccione un archivo.")]
        public string Archivo { get; set; }
   
    }
}