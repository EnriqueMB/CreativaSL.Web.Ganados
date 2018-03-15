using System.ComponentModel.DataAnnotations;
using System.Data;

namespace CreativaSL.Web.Ganados.Models
{
    public class TipoUsuarioModels
    {
        public int id_tipoUsuario { get; set; }

        private string _tipoUsuario;
        [Display(Name = "Descripcion")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\(\)\-\,\.\;\:\s]*$", ErrorMessage = "Solo Letras y números")]
        public string tipoUsuario
        {
            get { return _tipoUsuario; }
            set { _tipoUsuario = value; }
        }

        private string _descripcion;
        [Display(Name = "Descripcion")]
        [StringLength(50, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\(\)\-\,\.\;\:\s]*$", ErrorMessage = "Solo Letras y números")]
        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public DataTable tablaTipoUsuario { get; set; }

        #region Datos de control
        public bool activo { get; set; }
        public string user { get; set; }
        public string conexion { get; set; }
        public int opcion { get; set; }
        #endregion
    }
}