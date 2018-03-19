using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatProveedorModels
    {
        public CatProveedorModels() {
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
            //Datos control
            Conexion = string.Empty;
            Resultado = 0;
            Opcion = 0;
            Completado = false;
            Usuario = string.Empty;

        }
        /// <summary>
        /// LISTA DE TIPO DE PROVEEDOR
        /// </summary>
        /// 
        private List<CatTipoProveedorModels> _listaTipoProveedor;
        [Required(ErrorMessage = "El tipo de proveedor es obligatorio")]
        [Display(Name = "Municipio")]

        [RegularExpression(@"^[1-9][0-9]*$", ErrorMessage = "Seleccione un municipio")]
        public List<CatTipoProveedorModels> listaTipoProveedor
        {
            get { return _listaTipoProveedor; }
            set { _listaTipoProveedor = value; }
        }
        /// <summary>
        /// LISTA SUCURSAL
        /// </summary>
        /// 
        private List<CatSucursalesModels> _listaSucursal;
        [Required(ErrorMessage = "La sucursal es obligatoria")]
        [Display(Name = "Sucursal")]
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

        public int IDTipoProveedor
        {
            get { return _IDTipoProveedor; }
            set { _IDTipoProveedor = value; }
        }

        private string _IDSucursal;

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
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "nombre")]
        [StringLength(20, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\s]*$", ErrorMessage = "Solo Letras, Números")]
        public string RFC
        {
            get { return _RFC; }
            set { _RFC = value; }
        }
        [Required(ErrorMessage = "Seleccione la imagen del INE")]
        [Display(Name = "Imagen INE")]
        public HttpPostedFileBase[] ImgINEE { get; set; }

        private HttpPostedFileBase[] _ImgManifestacionFierros;
        [Required(ErrorMessage = "Seleccione la imagen de la manifestación del fierro")]
        [Display(Name = "Manifestación Fierro")]
        public HttpPostedFileBase[] ImgManifestacionFierros
        {
            get { return _ImgManifestacionFierros; }
            set { _ImgManifestacionFierros = value; }
        }

        public HttpPostedFileBase[] ImgINEE2 { get; set; }

        private HttpPostedFileBase[] _ImgManifestacionFierros2;
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
        [Required(ErrorMessage = "La Imagen es obligatorio")]
        [Display(Name = "Imagen")]
        [FileExtensions(Extensions = "png,jpg,jpeg", ErrorMessage = "Solo imagenes")]
        public string ImgINE
        {
            get { return _ImgINE; }
            set { _ImgINE = value; }
        }

        private string _ImgManifestacionFierro;
        [Required(ErrorMessage = "La Imagen es obligatorio")]
        [Display(Name = "Imagen")]
        [FileExtensions(Extensions = "png,jpg,jpeg", ErrorMessage = "Solo imagenes")]
        public string ImgManifestacionFierro
        {
            get { return _ImgManifestacionFierro; }
            set { _ImgManifestacionFierro = value; }
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