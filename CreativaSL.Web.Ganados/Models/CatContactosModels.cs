using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatContactosModels
    {
        public CatContactosModels() {
            this._IDCliente = string.Empty;
            this._IDProveedor = string.Empty;
            this._IDSucursal = string.Empty;
            this._IDContacto = string.Empty;
            this._listaContacto = new List<CatContactosModels>();
            this._nombreContacto = string.Empty;
            this._apMaterno = string.Empty;
            this._apPaterno = string.Empty;
            this._telefonoContacto = string.Empty;
            this._celularContacto = string.Empty;
            this._correo = string.Empty;
            this._direccion = string.Empty;
            this._observacion = string.Empty;
            Conexion = string.Empty;
            Resultado = 0;
            Completado = false;
            Usuario = string.Empty;
            Opcion = 0;
            
        }
        private string _IDSucursal;

        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }

        private string _IDProveedor;

        public string IDProveedor
        {
            get { return _IDProveedor; }
            set { _IDProveedor = value; }
        }

        private string _IDContacto;

        public string IDContacto
        {
            get { return _IDContacto; }
            set { _IDContacto = value; }
        }

        private string _IDCliente;

        public string IDCliente
        {
            get { return _IDCliente; }
            set { _IDCliente = value; }
        }

        private string _nombreContacto;
        [Required(ErrorMessage = "La dirección es obligatoria")]
        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Nombre(ErrorMessage = "Solo Letras")]
        public string nombreContacto
        {
            get { return _nombreContacto; }
            set { _nombreContacto = value; }
        }
       
        private string _apPaterno;
        [Required(ErrorMessage = "La dirección es obligatoria")]
        [Display(Name = "Apellido Paterno")]
        [StringLength(50, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Nombre(ErrorMessage = "Solo Letras")]
        public string apPaterno
        {
            get { return _apPaterno; }
            set { _apPaterno = value; }
        }
        private string _apMaterno;
        [Display(Name = "Apellido Materno")]
        [StringLength(50, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Nombre(ErrorMessage = "Solo Letras")]
        public string apMaterno
        {
            get { return _apMaterno; }
            set { _apMaterno = value; }
        }
        private string _correo;
        
        [Display(Name = "correo")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Correo(ErrorMessage = "Ingrese un formato válido para el correo")]
        public string correo
        {
            get { return _correo; }
            set { _correo = value; }
        }
        private string _telefonoContacto;

        [Display(Name = "Telefono")]
        [StringLength(25, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Telefono(ErrorMessage = "Solo  números")]
        public string telefonoContacto
        {
            get { return _telefonoContacto; }
            set { _telefonoContacto = value; }
        }
        private string _celularContacto;
        [Display(Name = "Celular")]
        [StringLength(25, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 1)]
        [Telefono(ErrorMessage = "Solo  números")]
        public string celularContacto
        {
            get { return _celularContacto; }
            set { _celularContacto = value; }
        }
        private string _direccion;

        [Display(Name = "direccion")]
        public string direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }
        private string _observacion;

        public string observacion
        {
            get { return _observacion; }
            set { _observacion = value; }
        }
        private List<CatContactosModels> _listaContacto;

        public List<CatContactosModels> listaContacto
        {
            get { return _listaContacto; }
            set { _listaContacto = value; }
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