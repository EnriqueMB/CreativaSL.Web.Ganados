using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Web;
using System.Web.Mvc;

namespace CreativaSL.Web.Ganados.Models
{
    public class UsuarioModels
    {
        private string _id_usuario;
        public string id_usuario
        {
            get { return _id_usuario; }
            set { _id_usuario = value; }
        }


        private int _id_tipoUsuario;
        public int id_tipoUsuario
        {
            get { return _id_tipoUsuario; }
            set { _id_tipoUsuario = value; }
        }

        private int _id_municipio;
        public int id_municipio
        {
            get { return _id_municipio; }
            set { _id_municipio = value; }
        }

        private int _id_estado;
        public int id_estado
        {
            get { return _id_estado; }
            set { _id_estado = value; }
        }

        private string _nombreCompleto;
        public string nombreCompleto
        {
            get { return _nombreCompleto; }
            set { _nombreCompleto = value; }
        }

        private string _nombre;
        [Required(ErrorMessage = "El Nombre es obligatorio")]
        [Display(Name = "Nombre")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ\s]*$", ErrorMessage = "Solo Letras")]
        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _apPat;
        [Required(ErrorMessage = "El Apellido Paterno es obligatorio")]
        [Display(Name = "Apellido Paterno")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ\s]*$", ErrorMessage = "Solo Letras")]
        public string apPat
        {
            get { return _apPat; }
            set { _apPat = value; }
        }

