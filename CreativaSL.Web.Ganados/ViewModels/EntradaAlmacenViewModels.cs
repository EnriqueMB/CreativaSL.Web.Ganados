using CreativaSL.Web.Ganados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.ViewModels
{
    public class EntradaAlmacenViewModels
    {
        public EntradaAlmacenViewModels()
        {
            _ListaAlmacenes = new List<CatAlmacenModels>();
            _ListaCompras = new List<CompraAlmacenModels>();
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

        private string _IDCompraAlmacen;
        /// <summary>
        /// Objeto que contiene los datos de la compra de almacén
        /// </summary>
        public string IDCompraAlmacen
        {
            get { return _IDCompraAlmacen; }
            set { _IDCompraAlmacen = value; }
        }

        private string _IDAlmacen;
        /// <summary>
        /// Objeto que contiene los datos del almacén al que se ingresarán los productos
        /// </summary>
        public string IDAlmacen
        {
            get { return _IDAlmacen; }
            set { _IDAlmacen = value; }
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

        private List<CompraAlmacenModels> _ListaCompras;
        /// <summary>
        /// Lista para llenar combo comrpas
        /// </summary>
        public List<CompraAlmacenModels> ListaCompras
        {
            get { return _ListaCompras; }
            set { _ListaCompras = value; }
        }

        private List<CatAlmacenModels> _ListaAlmacenes;
        /// <summary>
        /// Lista de almacenes 
        /// </summary>
        public List<CatAlmacenModels> ListaAlmacenes
        {
            get { return _ListaAlmacenes; }
            set { _ListaAlmacenes = value; }
        }
    }
}