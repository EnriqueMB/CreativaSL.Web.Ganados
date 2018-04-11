using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatEmpleadoAltaNominaModels
    {
        public CatEmpleadoAltaNominaModels()
        {
            _IDAltaNominal = string.Empty;
            _IDEmpleado = string.Empty;
            _IDPuesto = 0;
            _IDCategoriaPuesto = string.Empty;
            _NombreCompleto = string.Empty;
            _sueldoBase = 0;
            _FechaAlta = DateTime.Today;
            _Baja = false;
            _ListaEmpleadoAltaNomina = new List<CatEmpleadoAltaNominaModels>();
            _ListaCmbPuesto = new List<CatPuestoModels>();
            _ListaCmbCategoriaPuesto = new List<CatCategoriaPuestoModels>();
            Usuario = string.Empty;
            Conexion = string.Empty;
        }
        private string _IDAltaNominal;
        /// <summary>
        /// Identificador que le pertenece a la alta nominal del empleado
        /// </summary>
        public string IDAltaNominal
        {
            get { return _IDAltaNominal; }
            set { _IDAltaNominal = value; }
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
        private int _IDPuesto;
        /// <summary>
        /// Identificador del puesto que le pertenece al Empleado
        /// </summary>
        [Required(ErrorMessage = "Seleccione un puesto")]
        [Display(Name = "Puesto")]
        [CombosInt(ErrorMessage = "Selecciones un puesto valido")]
        public int IDPuesto
        {
            get { return _IDPuesto; }
            set { _IDPuesto = value; }
        }

        private string _IDCategoriaPuesto;
        /// <summary>
        /// Identificador de la categoria del puesto en la que le pertenece al Empleado
        /// </summary>
        [Required(ErrorMessage = "Seleccione una categoria puesto")]
        [Display(Name = "Categoria Puesto")]
        public string IDCategoriaPuesto
        {
            get { return _IDCategoriaPuesto; }
            set { _IDCategoriaPuesto = value; }
        }

        private List<CatEmpleadoAltaNominaModels> _ListaEmpleadoAltaNomina;
        /// <summary>
        /// Lista para llenar tabla alta del empleado
        /// </summary>
        public List<CatEmpleadoAltaNominaModels>  ListaEmpleadoAltaNomina
        {
            get { return _ListaEmpleadoAltaNomina; }
            set { _ListaEmpleadoAltaNomina = value; }
        }

        private List<CatPuestoModels> _ListaCmbPuesto;
        /// <summary>
        /// Lista para llenar combo de puesto 
        /// </summary>
        public List<CatPuestoModels> ListaCmbPuesto
        {
            get { return _ListaCmbPuesto; }
            set { _ListaCmbPuesto = value; }
        }

        private List<CatCategoriaPuestoModels> _ListaCmbCategoriaPuesto;
        /// <summary>
        /// Lista para llena combo Categoria Puesto
        /// </summary>
        public List<CatCategoriaPuestoModels> ListaCmbCategoriaPuesto
        {
            get { return _ListaCmbCategoriaPuesto; }
            set { _ListaCmbCategoriaPuesto = value; }

        }

        private DateTime _FechaAlta;
        /// <summary>
        /// Fecha de alta del empleado
        /// </summary>
        
        public DateTime FechaAlta
        {
            get { return _FechaAlta; }
            set { _FechaAlta = value; }
        }

        private decimal _sueldoBase;
        
        public decimal sueldoBase
        {
            get { return _sueldoBase; }
            set { _sueldoBase = value; }
        }

        private bool _Baja;

        public bool Baja
        {
            get { return _Baja; }
            set { _Baja = value; }
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