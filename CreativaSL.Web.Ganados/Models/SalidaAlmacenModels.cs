using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class SalidaAlmacenModels
    {
        public SalidaAlmacenModels()
        {
            _IDSalidaAlmacen = string.Empty;
            _Sucursal = new CatSucursalesModels();
            _Almacen = new CatAlmacenModels();
            _Empleado = new CatEmpleadoModels();
            _FechaSalida = DateTime.Today;
            _FolioSalida = string.Empty;
            _Comentario = string.Empty;
            _Estatus = string.Empty;
            _CssEstatus = string.Empty;
            _IDEstatus = 0;
            Conexion = string.Empty;
            Resultado = 0;
            Completado = false;
            Usuario = string.Empty;
            Opcion = 0;
        }

        private string _IDSalidaAlmacen;
        /// <summary>
        /// Identificador de la salida de almacén
        /// </summary>
        public string IDSalidaAlmacen
        {
            get { return _IDSalidaAlmacen; }
            set { _IDSalidaAlmacen = value; }
        }

        private CatSucursalesModels _Sucursal;
        /// <summary>
        /// Sucursal a la paque pertenece el almacén 
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

        private DateTime _FechaSalida;
        /// <summary>
        /// Fecha en la que se realiza la salida de almacén
        /// </summary>
        public DateTime FechaSalida
        {
            get { return _FechaSalida; }
            set { _FechaSalida = value; }
        }

        /// <summary>
        /// Fecha de salida en cadena con formato
        /// </summary>
        public string FechaSalidaFormat
        {
            get { return _FechaSalida.ToShortDateString(); }
        }
        
        private string _FolioSalida;
        /// <summary>
        /// Folio generado por el sistema
        /// </summary>
        public string FolioSalida
        {
            get { return _FolioSalida; }
            set { _FolioSalida = value; }
        }

        private string _Comentario;
        /// <summary>
        /// Información adicional de la Salida de almacén
        /// </summary>
        public string Comentario
        {
            get { return _Comentario; }
            set { _Comentario = value; }
        }

        private bool _Finalizado;
        public bool Finalizado
        {
            get { return _Finalizado; }
            set { _Finalizado = value; }
        }

        private string _Estatus;
        public string Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }

        private string _CssEstatus;
        public string CssEstatus
        {
            get { return _CssEstatus; }
            set { _CssEstatus = value; }
        }

        private int _IDEstatus;
        public int IDEstatus
        {
            get { return _IDEstatus; }
            set { _IDEstatus = value; }
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