using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatLugarModels
    {
        public CatLugarModels() {
            _listaEstado = new List<CatEstadoModels>();
            _listaLugares = new List<CatLugarModels>();
            _listaMunicipio = new List<CatMunicipioModels>();
            _listaPaises = new List<CatPaisModels>();
            _id_estadoCodigo = string.Empty;
            _id_municipio = 0;
            _id_pais = string.Empty;
            _id_estado = 0;
            _ejido = string.Empty;
            _id_lugar = string.Empty;
            _descripcion = string.Empty;
            _latitud = 0;
            _longitud = 0;
            _bascula = false;
            _nombrePropietario = string.Empty;
            _observaciones = string.Empty;
            Direccion = string.Empty;
            
            //Datos de control
            activo = false;
            user = string.Empty;
            conexion = string.Empty;
            Completado = false;
            opcion = 0;
            resultado = string.Empty;

        }

        public string Direccion { get; set; }

        //Nombre del dueño 
        private string _nombrePropietario;
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "nombre")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ\s]*$", ErrorMessage = "Solo Letras")]
        public string nombrePropietario
        {
            get { return _nombrePropietario; }
            set { _nombrePropietario = value; }
        }
        private string _apellidoPaterno;
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Apellido Paterno")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ\s]*$", ErrorMessage = "Solo Letras")]
        public string apellidoPaterno
        {
            get { return _apellidoPaterno; }
            set { _apellidoPaterno = value; }
        }
        private string _apellidoMaterno;
       // [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Apellido Materno")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ\s]*$", ErrorMessage = "Solo Letras")]
        public string apellidoMaterno
        {
            get { return _apellidoMaterno; }
            set { _apellidoMaterno = value; }
        }

        private bool _bascula;

        public bool bascula
        {
            get { return _bascula; }
            set { _bascula = value; }
        }
        private string _observaciones;
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "nombre")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\s\.\,\#]*$", ErrorMessage = "Solo Letras")]
        public string observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
        }
        private List<CatSucursalesModels> _listaSucursal;

        public List<CatSucursalesModels> listaSucursal
        {
            get { return _listaSucursal; }
            set { _listaSucursal = value; }
        }
        private string _id_sucursal;

        public string id_sucursal
        {
            get { return _id_sucursal; }
            set { _id_sucursal = value; }
        }
        private string _nombreSucursal;

        public string nombreSucursal
        {
            get { return _nombreSucursal; }
            set { _nombreSucursal = value; }
        }


        //LISTA DE PAISES, ESTADOS Y MUNICIPIOS
        private List<CatPaisModels> _listaPaises;
       
        
        
        public List<CatPaisModels> listaPaises
        {
            get { return _listaPaises; }
            set { _listaPaises = value; }
        }
        private List<CatEstadoModels> _listaEstado;
        
        
        public List<CatEstadoModels> listaEstado
        {
            get { return _listaEstado; }
            set { _listaEstado = value; }
        }
        private List<CatMunicipioModels> _listaMunicipio;
        
        public List<CatMunicipioModels> listaMunicipio
        {
            get { return _listaMunicipio; }
            set { _listaMunicipio = value; }
        }

        //PAIS, ESTADO, MUNICIPIO Y EJIDO DEL LUGAR
        private string _municipio;

        public string municipio
        {
            get { return _municipio; }
            set { _municipio = value; }
        }

        private int _id_municipio;

        public int id_municipio
        {
            get { return _id_municipio; }
            set { _id_municipio = value; }
        }
        private string _id_estadoCodigo;

        public string id_estadoCodigo
        {
            get { return _id_estadoCodigo; }
            set { _id_estadoCodigo = value; }
        }

        private int _id_estado;

        public int id_estado
        {
            get { return _id_estado; }
            set { _id_estado = value; }
        }
        private string _id_pais;

        public string id_pais
        {
            get { return _id_pais; }
            set { _id_pais = value; }
        }
        private string _ejido;
       
        [Display(Name = "Pais")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        public string ejido
        {
            get { return _ejido; }
            set { _ejido = value; }
        }

        //lista de lugares
        private List<CatLugarModels> _listaLugares;

        public List<CatLugarModels> listaLugares
        {
            get { return _listaLugares; }
            set { _listaLugares = value; }
        }

        /// <summary>
        /// ID DEL LUGAR 
        /// </summary>
        private string _id_lugar;

        public string id_lugar
        {
            get { return _id_lugar; }
            set { _id_lugar = value; }
        }
        /// <summary>
        /// DESCRIPCION DEL LUGAR 
        /// </summary>
        private string _descripcion;
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "nombre")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ\s]*$", ErrorMessage = "Solo Letras")]
        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        /// <summary>
        /// COORDENADA DEL LUGAR : LATITUD
        /// </summary>
        private float _latitud;

        public float latitud
        {
            get { return _latitud; }
            set { _latitud = value; }
        }
        private string _lat;

        public string lat
        {
            get { return _lat; }
            set { _lat = value; }
        }

        /// <summary>
        /// COORDENADA DEL LUGAR: LONGITUD
        /// </summary>
        /// 
        private float  _longitud;

        public float longitud
        {
            get { return _longitud; }
            set { _longitud = value; }
        }
        private string _lng;

        public string lng
        {
            get { return _lng; }
            set { _lng = value; }
        }

        #region Datos de control
        public bool activo { get; set; }
        public string user { get; set; }
        public string conexion { get; set; }
        public string resultado { get; set; }
        public int opcion { get; set; }
        public bool Completado { get; set; }
        #endregion
    }
}