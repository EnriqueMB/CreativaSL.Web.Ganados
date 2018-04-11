using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatEmpleadoBajaNominaModels
    {
        public CatEmpleadoBajaNominaModels()
        {
            _IBajaNominal = string.Empty;
            _IDEmpleado = string.Empty;
            _NombreCompleto = string.Empty;
            _ListaEmpleadoBajaNomina = new List<CatEmpleadoBajaNominaModels>();
            _ListaCmbMotivoBaja = new List<CatMotivoBajaModels>();
            Conexion = string.Empty;
            Usuario = string.Empty;
        }

        private string _IBajaNominal;
        /// <summary>
        /// Identificador que le pertenece a la alta nominal del empleado
        /// </summary>
        public string IDAltaNominal
        {
            get { return _IBajaNominal; }
            set { _IBajaNominal = value; }
        }
        private string _IDEmpleado;
        /// <summary>
        /// Identificador que le pertenece al Empleado
        /// </summary>
        public string IDEmpleado
        {
            get { return _IDEmpleado; }
            set { _IDEmpleado = value; }
        }
        private string _NombreCompleto;
        /// <summary>
        /// Nombre Completo del empleado
        /// </summary>
        public string NombreCompleto
        {
            get { return _NombreCompleto; }
            set { _NombreCompleto = value; }
        }

        private string _Comentarios;
        /// <summary>
        /// Comentarios de la baja
        /// </summary>
        [Required(ErrorMessage = "Debe ingresar los comentarios de la baja")]
        [Display(Name = "Comentarios")]
        [StringLength(300, ErrorMessage = "El número de caracteres del campo {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 5)]
        [Texto(ErrorMessage = "Ingrese caracteres validos")]
        public string Comentarios
        {
            get { return _Comentarios; }
            set { _Comentarios = value; }
        }
        private int _IDMotivoBaja;
        /// <summary>
        /// Identificador que le pertenece al motivo de la baja
        /// </summary>
        [Required(ErrorMessage = "Seleccione un motivo")]
        [Display(Name = "Puesto")]
        [CombosInt(ErrorMessage = "Seleccione un motivo")]
        public int IDMotivoBaja
        {
            get { return _IDMotivoBaja; }
            set { _IDMotivoBaja = value; }
        }

        private List<CatEmpleadoBajaNominaModels> _ListaEmpleadoBajaNomina;
        /// <summary>
        /// Lista para llena la tabla de las bajas de los empleados
        /// </summary>
        public List<CatEmpleadoBajaNominaModels> ListaEmpleadoBajaNomina
        {
            get { return _ListaEmpleadoBajaNomina; }
            set { _ListaEmpleadoBajaNomina = value; }
        }

        private List<CatMotivoBajaModels> _ListaCmbMotivoBaja;
        /// <summary>
        /// Lista para llena combo de motivos de baja
        /// </summary>
        public List<CatMotivoBajaModels> ListaCmbMotivoBaja
        {
            get { return _ListaCmbMotivoBaja; }
            set { _ListaCmbMotivoBaja = value; }
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