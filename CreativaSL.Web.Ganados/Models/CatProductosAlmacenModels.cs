using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatProductosAlmacenModels
    {
        public string UnidadMedida { get; set; }
        public CatProductosAlmacenModels() {
            _listaProductosAlmacen = new List<CatProductosAlmacenModels>();
            _listaTipoCodigoProducto = new List<CatTipoCodigoProductoModels>();
            _listaUnidadMedida = new List<CatUnidadMedidaModels>();
            _ListaTipoClasificacion = new List<CatTipoClasificacionModels>();
            _Clave = string.Empty;
            _Nombre = string.Empty;
            _Descripcion = string.Empty;
            _UltimoCosto = 0;
            _Almacen = true;
            _IDUnidadMedida = 0;
            _IDTipoCodigo = 0;
            _IDProductoAlmacen = string.Empty;
            _Imagen = string.Empty;
            //Datos control
            Conexion = string.Empty;
            Resultado = 0;
            Opcion = 0;
            Completado = false;
            Usuario = string.Empty;
        }

        public decimal Existencia { get; set; }
        public decimal PrecioUnidad { get; set; }
        public string Id_unidadProducto { get; set; }


        private List<CatProductosAlmacenModels> _listaProductosAlmacen;

        public List<CatProductosAlmacenModels> listaPrdocutosAlmacen
        {
            get { return _listaProductosAlmacen; }
            set { _listaProductosAlmacen = value; }
        }

        private List<CatTipoCodigoProductoModels> _listaTipoCodigoProducto;

        public List<CatTipoCodigoProductoModels> listaTipoCodigoProducto
        {
            get { return _listaTipoCodigoProducto; }
            set { _listaTipoCodigoProducto = value; }
        }
        private List<CatUnidadMedidaModels> _listaUnidadMedida;

        public List<CatUnidadMedidaModels> listaUnidadMedida
        {
            get { return _listaUnidadMedida; }
            set { _listaUnidadMedida = value; }
        }
        private bool _BandImg;

        public bool BandImg
        {
            get { return _BandImg; }
            set { _BandImg = value; }
        }


        private string _IDProductoAlmacen;

        public string IDProductoAlmacen
        {
            get { return _IDProductoAlmacen; }
            set { _IDProductoAlmacen = value; }
        }

        private int _IDTipoCodigo;

        public int IDTipoCodigo
        {
            get { return _IDTipoCodigo; }
            set { _IDTipoCodigo = value; }
        }

        private string _Clave;
        [Required(ErrorMessage = "Ingrese una clave del producto")]
        [Texto(ErrorMessage = "Ingrese una clave válida")]
        [Display(Name = "Clave producto")]
        public string Clave
        {
            get { return _Clave; }
            set { _Clave = value; }
        }

        private string _Nombre;
        [Required(ErrorMessage = "Ingrese un nombre del producto")]
        [Texto(ErrorMessage = "Ingrese un nombre válida")]
        [Display(Name = "Nombre producto")]
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        private string _Descripcion;
        [Required(ErrorMessage = "Ingrese una descripción del producto")]
        [Texto(ErrorMessage="Ingrese una descripción válida")]
        [Display(Name = "Descripción producto")]
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }


        private decimal _UltimoCosto;

        public decimal UltimoCosto
        {
            get { return _UltimoCosto; }
            set { _UltimoCosto = value; }
        }

        private bool _Almacen;

        public bool Almacen
        {
            get { return _Almacen; }
            set { _Almacen = value; }
        }

        private int _IDUnidadMedida;
        [Required(ErrorMessage = "Ingrese una unidad de médida")]
        public int IDUnidadMedida
        {
            get { return _IDUnidadMedida; }
            set { _IDUnidadMedida = value; }
        }
        private HttpPostedFileBase[] _imagen2;
        [Required(ErrorMessage = "Seleccione la imagen de productos")]
        
        [Display(Name = "Imagen producto")]
        public HttpPostedFileBase[] imagen2
        {
            get { return _imagen2; }
            set { _imagen2 = value; }
        }

        public HttpPostedFileBase[] Img2 { get; set; }
        private string _Imagen;

        public string Imagen
        {
            get { return _Imagen; }
            set { _Imagen = value; }
        }

        private string _Extension;

        public string Extension
        {
            get { return _Extension; }
            set { _Extension = value; }
        }

        private int _IDTipoClasificacion;

        public int IDTipoClasificacion
        {
            get { return _IDTipoClasificacion; }
            set { _IDTipoClasificacion = value; }
        }
        //-------------------------------------------------------------------------------
        private bool _IdCheck;
        public bool IdCheck
        {
            get { return _IdCheck; }
            set { _IdCheck = value; }
        }
        //-------------------------------------------------------------------------------


        private List<CatTipoClasificacionModels> _ListaTipoClasificacion;

        public List<CatTipoClasificacionModels> ListaTipoClasificacion
        {
            get { return _ListaTipoClasificacion; }
            set { _ListaTipoClasificacion = value; }
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