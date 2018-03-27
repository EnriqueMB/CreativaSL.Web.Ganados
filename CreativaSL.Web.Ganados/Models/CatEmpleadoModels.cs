using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatEmpleadoModels
    {
        public CatEmpleadoModels()
        {
            _IDEmpleado = string.Empty;
            _IDTipoUsuario = 0;
            _AbrirCaja = false;
            _CodigoUsuario = string.Empty;
            _NombreCompleto = string.Empty;
            _Nombre = string.Empty;
            _ApellidoPat = string.Empty;
            _ApellidoMat = string.Empty;
            _AltaNominal = false;
            _IDPuesto = 0;
            _IDCategoriaPuesto = string.Empty;
            _IDSucursalActual = string.Empty;
            _DirCalle = string.Empty;
            _DirColonia = string.Empty;
            _DirNumero = string.Empty;
            _IDGrupoSanguineo = 0;
            Conexion = string.Empty;
            Usuario = string.Empty;
            _NombreCategoriaP = string.Empty;
            _NombrePuesto = string.Empty;
            _NombreSucursal = string.Empty;
            _ListaEmpleado = new List<CatEmpleadoModels>();
            _ListaCmbSucursal = new List<CatSucursalesModels>();
            _ListaCmbPuesto = new List<CatPuestoModels>();
            _ListaCmbGrupoSanguineo = new List<CatGrupoSanguineoModels>();
            _ListaCmbCategoriaPuesto = new List<CatCategoriaPuestoModels>();
            _Telefono = string.Empty;
        }

        private string _IDEmpleado;
        /// <summary>
        /// Identificador que le pertenece al Empleado
        /// </summary>
        public string IDEmpleado
        {
            get { return _IDEmpleado; }
            set { _IDEmpleado = value; }
        }

        private int _IDTipoUsuario;
        /// <summary>
        /// El tipo se usuario que es el Empleado
        /// </summary>
        public int IDTipoUsuario
        {
            get { return _IDTipoUsuario; }
            set { _IDTipoUsuario = value; }
        }

        private bool _AbrirCaja;
        /// <summary>
        /// Ver si el Empleado Abre caja 
        /// </summary>
        public bool AbrirCaja
        {
            get { return _AbrirCaja; }
            set { _AbrirCaja = value; }
        }

        private string _CodigoUsuario;
        /// <summary>
        /// Codigo del sistema como Identificador del Empleado
        /// </summary>
        public string CodigoUsuario
        {
            get { return _CodigoUsuario; }
            set { _CodigoUsuario = value; }
        }
        private string _NombreCompleto;
        /// <summary>
        /// Nombre Completo del empleado
        /// </summary>
        public string NombreCompleto
        {
            get { return _NombreCompleto; }
            set { _NombreCompleto = value; }
        }

        private string _Nombre;
        /// <summary>
        /// Nombre del Empleado
        /// </summary>
        [Required(ErrorMessage = "Debe ingresar el nombre del empleado")]
        [Display(Name = "Nombre")]
        [StringLength(70, ErrorMessage = "El número de caracteres del campo {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Texto(ErrorMessage = "Ingrese un dato válido para Nombre")]
        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        private string _ApellidoPat;
        /// <summary>
        /// Apellido Paterno del Empleado
        /// </summary>
        [Required(ErrorMessage = "Debe ingresar el apellido paterno del empleado")]
        [Display(Name = "Apellido Paterno")]
        [StringLength(50, ErrorMessage = "El número de caracteres del campo {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Texto(ErrorMessage = "Ingrese un dato válido para el apellido paterno")]
        public string ApellidoPat
        {
            get { return _ApellidoPat; }
            set { _ApellidoPat = value; }
        }

        private string _ApellidoMat;
        /// <summary>
        /// Apellido Materno del Empleado
        /// </summary>
        [Display(Name = "Apellido Materno")]
        [StringLength(50, ErrorMessage = "El número de caracteres del campo {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        public string ApellidoMat
        {
            get { return _ApellidoMat; }
            set { _ApellidoMat = value; }
        }

        private bool _AltaNominal;
        /// <summary>
        /// Ver Si el Empleado esta en la nomina
        /// </summary>
        public bool AltaNominal
        {
            get { return _AltaNominal; }
            set { _AltaNominal = value; }
        }

        private int _IDPuesto;
        /// <summary>
        /// Identificador del puesto que le pertenece al Empleado
        /// </summary>
        [Required(ErrorMessage = "Seleccione un puesto")]
        [Display(Name = "Puesto")]
        [CombosInt(ErrorMessage = "Selecciones un puesto valido")]
        public int IDPuesto
        {
            get { return _IDPuesto; }
            set { _IDPuesto = value; }
        }

        private string _IDCategoriaPuesto;
        /// <summary>
        /// Identificador de la categoria del puesto en la que le pertenece al Empleado
        /// </summary>
        [Required(ErrorMessage = "Seleccione una categoria puesto")]
        [Display(Name = "Categoria Puesto")]
        [CombosInt(ErrorMessage = "Selecciones una categoria puesto valido")]
        public string IDCategoriaPuesto
        {
            get { return _IDCategoriaPuesto; }
            set { _IDCategoriaPuesto = value; }
        }

        private string _IDSucursalActual;
        /// <summary>
        /// Identificacor de la sucursal actual del Empleado
        /// </summary>
        [Required(ErrorMessage = "Seleccione una sucursal")]
        [Display(Name = "Sucursal")]
        public string IDSucursalActual
        {
            get { return _IDSucursalActual; }
            set { _IDSucursalActual = value; }
        }

        private string _DirCalle;
        /// <summary>
        /// Nombre de la calle que le pertenece al Empleado
        /// </summary>
        [StringLength(90, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Display(Name = "Calle")]
        [Direccion(ErrorMessage = "Ingrese una calle valida")]
        public string DirCalle
        {
            get { return _DirCalle; }
            set { _DirCalle = value; }
        }

        private string _DirColonia;
        /// <summary>
        /// Nombre de la colonia en la que pertenece al Empleado
        /// </summary>
        [StringLength(90, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Display(Name = "Colonia")]
        [Direccion(ErrorMessage = "Ingrese una colonia valida")]
        public string DirColonia
        {
            get { return _DirColonia; }
            set { _DirColonia = value; }
        }

        private string _DirNumero;
        /// <summary>
        /// Número de casa que le pertenece al Empleado
        /// </summary>
        public string DirNumero
        {
            get { return _DirNumero; }
            set { _DirNumero = value; }
        }

        private int _IDGrupoSanguineo;
        /// <summary>
        /// Identificador de Grupo sanguineo en la que pertenece el empleado
        /// </summary>
        [Required(ErrorMessage = "Seleccione un grupo sanguineo")]
        [Display(Name = "Grupo sanguineo")]
        [CombosInt(ErrorMessage = "Selecciones un grupo sanguineo valido")]
        public int IDGrupoSanguineo
        {
            get { return _IDGrupoSanguineo; }
            set { _IDGrupoSanguineo = value; }
        }

        private string _NombreSucursal;
        /// <summary>
        /// Nombre de la sucursal en la que pertenece el empleado
        /// </summary>
        public string NombreSucursal
        {
            get { return _NombreSucursal; }
            set { _NombreSucursal = value; }
        }

        private string _NombrePuesto;
        /// <summary>
        /// Nombre del puesto del empleado
        /// </summary>
        public string NombrePuesto
        {
            get { return _NombrePuesto; }
            set { _NombrePuesto = value; }
        }

        private string _NombreCategoriaP;
        /// <summary>
        /// Nombre Categoria en la que pertenece el empleado
        /// </summary>
        public string NombreCategoriaP
        {
            get { return _NombreCategoriaP; }
            set { _NombreCategoriaP = value; }
        }

        private string _GrupoSanguineo;
        /// <summary>
        /// Grupo  sanguine del empleado
        /// </summary>
        public string GrupoSanguineo
        {
            get { return _GrupoSanguineo; }
            set { _GrupoSanguineo = value; }
        }

        private List<CatEmpleadoModels> _ListaEmpleado;
        /// <summary>
        /// Lista para llenar tabla Empleado
        /// </summary>
        public List<CatEmpleadoModels> ListaEmpleado
        {
            get { return _ListaEmpleado; }
            set { _ListaEmpleado = value; }
        }


        private List<CatSucursalesModels> _ListaCmbSucursal;
        /// <summary>
        /// Lista para llenar combo de sucursal
        /// </summary>
        public List<CatSucursalesModels> ListaCmbSucursal
        {
            get { return _ListaCmbSucursal; }
            set { _ListaCmbSucursal = value; }
        }

        private List<CatGrupoSanguineoModels> _ListaCmbGrupoSanguineo;
        /// <summary>
        /// Lista para llenar combo de grupo sanguineo
        /// </summary>
        public List<CatGrupoSanguineoModels> ListaCmbGrupoSanguineo
        {
            get { return _ListaCmbGrupoSanguineo; }
            set { _ListaCmbGrupoSanguineo = value; }
        }

        private List<CatPuestoModels> _ListaCmbPuesto;
        /// <summary>
        /// Lista para llenar combo de puesto 
        /// </summary>
        public List<CatPuestoModels> ListaCmbPuesto
        {
            get { return _ListaCmbPuesto; }
            set { _ListaCmbPuesto = value; }
        }

        private List<CatCategoriaPuestoModels> _ListaCmbCategoriaPuesto;
        /// <summary>
        /// Lista para llena combo Categoria Puesto
        /// </summary>
        public List<CatCategoriaPuestoModels> ListaCmbCategoriaPuesto
        {
            get { return _ListaCmbCategoriaPuesto; }
            set { _ListaCmbCategoriaPuesto = value; }
        }

        private string _Telefono;
        /// <summary>
        /// El telefono que le pertenece al empleado
        /// </summary>
        [Display(Name = "Teléfono")]
        [Telefono(ErrorMessage = "Ingrese un número telefónico válido")]
        public string Telefono
        {
            get { return _Telefono; }
            set { _Telefono = value; }
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