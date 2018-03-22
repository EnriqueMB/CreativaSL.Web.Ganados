using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data;

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
            _Estatus = false;
            _ListaChoferes = new List<CatChoferModels>();
            _Ife = string.Empty;
            _TipoSangre = string.Empty;
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
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\s]*$", ErrorMessage = "Solo Letras y número")]
        public string numLicencia
        {
            get { return _numLicencia; }
            set { _numLicencia = value; }
        }
        /// <summary>
        /// VIGENCIA DE LA LICENCIA
        /// </summary>
        private DateTime _vigencia;

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
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\s]*$", ErrorMessage = "Solo Letras y número")]
        public string Ife
        {
            get { return _Ife; }
            set { _Ife = value; }
        }

        private string _TipoSangre;
        [Required(ErrorMessage = "El tipo de sangre es obligatorio")]
        [Display(Name = "tipo de sangre")]
        [StringLength(15, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\s]*$", ErrorMessage = "Solo Letras y número")]
        public string TipoSangre
        {
            get { return _TipoSangre; }
            set { _TipoSangre = value; }
        }

        private int _IDGenero;

        public int IDGenero
        {
            get { return _IDGenero; }
            set { _IDGenero = value; }
        }

        private string _NumSeguroSocial;
        [Required(ErrorMessage = "El número de seguro social es obligatorio")]
        [Display(Name = "número de seguro social")]
        [StringLength(30, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\s]*$", ErrorMessage = "Solo Letras y número")]
        public string NumSeguroSocial
        {
            get { return _NumSeguroSocial; }
            set { _NumSeguroSocial = value; }
        }

        private string _AvisoAccidente;
        [Required(ErrorMessage = "El nombre encaso de accidente es obligatorio")]
        [Display(Name = "nombre encaso de accidente")]
        [StringLength(250, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ\s]*$", ErrorMessage = "Solo Letras y número")]
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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento
        {
            get { return _FechaNacimiento; }
            set { _FechaNacimiento = value; }
        }

        private DateTime _FechaIngreso;
        [Required(ErrorMessage = "La Fecha de ingreso es obligatorio")]
        [Display(Name = "Fecha de ingreso")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaIngreso
        {
            get { return _FechaIngreso; }
            set { _FechaIngreso = value; }
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