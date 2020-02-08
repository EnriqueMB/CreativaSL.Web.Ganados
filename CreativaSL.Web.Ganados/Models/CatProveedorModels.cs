using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatProveedorModels
    {
        public CatProveedorModels()
        {
            _fotoPerfil = string.Empty;
            _IDProveedor = string.Empty;
            _IDTipoProveedor = 0;
            _IDSucursal = string.Empty;
            _NombreRazonSocial = string.Empty;
            _RFC = string.Empty;
            _ImgINE = string.Empty;
            _ImgManifestacionFierro = string.Empty;
            _listaProveedores = new List<CatProveedorModels>();
            _listaSucursal = new List<CatSucursalesModels>();
            _listaTipoProveedor = new List<CatTipoProveedorModels>();
            _merma = 0;
            //Datos control
            Conexion = string.Empty;
            Resultado = 0;
            Opcion = 0;
            Completado = false;
            Usuario = string.Empty;
            _FechaIngreso = DateTime.Now;
            _EsEmpresa = false;
            _Tolerancia = 0;
            _Observaciones = string.Empty;
            _Direccion = string.Empty;
            _ListaGeneroCMB = new List<CatGeneroModels>();
            _ListaPeriodo = new List<CatPeriodoModels>();
            _TodaSucursale = false;
        }
        private decimal _merma;

        public decimal merma
        {
            get { return _merma; }
            set { _merma = value; }
        }

        private string _telefonoCasa;

        public string telefonoCasa
        {
            get { return _telefonoCasa; }
            set { _telefonoCasa = value; }
        }
        private string _telefonoCelular;

        public string telefonoCelular
        {
            get { return _telefonoCelular; }
            set { _telefonoCelular = value; }
        }
        private string _correo;
        //[Required(ErrorMessage = "El Correo es obligatorio")]
        [Display(Name = "Correo")]
        [RegularExpression(@"^[_A-Za-z0-9-]+(.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(.[A-Za-z0-9-]+)*(.[A-Za-z]{2,4})$", ErrorMessage = "Correo no Valido")]

        //[RegularExpression(@" ^[_A-Za-z0-9-.\\+]+(\\.[_A-Za-z0-9-.]+)*@" + "[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "Correo no Valido")]
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        public string correo
        {
            get { return _correo; }
            set { _correo = value; }
        }
        private int _sexo;

        public int sexo
        {
            get { return _sexo; }
            set { _sexo = value; }
        }
        private string _Direccion;
        [Display(Name = "Dirección")]
        [Direccion(ErrorMessage = "Ingrese un datos válido para {0}")]
        public string Direccion
        {
            get { return _Direccion; }
            set { _Direccion = value; }
        }
        /// <summary>
        /// LISTA DE GENERO DE PERSONA
        /// </summary>
        private List<CatGeneroModels> _ListaGeneroCMB;

        public List<CatGeneroModels> ListaGeneroCMB
        {
            get { return _ListaGeneroCMB; }
            set { _ListaGeneroCMB = value; }
        }
        /// <summary>
        /// LISTA DE TIPO DE PROVEEDOR
        /// </summary>
        /// 
        private List<CatTipoProveedorModels> _listaTipoProveedor;

        public List<CatTipoProveedorModels> listaTipoProveedor
        {
            get { return _listaTipoProveedor; }
            set { _listaTipoProveedor = value; }
        }
        private List<CatContactosModels> _listaDatosContactos;

        public List<CatContactosModels> listaDatosContactos 
        {
            get { return _listaDatosContactos; }
            set { _listaDatosContactos = value; }
        }

        /// <summary>
        /// LISTA SUCURSAL
        /// </summary>
        /// 
        private List<CatSucursalesModels> _listaSucursal;

        public List<CatSucursalesModels> listaSucursal
        {
            get { return _listaSucursal; }
            set { _listaSucursal = value; }
        }
        /// <summary>
        /// NOMBRE DEL TIPO DE PROVEEDOR
        /// </summary>
        private string _nombreProveedor;

        public string nombreProveedor
        {
            get { return _nombreProveedor; }
            set { _nombreProveedor = value; }
        }

        private string _nombreSucursal;

        public string nombreSucursal
        {
            get { return _nombreSucursal; }
            set { _nombreSucursal = value; }
        }
        /// <summary>
        /// lista proveedor
        /// </summary>
        /// 
        private List<CatProveedorModels> _listaProveedores;

        public List<CatProveedorModels> listaProveedores
        {
            get { return _listaProveedores; }
            set { _listaProveedores = value; }
        }

        private string _IDProveedor;

        public string IDProveedor
        {
            get { return _IDProveedor; }
            set { _IDProveedor = value; }
        }

        private int _IDTipoProveedor;
        [Required(ErrorMessage = "Seleccione un tipo proveedor")]
        [Display(Name = "Proveedor")]
        [CombosInt(ErrorMessage = "Seleccione un tipo proveedor")]
        public int IDTipoProveedor
        {
            get { return _IDTipoProveedor; }
            set { _IDTipoProveedor = value; }
        }

        private string _IDSucursal;
        [Required(ErrorMessage = "Seleccione una sucursal")]
        [Display(Name = "Sucursal")]
        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }

        private string _NombreRazonSocial;
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "nombre")]
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\s\.\,\-]*$", ErrorMessage = "Solo Letras, Números")]
        public string NombreRazonSocial
        {
            get { return _NombreRazonSocial; }
            set { _NombreRazonSocial = value; }
        }

        private string _RFC;
        //[Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "nombre")]
        [StringLength(20, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\s]*$", ErrorMessage = "Solo Letras, Números")]
        public string RFC
        {
            get { return _RFC; }
            set { _RFC = value; }
        }
        //[Required(ErrorMessage = "Seleccione la imagen del INE")]
        [Display(Name = "Imagen INE")]
        public HttpPostedFileBase[] ImgINEE { get; set; }

        private HttpPostedFileBase[] _ImgManifestacionFierros;
        //[Required(ErrorMessage = "Seleccione la imagen de la manifestación del fierro")]
        [Display(Name = "Manifestación Fierro")]
        public HttpPostedFileBase[] ImgManifestacionFierros
        {
            get { return _ImgManifestacionFierros; }
            set { _ImgManifestacionFierros = value; }
        }

        [Display(Name = "Imagen INE")]
        public HttpPostedFileBase[] ImgINEE2 { get; set; }

        private HttpPostedFileBase[] _ImgManifestacionFierros2;
        [Display(Name = "Manifestación Fierro")]
        public HttpPostedFileBase[] ImgManifestacionFierros2
        {
            get { return _ImgManifestacionFierros2; }
            set { _ImgManifestacionFierros2 = value; }
        }

        private string _Imagenes;

        public string Imagenes
        {
            get { return _Imagenes; }
            set { _Imagenes = value; }
        }

        private string _NombreImagen;

        public string NombreImagen
        {
            get { return _NombreImagen; }
            set { _NombreImagen = value; }
        }


        private string _ImgINE;
        
        public string ImgINE
        {
            get { return _ImgINE; }
            set { _ImgINE = value; }
        }

        private string _ImgManifestacionFierro;
        
        public string ImgManifestacionFierro
        {
            get { return _ImgManifestacionFierro; }
            set { _ImgManifestacionFierro = value; }
        }

        private bool _BandINE;

        public bool BandINE
        {
            get { return _BandINE; }
            set { _BandINE = value; }
        }

        private bool _BandMF;

        public bool BandMF
        {
            get { return _BandMF; }
            set { _BandMF = value; }
        }

        private DateTime _FechaIngreso;

        public DateTime FechaIngreso
        {
            get { return _FechaIngreso; }
            set { _FechaIngreso = value; }
        }

        private bool _EsEmpresa;

        public bool EsEmpresa
        {
            get { return _EsEmpresa; }
            set { _EsEmpresa = value; }
        }

        private decimal _Tolerancia;

        public decimal Tolerancia
        {
            get { return _Tolerancia; }
            set { _Tolerancia = value; }
        }

        private string _Observaciones;

        public string Observaciones
        {
            get { return _Observaciones; }
            set { _Observaciones = value; }
        }

        private int _CantidadPeriodo;

        public int CantidadPeriodo
        {
            get { return _CantidadPeriodo; }
            set { _CantidadPeriodo = value; }
        }

        private int _IDPeriodo;
        //[Required(ErrorMessage = "Seleccione un periodo")]
        [Display(Name = "Periodo")]
       // [CombosInt(ErrorMessage = "Seleccione un tipo periodo")]
        public int IDPeriodo
        {
            get { return _IDPeriodo; }
            set { _IDPeriodo = value; }
        }

        private List<CatPeriodoModels> _ListaPeriodo;

        public List<CatPeriodoModels> ListaPeriodo
        {
            get { return _ListaPeriodo; }
            set { _ListaPeriodo = value; }
        }

        public string TipoProveedor { get; set; }

        private bool _TodaSucursale;

        public bool TodaSucursale
        {
            get { return _TodaSucursale; }
            set { _TodaSucursale = value; }
        }

        private string _fotoPerfil;

        public string FotoPerfil
        {
            get { return  _fotoPerfil; }
            set {  _fotoPerfil = value; }
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