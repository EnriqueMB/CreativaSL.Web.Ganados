using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatTipoServicioModels
    {
        public CatTipoServicioModels() {
            _IDTipoServicio = string.Empty;
            _Descripcion = string.Empty;
            //Datos de control
            Activo = false;
            Usuario = string.Empty;
            Conexion = string.Empty;
            Completado = false;
            Opcion = 0;
            Resultado = 0;
            _listaTipoServicio = new List<CatTipoServicioModels>();
        }
        private List<CatTipoServicioModels> _listaTipoServicio;

        public List<CatTipoServicioModels> listaTipoServicio
        {
            get { return _listaTipoServicio; }
            set { _listaTipoServicio = value; }
        }

        private string _IDTipoServicio;

        public string IDTipoServicio
        {
            get { return _IDTipoServicio; }
            set { _IDTipoServicio = value; }
        }
        private string _Descripcion;
        [Required(ErrorMessage = "La descripción es obligatoria")]
        [Display(Name = "Descripción")]
        [StringLength(120, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Texto(ErrorMessage ="Ingrese un formato válido")]
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        public bool Activo { get; set; }
        #endregion
    }
}