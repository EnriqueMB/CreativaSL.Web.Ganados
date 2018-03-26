using System;
using System.Collections.Generic;
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
        }
        private string _IDSucursal;

        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }

        private string _IDLugar;

        public string IDLugar
        {
            get { return _IDLugar; }
            set { _IDLugar = value; }
        }

        private string _Direccion;

        public string Direccion
        {
            get { return _Direccion; }
            set { _Direccion = value; }
        }
        private string _NombreSucursal;

        public string NombreSucursal
        {
            get { return _NombreSucursal; }
            set { _NombreSucursal = value; }
        }
        public decimal MermaPredeterminada { get; set; }

        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}