using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class EntradaAlmacenModels
    {

        public EntradaAlmacenModels()
        {
            _IDEntradaAlmacen = string.Empty;
            _CompraAlmacen = new CompraAlmacenModels();
            _Almacen = new CatAlmacenModels();
            _FechaEntrada = DateTime.Today;
            _FolioEntrada = string.Empty;
            _Comentario = string.Empty;
            Conexion = string.Empty;
            Resultado = 0;
            Completado = false;
            Usuario = string.Empty;
            Opcion = 0;
        }

        private string _IDEntradaAlmacen;
        /// <summary>
        /// Identificador de la entrada de almacén
        /// </summary>
        public string IDEntradaAlmacen
        {
            get { return _IDEntradaAlmacen; }
            set { _IDEntradaAlmacen = value; }
        }

        private CompraAlmacenModels _CompraAlmacen;
        /// <summary>
        /// Objeto que contiene los datos de la compra de almacén
        /// </summary>
        public CompraAlmacenModels CompraAlmacen
        {
            get { return _CompraAlmacen; }
            set { _CompraAlmacen = value; }
        }
        
        private CatAlmacenModels _Almacen;
        /// <summary>
        /// Objeto que contiene los datos del almacén al que se ingresarán los productos
        /// </summary>
        public CatAlmacenModels Almacen
        {
            get { return _Almacen; }
            set { _Almacen = value; }
        }
        
        private DateTime _FechaEntrada;
        /// <summary>
        /// Fecha en la que se realiza la entrada a almacén
        /// </summary>
        public DateTime FechaEntrada
        {
            get { return _FechaEntrada; }
            set { _FechaEntrada = value; }
        }
        
        public string FechaEntradaFormat
        {
            get { return _FechaEntrada.ToShortDateString(); }
        }


        private string _FolioEntrada;
        /// <summary>
        /// Folio generado por el sistema
        /// </summary>
        public string FolioEntrada
        {
            get { return _FolioEntrada; }
            set { _FolioEntrada = value; }
        }

        private string _Comentario;
        /// <summary>
        /// Información adicional de la entrada de almacén
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