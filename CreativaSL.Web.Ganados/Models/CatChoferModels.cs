using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data;
using CreativaSL.Web.Ganados.Models.Validaciones;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatChoferModels
    {
        public CatChoferModels() {

            _id_licencia = string.Empty;
            _numLicencia = string.Empty;
            _vigencia = DateTime.Now;
            _IDChofer = string.Empty;
            _Nombre = string.Empty;
            _ApPaterno = string.Empty;
            _ApMaterno = string.Empty;
            _Licencia = true;
            _listaGrupoSanguineo = new List<CatGrupoSanguineoModels>();
            _idgruposanguineo = 0;
            _Estatus = false;
            _ListaChoferes = new List<CatChoferModels>();
            _Ife = string.Empty;
            _listaSucursales = new List<CatSucursalesModels>();
            _IDSucursal = string.Empty;
            _IDGenero = 0;
            _NumSeguroSocial = string.Empty;
            _AvisoAccidente = string.Empty;
            _TelefonoAccidente = string.Empty;
            _Telefono = string.Empty;
            _Movil = string.Empty;
            _FechaNacimiento = DateTime.Now.AddYears(-15);
            _FechaIngreso = DateTime.Now;
            _ListaGeneroCMB = new List<CatGeneroModels>();
            //Datos de control
            Conexion = string.Empty;
            Resultado = 0;
            Completado = false;
            Usuario = string.Empty;
            Opcion = 0;
            ListaEmpresas = new List<CatEmpresaModels>();
            IDEmpresa = string.Empty;
        }

        public List<CatEmpresaModels> ListaEmpresas { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Seleccione una empresa.")]
        public string IDEmpresa { get; set; }

        private List<CatSucursalesModels> _listaSucursales;

        public List<CatSucursalesModels> listaSucursales
        {
            get { return _listaSucursales; }
            set { _listaSucursales = value; }
        }
        private string _IDSucursal;

        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }

        /// <summary>
        /// LICENCIA DEL CHOFER
        /// </summary>
        private string _id_licencia;

        public string id_licencia
        {
            get { return _id_licencia; }
            set { _id_licencia = value; }
        }
        /// <summary>
        /// NUMERO DE LICENCIA DEL CHOFER
        /// </summary>
        private string _numLicencia;
        [Display(Name = "número licencia")]
        [StringLength(20, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Texto(ErrorMessage = "Solo Letras y número")]
        public string numLicencia
        {
            get { return _numLicencia; }
            set { _numLicencia = value; }
        }
        /// <summary>
        /// VIGENCIA DE LA LICENCIA
        /// </summary>
        private DateTime _vigencia;
       // [Required(ErrorMessage = "La Fecha de nacimiento es obligatorio")]
        [Display(Name = "Vigencia")]

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime vigencia
        {
            get { return _vigencia; }
            set { _vigencia = value; }
        }
        private List<CatChoferModels> _ListaChoferes;

        public List<CatChoferModels> ListaChoferes
        {
            get { return _ListaChoferes; }
            set { _ListaChoferes = value; }
        }

        /// <summary>
        /// El Identificacidor de por chofer
        /// </summary>
        private string _IDChofer;

        public string IDChofer
        {
            get { return _IDChofer; }
            set { _IDChofer = value; }
        }
        /// <summary>
        /// El nombre del chofer
        /// </summary>
        private string _Nombre;
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "nombre")]
        [StringLength(80, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ\s]*$", ErrorMessage = "Solo Letras")]
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }
        /// <summary>
        /// El Apellido Parteno del Chofer
        /// </summary>
        private string _ApPaterno;
        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        [Display(Name = "apellido paterno")]
        [StringLength(70, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ\s]*$", ErrorMessage = "Solo Letras")]
        public string ApPaterno
        {
            get { return _ApPaterno; }
            set { _ApPaterno = value; }
        }
        /// <summary>
        /// El Apellido Materno del Chofer
        /// </summary>
        private string _ApMaterno;
        [Display(Name = "apellido materno")]
        [StringLength(70, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ\s]*$", ErrorMessage = "Solo Letras")]
        public string ApMaterno
        {
            get { return _ApMaterno; }
            set { _ApMaterno = value; }
        }
        /// <summary>
        /// Para ver si su Licencia del Chofer si tiene vigencia
        /// </summary>
        private bool _Licencia;

        public bool Licencia
        {
            get { return _Licencia; }
            set { _Licencia = value; }
        }
        /// <summary>
        /// Para saber si el chofer no se encuentra en viaje
        /// </summary>
        private bool _Estatus;

        public bool Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }

        private DataTable _TablaDatos;

        public DataTable TablaDatos
        {
            get { return _TablaDatos; }
            set { _TablaDatos = value; }
        }

        private string _Ife;
        [Required(ErrorMessage = "El número del ife es obligatorio")]
        [Display(Name = "Número del ife")]
        [StringLength(13, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [IFE(ErrorMessage = "Ingrese un dato válido para credencial de elector")]
        public string Ife
        {
            get { return _Ife; }
            set { _Ife = value; }
        }

       

        private int _IDGenero;

        public int IDGenero
        {
            get { return _IDGenero; }
            set { _IDGenero = value; }
        }

        private string _NumSeguroSocial;
       
        [Display(Name = "número de seguro social")]
        [StringLength(30, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Texto( ErrorMessage = "Solo Letras y número")]
        public string NumSeguroSocial
        {
            get { return _NumSeguroSocial; }
            set { _NumSeguroSocial = value; }
        }

        private string _AvisoAccidente;
        [Required(ErrorMessage = "El nombre encaso de accidente es obligatorio")]
        [Display(Name = "nombre encaso de accidente")]
        [StringLength(250, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Texto(ErrorMessage = "Solo Letras y número")]
        public string AvisoAccidente
        {
            get { return _AvisoAccidente; }
            set { _AvisoAccidente = value; }
        }

        private string _TelefonoAccidente;
    
        public string TelefonoAccidente
        {
            get { return _TelefonoAccidente; }
            set { _TelefonoAccidente = value; }
        }

        private string _Telefono;

        public string Telefono
        {
            get { return _Telefono; }
            set { _Telefono = value; }
        }

        private string _Movil;

        public string Movil
        {
            get { return _Movil; }
            set { _Movil = value; }
        }

        private DateTime _FechaNacimiento;
        [Required(ErrorMessage = "La Fecha de nacimiento es obligatorio")]
        [Display(Name = "Fecha de nacimiento")]
      
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento
        {
            get { return _FechaNacimiento; }
            set { _FechaNacimiento = value; }
        }

        private DateTime _FechaIngreso;
        [Required(ErrorMessage = "La Fecha de ingreso es obligatorio")]
        [Display(Name = "Fecha de ingreso")]
     
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaIngreso
        {
            get { return _FechaIngreso; }
            set { _FechaIngreso = value; }
        }
        private List<CatGrupoSanguineoModels> _listaGrupoSanguineo;

        public List<CatGrupoSanguineoModels> listaGrupoSanguineo
        {
            get { return _listaGrupoSanguineo; }
            set { _listaGrupoSanguineo = value; }
        }
        private int _idgruposanguineo;

        public int idgruposanguineo
        {
            get { return _idgruposanguineo; }
            set { _idgruposanguineo = value; }
        }

        private List<CatGeneroModels> _ListaGeneroCMB;

        public List<CatGeneroModels> ListaGeneroCMB
        {
            get { return _ListaGeneroCMB; }
            set { _ListaGeneroCMB = value; }
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