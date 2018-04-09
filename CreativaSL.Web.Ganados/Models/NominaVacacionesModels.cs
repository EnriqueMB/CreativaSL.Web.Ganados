using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class NominaVacacionesModels
    {
        public NominaVacacionesModels()
        {
            _IDVacaciones = string.Empty;
            _IDEmpleado = string.Empty;
            _FechaInicio = DateTime.Today;
            _FechaFin = DateTime.Today;
            Conexion = string.Empty;
            Usuario = string.Empty;
            _ListaNominaVacaciones = new List<NominaVacacionesModels>();
        }

        private string _IDVacaciones;

        public string IDVacaciones
        {
            get { return _IDVacaciones; }
            set { _IDVacaciones = value; }
        }

        private string _IDEmpleado;

        public string IDEmpleado
        {
            get { return _IDEmpleado; }
            set { _IDEmpleado = value; }
        }

        private DateTime _FechaInicio;
        [Required(ErrorMessage = "Debe seleccionar una fecha de inicio de vacaciones")]
        [Display(Name = "Fecha de inicio de vacaciones")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio
        {
            get { return _FechaInicio; }
            set { _FechaInicio = value; }
        }

        private DateTime _FechaFin;
        [Required(ErrorMessage = "Debe seleccionar una fecha de fin de vacaciones")]
        [Display(Name = "Fecha de fin de vacaciones")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFin
        {
            get { return _FechaFin; }
            set { _FechaFin = value; }
        }

        private List<NominaVacacionesModels> _ListaNominaVacaciones;

        public List<NominaVacacionesModels> ListaNominaVacaciones
        {
            get { return _ListaNominaVacaciones; }
            set { _ListaNominaVacaciones = value; }
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