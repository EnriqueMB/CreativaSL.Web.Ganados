using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Models
{
    public class NominaDiasFestivosModels
    {

        public NominaDiasFestivosModels()
        {
            _idDiasFestivos = string.Empty;
            _fecha = DateTime.Today;
            _Descripcion = string.Empty;
        }

        private string _idDiasFestivos;

        public string IdDiasFestivos
        {
            get { return _idDiasFestivos; }
            set { _idDiasFestivos = value; }
        }

        private DateTime _fecha;
        [Required(ErrorMessage = "Debe seleccionar una fecha")]
        [Display(Name = "Fecha del Día Festivo")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        private string _Descripcion;
        [Required(ErrorMessage = "Debe ingresar el nombre del proveedor")]
        [Display(Name = "Nombre Proveedor")]
        [StringLength(100, ErrorMessage = "El número de caracteres del campo {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Texto(ErrorMessage = "Ingrese un dato válido para Nombre del Proveedor")]
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }
        private List<NominaDiasFestivosModels> _LNominaDias;

        public List<NominaDiasFestivosModels> LNominaDias
        {
            get { return _LNominaDias; }
            set { _LNominaDias = value; }
        }
        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}