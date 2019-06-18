using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatVehiculoModels
    {
        public CatVehiculoModels()
        {
            _listaSucursal = new List<CatSucursalesModels>();
            _listaMarcas = new List<CatMarcaVehiculoModels>();
            _listaTipoVehiculos = new List<CatTipoVehiculoModels>();
            _listaVehiculos = new List<CatVehiculoModels>();
            _IDVehiculo = string.Empty;
            _IDSucursal = string.Empty;
            _IDMarca = 0;
            _IDTipoVehiculo = 0;
            _EsPropio = true;
            _Gps = string.Empty;
            _Capacidad = string.Empty;
            _Modelo = string.Empty;
            _Color = string.Empty;
            _Placas = string.Empty;

            _NoSerie = string.Empty;
            _Estatus = true;
            _fechaIngreso = DateTime.Now;

            _tarjetaCirculacion = string.Empty;

            _DateLastService = DateTime.MinValue;

            _nombreVehiculo = string.Empty;
            //datos de control
            Conexion = string.Empty;
            Resultado = 0;
            Opcion = 0;
            Completado = false;
            Usuario = string.Empty;
            ListaEmpresas = new List<CatEmpresaModels>();
            IDEmpresa = string.Empty;
            EsJaula = true;
            ColorJaula = string.Empty;
        }
        public bool EsJaula { get; set; }
        public string PlacaJaula { get; set; }
        public string ColorJaula { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Seleccione una empresa.")]
        public string IDEmpresa { get; set; }
        public List<CatEmpresaModels> ListaEmpresas { get; set; }

        private string _tarjetaCirculacion;
        [Required(ErrorMessage = "La tarjeta de Circulacion es obligatoria")]
        [Display(Name = "Tarjeta Circulación")]
        [StringLength(10, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Texto( ErrorMessage = "Solo Letras y números")]
        public string tarjetaCirculacion
        {
            get { return _tarjetaCirculacion; }
            set { _tarjetaCirculacion = value; }
        }

        private DateTime _fechaIngreso;
        [Required(ErrorMessage = "La Fecha de ingreso es obligatorio")]
        [Display(Name = "Fecha de ingreso")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        
        public DateTime fechaIngreso
        {
            get { return _fechaIngreso; }
            set { _fechaIngreso = value; }
        }

        private HttpPostedFileBase[] _Img;
        [Required(ErrorMessage = "Seleccione la imagen de la manifestación del fierro")]
        [Display(Name = "Manifestación Fierro")]
        public HttpPostedFileBase[] Img 
        {
            get { return _Img; }
            set { _Img = value; }
        }
        private string _img64;

        public string img64
        {
            get { return _img64; }
            set { _img64 = value; }
        }
        
        private string _numeroUnidad;

        public string numeroUnidad
        {
            get { return _numeroUnidad; }
            set { _numeroUnidad = value; }
        }

        private List<CatSucursalesModels> _listaSucursal;
        [Required(ErrorMessage = "La sucursal es obligatoria")]
        [Display(Name = "Sucursal")]
        public List<CatSucursalesModels> listaSucursal
        {
            get { return _listaSucursal; }
            set { _listaSucursal = value; }
        }
        private List<CatMarcaVehiculoModels> _listaMarcas;
        [Required(ErrorMessage = "La marca es obligatorio")]
        [Display(Name = "Marca")]
        
        public List<CatMarcaVehiculoModels> listaMarcas
        {
            get { return _listaMarcas; }
            set { _listaMarcas = value; }
        }
        private List<CatTipoVehiculoModels> _listaTipoVehiculos;
        [Required(ErrorMessage = "El tipo de Vehículo es obligatorio")]
        [Display(Name = "tipo de Vehículo")]
       
        public List<CatTipoVehiculoModels> listaTipoVehiculos
        {
            get { return _listaTipoVehiculos; }
            set { _listaTipoVehiculos = value; }
        }

        private List<CatVehiculoModels> _listaVehiculos;

        public List<CatVehiculoModels> listaVehiculos
        {
            get { return _listaVehiculos; }
            set { _listaVehiculos = value; }
        }
        private bool _BandImg;

        public bool BandImg
        {
            get { return _BandImg; }
            set { _BandImg = value; }
        }
        private string _IDVehiculo;

        public string IDVehiculo
        {
            get { return _IDVehiculo; }
            set { _IDVehiculo = value; }
        }
        private string _nombreSucursal;

        public string nombreSucursal
        {
            get { return _nombreSucursal; }
            set { _nombreSucursal = value; }
        }
        private string _nombreTipoVehiculo;

        public string nombreTipoVehiculo
        {
            get { return _nombreTipoVehiculo; }
            set { _nombreTipoVehiculo = value; }
        }
        private string _nombreMarca;

        public string nombreMarca
        {
            get { return _nombreMarca; }
            set { _nombreMarca = value; }
        }

        private string _IDSucursal;

        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }

        private int _IDTipoVehiculo;

        public int IDTipoVehiculo
        {
            get { return _IDTipoVehiculo; }
            set { _IDTipoVehiculo = value; }
        }

        private int _IDMarca;

        public int IDMarca
        {
            get { return _IDMarca; }
            set { _IDMarca = value; }
        }

        private bool _EsPropio;

        public bool EsPropio
        {
            get { return _EsPropio; }
            set { _EsPropio = value; }
        }

        private string _Gps;

        
        public string Gps
        {
            get { return _Gps; }
            set { _Gps = value; }
        }

        private string _Capacidad;
        [Required(ErrorMessage = "La capacidad es obligatorio")]
        [Display(Name = "Capacidad")]
        [StringLength(30, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        public string Capacidad
        {
            get { return _Capacidad; }
            set { _Capacidad = value; }
        }

        private string _Modelo;
        [Required(ErrorMessage = "El modelo es obligatorio")]
        [Display(Name = "Modelo")]
        [StringLength(10, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Texto(ErrorMessage = "Solo Letras, números y guiones ")]
        public string Modelo
        {
            get { return _Modelo; }
            set { _Modelo = value; }
        }

        private string _Color;
        [Required(ErrorMessage = "Color de unidad es obligatorio")]
        [Display(Name = "Color")]
        [StringLength(30, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Texto(ErrorMessage = "Solo Letras, números y guiones ")]
        public string Color
        {
            get { return _Color; }
            set { _Color = value; }
        }

        private string _Placas;
        [Required(ErrorMessage = "Las placas es obligatorio")]
        [Display(Name = "Placas")]
        [StringLength(10, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Placas(ErrorMessage = "Solo Letras, números y guiones ")]
        public string Placas
        {
            get { return _Placas; }
            set { _Placas = value; }
        }

      
        private string _NoSerie;
        //[Required(ErrorMessage = "Número de serie es obligatorio")]
        [Display(Name = "Número de motor")]
        [StringLength(30, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\s]*$", ErrorMessage = "Solo Letras y números")]
        public string NoSerie
        {
            get { return _NoSerie; }
            set { _NoSerie = value; }
        }

        private bool _Estatus;

        public bool Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }
        private string _nombreVehiculo;

        public string nombreVehiculo
        {
            get { return _nombreVehiculo; }
            set { _nombreVehiculo = value; }
        }

        private DateTime _DateLastService;
        /// <summary>
        /// Fecha de último servicio
        /// </summary>
        public DateTime DateLastService
        {
            get { return _DateLastService; }
            set { _DateLastService = value; }
        }

        //Fecha de último servicio con formato 
        public string DateLastServiceFormat
        {
            get { return _DateLastService != DateTime.MinValue ? _DateLastService.ToShortDateString() : "Sin Datos"; }
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