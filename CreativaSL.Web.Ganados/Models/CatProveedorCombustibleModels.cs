using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatProveedorCombustibleModels
    {
        public CatProveedorCombustibleModels()
        {
            _IDProveedor = string.Empty;
            _IDSucursal = string.Empty;
            _NombreRazonSocial = string.Empty;
            _RFC = string.Empty;
            _Direccion = string.Empty;
            _telefonoCelular = string.Empty;
            _telefonoCasa = string.Empty;
            _correo = string.Empty;
            _fechaIngreso = DateTime.Now;
            _observaciones = string.Empty;
            _listaProveedoresCombustible = new List<CatProveedorCombustibleModels>();
            _listaSucursal = new List<CatSucursalesModels>();
        }
        private List<CatContactosModels> _listaDatosContactos;

        public List<CatContactosModels> listaDatosContactos
        {
            get { return _listaDatosContactos; }
            set { _listaDatosContactos = value; }
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
        [Required(ErrorMessage = "El Correo es obligatorio")]
        [Display(Name = "Correo")]
        [RegularExpression(@"^[_A-Za-z0-9-.\\+]+(\\.[_A-Za-z0-9-.]+)*@" + "[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "Correo no Valido")]
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        public string correo
        {
            get { return _correo; }
            set { _correo = value; }
        }

        private string _Direccion;
        [Display(Name = "Dirección")]
        [Direccion(ErrorMessage = "Ingrese un datos válido para {0}")]
        public string Direccion
        {
            get { return _Direccion; }
            set { _Direccion = value; }
        }

        private List<CatSucursalesModels> _listaSucursal;

        public List<CatSucursalesModels> listaSucursal
        {
            get { return _listaSucursal; }
            set { _listaSucursal = value; }
        }

        private string _nombreSucursal;

        public string nombreSucursal
        {
            get { return _nombreSucursal; }
            set { _nombreSucursal = value; }
        }

        private List<CatProveedorCombustibleModels> _listaProveedoresCombustible;

        public List<CatProveedorCombustibleModels> listaProveedoresCombustible
        {
            get { return _listaProveedoresCombustible; }
            set { _listaProveedoresCombustible = value; }
        }

        private string _IDProveedor;

        public string IDProveedor
        {
            get { return _IDProveedor; }
            set { _IDProveedor = value; }
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

        private DateTime _fechaIngreso;
        [Display(Name = "Fecha de inicio laboral")]
        public DateTime fechaIngreso
        {
            get { return _fechaIngreso; }
            set { _fechaIngreso = value; }
        }

        private string _observaciones;

        public string observaciones
        {
            get { return _observaciones; }
            set { _observaciones = value; }
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