        private string _apMat;
        [Required(ErrorMessage = "El Apellido Materno es obligatorio")]
        [Display(Name = "Apellido Materno")]
        [StringLength(50, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ\s]*$", ErrorMessage = "Solo Letras")]
        public string apMat
        {
            get { return _apMat; }
            set { _apMat = value; }
        }

        private DateTime _fechaNac = DateTime.Now;
        [Required(ErrorMessage = "La Fecha de Nacimiento es obligatorio")]
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaNac
        {
            get { return _fechaNac; }
            set { _fechaNac = value; }
        }

        private DateTime _fechaAntiguedad = DateTime.Now;
        [Required(ErrorMessage = "La Fecha de Antiguedad es obligatorio")]
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime fechaAntiguedad
        {
            get { return _fechaAntiguedad; }
            set { _fechaAntiguedad = value; }
        }

        private string _direccion;
        [Display(Name = "Dirección")]
        [StringLength(1000, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [RegularExpression(@"^[A-Za-záéíóúñÁÉÍÓÚÑ0-9\(\)\-\,\.\;\:\s]*$", ErrorMessage = "Solo Letras y números")]
        public string direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        private string _codigoPostal;

        public string codigoPostal
        {
            get { return _codigoPostal; }
            set { _codigoPostal = value; }
        }

        private string _telefono;
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefono")]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Solo números")]    
        public string telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        private string _email;
        [Required(ErrorMessage = "El Correo es obligatorio")]
        [Display(Name = "Correo")]
        [RegularExpression(@"^[_A-Za-z0-9-.\\+]+(\\.[_A-Za-z0-9-.]+)*@" + "[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "Correo no Valido")]
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Remote("CheckEmailAvailability", "Account", AdditionalFields = "id_usuario", ErrorMessage = "Este email esta ocupado")]
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }

        private string _url_foto;

        public string url_foto
        {
            get { return _url_foto; }
            set { _url_foto = value; }
        }
        private string _cuenta;
        [Required(ErrorMessage = "El Usuario es obligatorio")]
        [Display(Name = "Usuario")]
        [StringLength(15, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Remote("CheckUserAvailability", "Account", AdditionalFields = "id_usuario", ErrorMessage = "Este usuario esta ocupado")]
        public string cuenta
        {
            get { return _cuenta; }
            set { _cuenta = value; }
        }

        private string _contraseña;
        [Required(ErrorMessage = "El Contraseña es obligatorio")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [StringLength(32, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        public string contraseña
        {
            get { return _contraseña; }
            set { _contraseña = value; }
        }

        private bool _activo;

        public bool activo
        {
            get { return _activo; }
            set { _activo = value; }
        }

        private string _user;
        [Required(ErrorMessage = "El Usuario es obligatorio")]
        [Display(Name = "Usuario")]
        [StringLength(15, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        public string user
        {
            get { return _user; }
            set { _user = value; }
        }

        private string _password;
        [Required(ErrorMessage = "La Contraseña es obligatorio")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [StringLength(32, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        public string password
        {
            get { return _password; }
            set { _password = value; }
        }

        private DataTable _tablaUsuario;

        public DataTable tablaUsuario
        {
            get { return _tablaUsuario; }
            set { _tablaUsuario = value; }
        }

        private string _tipoUsuario;

        public string tipoUsuario
        {
            get { return _tipoUsuario; }
            set { _tipoUsuario = value; }
        }

        private List<TipoUsuarioModels> _tablaTipoUsuariosCmb;
        [Required(ErrorMessage = "Tipo de Usuario es un campo requerido")]
        [Display(Name = "Tipo de Usuario")]
        public List<TipoUsuarioModels> tablaTipoUsuariosCmb
        {
            get { return _tablaTipoUsuariosCmb; }
            set { _tablaTipoUsuariosCmb = value; }
        }

        private HttpPostedFileBase _foto;
        [Required(ErrorMessage = "La Imagen es obligatorio")]
        [Display(Name = "Imagen")]
        [FileExtensions(Extensions = "png,jpg,jpeg", ErrorMessage = "Solo imagenes")]
        public HttpPostedFileBase foto
        {
            get { return _foto; }
            set { _foto = value; }
        }

        private HttpPostedFileBase _foto2;
        [Display(Name = "Imagen")]
        [FileExtensions(Extensions = "png,jpg,jpeg", ErrorMessage = "Solo imagenes")]
        public HttpPostedFileBase foto2
        {
            get { return _foto2; }
            set { _foto2 = value; }
        }

        private List<EstadoModels> _tablaEstadoCmb;
        [Required(ErrorMessage = "Estado es obligatorio")]
        [Display(Name = "Estado")]
        public List<EstadoModels> tablaEstadoCmb
        {
            get { return _tablaEstadoCmb; }
            set { _tablaEstadoCmb = value; }
        }

        private List<MunicipioModels> _tablaMunicipioCmb;
        [Required(ErrorMessage = "Municipio es obligatorio")]
        [Display(Name = "Municipio")]
        public List<MunicipioModels> tablaMunicipioCmb
        {
            get { return _tablaMunicipioCmb; }
            set { _tablaMunicipioCmb = value; }
        }
        private List<MenuModels> _listaMenu;

        public List<MenuModels> listaMenu
        {
            get { return _listaMenu; }
            set { _listaMenu = value; }
        }

        private string  _clvUser;

        public string  clvUser
        {
            get { return _clvUser; }
            set { _clvUser = value; }
        }
        private string _passUser;

        public string passUser
        {
            get { return _passUser; }
            set { _passUser = value; }
        }

        private string _NombreUrl;

        public string NombreUrl
        {
            get { return _NombreUrl; }
            set { _NombreUrl = value; }
        }

        

        //VARIABLES PARA PERMISOS POR USUARIO
        private DataTable _TablaPermisos;

        public DataTable TablaPermisos
        {
            get { return _TablaPermisos; }
            set { _TablaPermisos = value; }
        }
        private List<UsuarioModels> _ListaPermisos;

        public List<UsuarioModels> ListaPermisos
        {
            get { return _ListaPermisos; }
            set { _ListaPermisos = value; }
        }

        private List<UsuarioModels> _ListaPermisosDetalle;

        public List<UsuarioModels> ListaPermisosDetalle
        {
            get { return _ListaPermisosDetalle; }
            set { _ListaPermisosDetalle = value; }
        }
        private string _IDPermiso;

        public string IDPermiso
        {
            get { return _IDPermiso; }
            set { _IDPermiso = value; }
        }

        private int _IDMenu;

        public int IDMenu
        {
            get { return _IDMenu; }
            set { _IDMenu = value; }
        }

        private bool _Ver;

        public bool ver
        {
            get { return _Ver; }
            set { _Ver = value; }
        }

        private string _NombreMenu;

        public string NombreMenu
        {
            get { return _NombreMenu; }
            set { _NombreMenu = value; }
        }
        private int _numeroMenu;

        public int numeroMenu
        {
            get { return _numeroMenu; }
            set { _numeroMenu = value; }
        }

        #region Datos de control
        private int _opcion;

        public int opcion
        {
            get { return _opcion; }
            set { _opcion = value; }
        }

        private string _conexion;

        public string conexion
        {
            get { return _conexion; }
            set { _conexion = value; }
        }

        private bool _RememberMe;

        public bool RememberMe
        {
            get { return _RememberMe; }
            set { _RememberMe = value; }
        }

        #endregion

        private string _email2;
        [Required(ErrorMessage = "El Correo es obligatorio")]
        [Display(Name = "Correo")]
        [RegularExpression(@"^[_A-Za-z0-9-.\\+]+(\\.[_A-Za-z0-9-.]+)*@" + "[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "Correo no Valido")]
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        public string email2
        {
            get { return _email2; }
            set { _email2 = value; }
        }
        #region Datos de control
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        #endregion
    }
}