using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class PrestamoHerramientaModels
    {
        public PrestamoHerramientaModels()
        {
            _IDPrestamo = 0;
            _Sucursal = new CatSucursalesModels();
            _Almacen = new CatAlmacenModels();
            _Empleado = new CatEmpleadoModels();
            _FolioPrestamo = string.Empty;
            _FechaPrestamo = DateTime.Today;
            _Observacion = string.Empty;
            _Css = string.Empty;
            _IDEstatus = 0;
            Conexion = string.Empty;
            Resultado = 0;
            Completado = false;
            Usuario = string.Empty;
            Opcion = 0;
            _EstatusDes = string.Empty;
        }

        private int _IDPrestamo;

        public int IDPrestamo
        {
            get { return _IDPrestamo; }
            set { _IDPrestamo = value; }
        }

        private CatSucursalesModels _Sucursal;
        /// <summary>
        /// Sucursal a la que pertenece el almacén 
        /// </summary>
        public CatSucursalesModels Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }

        private CatAlmacenModels _Almacen;
        /// <summary>
        /// Objeto que contiene los datos del almacén del que salen los productos
        /// </summary>
        public CatAlmacenModels Almacen
        {
            get { return _Almacen; }
            set { _Almacen = value; }
        }

        private CatEmpleadoModels _Empleado;

        public CatEmpleadoModels Empleado
        {
            get { return _Empleado; }
            set { _Empleado = value; }
        }

        private string _FolioPrestamo;

        public string FolioPrestamo
        {
            get { return _FolioPrestamo; }
            set { _FolioPrestamo = value; }
        }

        private DateTime _FechaPrestamo;

        public DateTime FechaPrestamo
        {
            get { return _FechaPrestamo; }
            set { _FechaPrestamo = value; }
        }

        /// <summary>
        /// Fecha de salida en cadena con formato
        /// </summary>
        public string FechaPrestamoFormat
        {
            get { return _FechaPrestamo.ToShortDateString(); }
        }

        private string _Observacion;

        public string Observacion
        {
            get { return _Observacion; }
            set { _Observacion = value; }
        }

        private int _IDEstatus;

        public int IDEstatus
        {
            get { return _IDEstatus; }
            set { _IDEstatus = value; }
        }

        private string _Css;

        public string Css
        {
            get { return _Css; }
            set { _Css = value; }
        }

        private string _EstatusDes;

        public string EstatusDes
        {
            get { return _EstatusDes; }
            set { _EstatusDes = value; }
        }


        #region Datos De Control
        public bool NuevoRegistro { get; set; }
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}