using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatSucursalesModels
    {
        public CatSucursalesModels()
        {
            _IDSucursal = string.Empty;
            _IDLugar = string.Empty;
            _Direccion = string.Empty;
            _NombreSucursal = string.Empty;
            Conexion = string.Empty;
            Usuario = string.Empty;
            MermaPredeterminada = 0;
            ListaSucursales = new List<CatSucursalesModels>();
            NombreSucursalMatriz = string.Empty;
            IDEmpresa = string.Empty;
            Mensaje = string.Empty;
        }

        private string _IDSucursal;
        private string _IDLugar;
        private string _Direccion;
        private string _NombreSucursal;

        [RegularExpression("^[0-9]*$",
            ErrorMessage = "Solo número enteros positivos.")]
        [DisplayName("Merma predeterminada")]
        public decimal MermaPredeterminada { get; set; }
        public List<CatSucursalesModels> ListaSucursales { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 5)]
        public string NombreSucursalMatriz { get; set; }
        public string IDEmpresa { get; set; }

        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }
        public string IDLugar
        {
            get { return _IDLugar; }
            set { _IDLugar = value; }
        }
        [Required]
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 5)]
        public string Direccion
        {
            get { return _Direccion; }
            set { _Direccion = value; }
        }
        [Required]
        [StringLength(300, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y un maximo de {1}.", MinimumLength = 5)]
        public string NombreSucursal
        {
            get { return _NombreSucursal; }
            set { _NombreSucursal = value; }
        }

        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Mensaje { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}