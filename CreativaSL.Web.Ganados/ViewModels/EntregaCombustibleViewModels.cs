using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.ViewModels
{
    public class EntregaCombustibleViewModels
    {
        public EntregaCombustibleViewModels()
        {
            _IDEntregaCombustible = string.Empty;
            _IDSucursal = string.Empty;
            _IDVehiculo = string.Empty;
            _IDProveedor = string.Empty;
            _IDTipoCombustible = 0;
            _Fecha = DateTime.Today;
            _NoTicket = string.Empty;
            _KMInicial = 0;
            _Litros = 0;
            _Total = 0;
            _UrlImagen64 = string.Empty;
            _ImgTicketBand = false;
            _ListaVehiculos = new List<CatVehiculoModels>();
            _ListaTipoCombustible = new List<CatTipoCombustibleModels>();
            _ListaSucursales = new List<CatSucursalesModels>();
            _ListaProveedores = new List<CatProveedorModels>();
            _IDChofer = String.Empty;
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
        [Required(ErrorMessage = "Seleccione una sucursal.")]
        [Display(Name = "Sucursal")]
        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }

        private string _IDProveedor;
        /// <summary>
        /// Identificador del proveedor al que se le compra el combustible
        /// </summary>
        [Required(ErrorMessage = "Seleccione un proveedor.")]
        [Display(Name = "Proveedor")]
        public string IDProveedor
        {
            get { return _IDProveedor; }
            set { _IDProveedor = value; }
        }
        
        private string _IDVehiculo;
        /// <summary>
        /// Identificador del vehículo
        /// </summary>
        [Required(ErrorMessage = "Seleccione un vehículo.")]
        [Display(Name = "Vehículo")]
        public string IDVehiculo
        {
            get { return _IDVehiculo; }
            set { _IDVehiculo = value; }
        }
        //--------------------------------------------------------------

        private string _IDChofer;
        [Required(ErrorMessage = "Seleccione un Chofer.")]
        [Display(Name = "Chofer")]


        public string IDChofer
        {
            get { return _IDChofer; }
            set { _IDChofer = value; }
        }
        //--------------------------------------------------------------

        private int _IDTipoCombustible;
        /// <summary>
        /// Identificador del tipo de combustible
        /// </summary>
        [CombosInt(ErrorMessage = "Seleccione un tipo de combustible.")]
        [Display(Name = "Tipo de combustible")]
        public int IDTipoCombustible
        {
            get { return _IDTipoCombustible; }
            set { _IDTipoCombustible = value; }
        }
        
        private DateTime _Fecha;
        /// <summary>
        /// Fecha en que se realiza la carga de combustible
        /// </summary>
        [Required(ErrorMessage = "Debe seleccionar una fecha de carga")]
        [Display(Name = "Fecha de carga")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }

        private string _NoTicket;
        /// <summary>
        /// Número de ticket de compra de combustible
        /// </summary>
        [Required(ErrorMessage = "Debe ingresar el número del ticket.")]
        [Display(Name = "No. de ticket")]
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
        
        private decimal _Litros;
        /// <summary>
        /// Cantidad de litros ingresados
        /// </summary>
        [DecimalMayor0(ErrorMessage = "{0} debe ser mayor a 0")]
        [Display(Name = "Litros")]
        public decimal Litros
        {
            get { return _Litros; }
            set { _Litros = value; }
        }
        
        private decimal _Total;
        /// <summary>
        /// Monto total
        /// </summary>
        [DecimalMayor0(ErrorMessage = "{0} debe ser mayor a 0")]
        [Display(Name = "Total")]
        public decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }

        private string _UrlImagen64;
        /// <summary>
        /// Imagen en Base 64
        /// </summary>
        public string UrlImagen64
        {
            get { return _UrlImagen64; }
            set { _UrlImagen64 = value; }
        }
        
        private HttpPostedFile[] _ImgTicket;
        /// <summary>
        /// Imagen del ticket de la carga de combustible
        /// </summary>
        public HttpPostedFile[] ImgTicket
        {
            get { return _ImgTicket; }
            set { _ImgTicket = value; }
        }

        private HttpPostedFile[] _ImgTicket2;
        /// <summary>
        /// Imagen del ticket de la carga de combustible
        /// </summary>
        public HttpPostedFile[] ImgTicket2
        {
            get { return _ImgTicket; }
            set { _ImgTicket = value; }
        }

        private bool _ImgTicketBand;
        public bool ImgTicketBand
        {
            get { return _ImgTicketBand; }
            set { _ImgTicketBand = value; }
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

        private List<CatProveedorModels> _ListaProveedores;
        /// <summary>
        /// Lista para llenar combo de proveedores
        /// </summary>
        public List<CatProveedorModels> ListaProveedores
        {
            get { return _ListaProveedores; }
            set { _ListaProveedores = value; }
        }
        //-------------------------------------------------chofer-------------------------inicio----------------------------------
        private List<CatChoferModels> _listaChofer;

        public List<CatChoferModels> listaChofer
        {
            get { return _listaChofer; }
            set { _listaChofer = value; }
        }
        //-------------------------------------------------chofer-------------------------final----------------------------------

        public EntregaCombustibleModels ObtenerModeloPersistencia()
        {
            return new EntregaCombustibleModels
            {
                IDEntregaCombustible = _IDEntregaCombustible,
                IDSucursal = _IDSucursal,
                IDVehiculo = _IDVehiculo,
                IDProveedor = _IDProveedor,
                IDTipoCombustible = _IDTipoCombustible,
                IDChofer = _IDChofer,//--------------------agregue


                Fecha = _Fecha,
                NoTicket = _NoTicket,
                KMInicial = _KMInicial,
                Litros = _Litros,
                Total = _Total,
                UrlImagen64 = _UrlImagen64,
                ImgTicketBand = _ImgTicketBand
            };
        }
    }
}