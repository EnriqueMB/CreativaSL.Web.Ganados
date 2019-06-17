using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class EntregaCombustibleModels
    {
        public EntregaCombustibleModels()
        {
            _IDEntregaCombustible = string.Empty;
            _IDSucursal = string.Empty;
            _IDVehiculo = string.Empty;
            _IDTipoCombustible = 0;
            _Fecha = DateTime.Today;
            _NoTicket = string.Empty;
            _KMInicial = 0;
            _KMFinal = 0;
            _Litros = 0;
            _Precio = 0;
            _Total = 0;
            _UrlImagen64 = string.Empty;
            _ImgTicketBand = false;
            _BandFechaEntrega = false;
            _BandIDSucursal = false;
            _BandIDVehiculo = false;
            _Sucursal = string.Empty;
            _Vehiculo = string.Empty;
            //_ImgTicket = new HttpPostedFile
            _ListaVehiculos = new List<CatVehiculoModels>();
            _ListaTipoCombustible = new List<CatTipoCombustibleModels>();
            _ListaSucursales = new List<CatSucursalesModels>();
            Conexion = string.Empty;
            Resultado = 0;
            Completado = false;
            Usuario = string.Empty;
            Opcion = 0;
        }
        private decimal _Precio;

        public decimal Precio
        {
            get { return _Precio; }
            set { _Precio = value; }
        }

        private bool _BandFechaEntrega;

        public bool BandFechaEntrega
        {
            get { return _BandFechaEntrega; }
            set { _BandFechaEntrega = value; }
        }
        private bool _BandIDSucursal;

        public bool BandIDSucursal
        {
            get { return _BandIDSucursal; }
            set { _BandIDSucursal = value; }
        }
        private bool _BandIDVehiculo;

        public bool BandIDVehiuculo
        {
            get { return _BandIDVehiculo; }
            set { _BandIDVehiculo = value; }
        }

        private string _IDEntregaCombustible;
        /// <summary>
        /// Identificador de la entrega de combustible
        /// </summary>
        public string IDEntregaCombustible
        {
            get { return _IDEntregaCombustible; }
            set { _IDEntregaCombustible = value; }
        }

        private string _IDSucursal;
        /// <summary>
        /// Identificador de la sucursal a la que se cargará el documento por pagar
        /// </summary>
        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }

        private string _IDProveedor;
        /// <summary>
        /// Identificador del proveedor
        /// </summary>
        public string IDProveedor
        {
            get { return _IDProveedor; }
            set { _IDProveedor = value; }
        }

        private string _Proveedor;

        public string Proveedor
        {
            get { return _Proveedor; }
            set { _Proveedor = value; }
        }


        private string _IDVehiculo;
        /// <summary>
        /// Identificador del vehículo
        /// </summary>
        /// 

        public string IDVehiculo
        {
            get { return _IDVehiculo; }
            set { _IDVehiculo = value; }
        }

        private int _IDTipoCombustible;
        /// <summary>
        /// Identificador del tipo de combustible
        /// </summary>
        public int IDTipoCombustible
        {
            get { return _IDTipoCombustible; }
            set { _IDTipoCombustible = value; }
        }

        private DateTime _Fecha;
        /// <summary>
        /// Fecha en que se realiza la carga de combustible
        /// </summary>
        [Display(Name = "Fecha")]

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        private string _NoTicket;
        /// <summary>
        /// Número de ticket de compra de combustible
        /// </summary>
        public string NoTicket
        {
            get { return _NoTicket; }
            set { _NoTicket = value; }
        }

        private int _KMInicial;
        /// <summary>
        /// Kilometraje inicial 
        /// </summary>
        public int KMInicial
        {
            get { return _KMInicial; }
            set { _KMInicial = value; }
        }

        private int _KMFinal;
        /// <summary>
        /// Kilometraje final
        /// </summary>
        public int KMFinal
        {
            get { return _KMFinal; }
            set { _KMFinal = value; }
        }

        private decimal _Litros;
        /// <summary>
        /// Cantidad de litros ingresados
        /// </summary>
        public decimal Litros
        {
            get { return _Litros; }
            set { _Litros = value; }
        }

        private decimal _Total;
        /// <summary>
        /// Monto total
        /// </summary>
        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        public string TotalFormat
        {
            get { return string.Format("{0:C}", _Total); }
        }


        private string _UrlImagen64;
        /// <summary>
        /// Imagen en Base 64
        /// </summary>
        public string UrlImagen64      //---------------se puede utilizar otra vez para sacar la imagen
        {
            get { return _UrlImagen64; }
            set { _UrlImagen64 = value; }
        }

        public string UrlImagen64Tickets      //---------------se puede utilizar otra vez para sacar la imagen
        {
            get { return _UrlImagen64; }
            set { _UrlImagen64 = value; }
        }




        private string _Estatus;
        /// <summary>
        /// Descripción textual del estatus de la entrega de combustible
        /// </summary>
        public string Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }


        private string _CssClassEstatus;
        /// <summary>
        /// Estilo para dibujado del texto del estatus de la entrega de combustible
        /// </summary>
        public string CssClassEstatus
        {
            get { return _CssClassEstatus; }
            set { _CssClassEstatus = value; }
        }


        private HttpPostedFileBase[] _ImgTicket;
        [Required(ErrorMessage = "Seleccione la imagen de productos")]

        [Display(Name = "Imagen Ticket")]
        /// <summary>
        /// Imagen del ticket de la carga de combustible
        /// </summary>
        public HttpPostedFileBase[] ImgTicket
        {
            get { return _ImgTicket; }
            set { _ImgTicket = value; }
        }
        public HttpPostedFileBase[] Img2 { get; set; }
        private bool _ImgTicketBand;

        public bool ImgTicketBand
        {
            get { return _ImgTicketBand; }
            set { _ImgTicketBand = value; }
        }

        private List<EntregaCombustibleModels> _listaEntregaCombustible;

        public List<EntregaCombustibleModels> listaEntregaCombustible
        {
            get { return _listaEntregaCombustible; }
            set { _listaEntregaCombustible = value; }
        }
        private string _Vehiculo;

        public string Vehiculo
        {
            get { return _Vehiculo; }
            set { _Vehiculo = value; }
        }
        private string _Sucursal;

        public string Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }


        private List<CatVehiculoModels> _ListaVehiculos;
        /// <summary>
        /// Lista para llenar combo de sucursales
        /// </summary>
        public List<CatVehiculoModels> ListaVehiculos
        {
            get { return _ListaVehiculos; }
            set { _ListaVehiculos = value; }
        }

        private List<CatTipoCombustibleModels> _ListaTipoCombustible;
        /// <summary>
        /// Lista para llenar combo de tipo de combustible
        /// </summary>
        public List<CatTipoCombustibleModels> ListaTipoCombustible
        {
            get { return _ListaTipoCombustible; }
            set { _ListaTipoCombustible = value; }
        }

        private List<CatSucursalesModels> _ListaSucursales;
        /// <summary>
        /// Lista para llenar combo de sucursales
        /// </summary>
        public List<CatSucursalesModels> ListaSucursales
        {
            get { return _ListaSucursales; }
            set { _ListaSucursales = value; }
        }
        private bool _BandImg;
        /// <summary>
        /// BANDERA PARA ACTUALIZAR IMAGEN
        /// </summary>
        public bool BandImg
        {
            get { return _BandImg; }
            set { _BandImg = value; }
        }
        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        public bool NuevoRegistro { get; set; }
        #endregion
    }
}