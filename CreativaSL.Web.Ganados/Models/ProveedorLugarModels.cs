using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ProveedorLugarModels
    {
        public ProveedorLugarModels()
        {
            _IDProveedorLugar = string.Empty;
            _IDProveedor = string.Empty;
            _IDLugar = string.Empty;
            Conexion = string.Empty;
            Usuario = string.Empty;
            _ListaProveedorLugar = new List<ProveedorLugarModels>();
            _ListaSucursale = new List<CatSucursalesModels>();
            _ListaLugares = new List<CatLugarModels>();
            _IDSucursal = string.Empty; 
        }

        private string _IDSucursal;

        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }


        private string _IDProveedorLugar;

        public string IDProveedorLugar
        {
            get { return _IDProveedorLugar; }
            set { _IDProveedorLugar = value; }
        }

        private string _IDProveedor;

        public string IDProveedor
        {
            get { return _IDProveedor; }
            set { _IDProveedor = value; }
        }

        private string _IDLugar;
        [Required(ErrorMessage = "Seleccione un Lugar")]
        [Display(Name = "Lugar")]
        public string IDLugar
        {
            get { return _IDLugar; }
            set { _IDLugar = value; }
        }

        private string _NombreLugar;

        public string NombreLugar
        {
            get { return _NombreLugar; }
            set { _NombreLugar = value; }
        }

        private bool _Bascula;

        public bool Bascula
        {
            get { return _Bascula; }
            set { _Bascula = value; }
        }

        private List<ProveedorLugarModels> _ListaProveedorLugar;

        public List<ProveedorLugarModels> ListaProveedorLugar
        {
            get { return _ListaProveedorLugar; }
            set { _ListaProveedorLugar = value; }
        }

        private List<CatSucursalesModels> _ListaSucursale;

        public List<CatSucursalesModels> ListaSucursales
        {
            get { return _ListaSucursale; }
            set { _ListaSucursale = value; }
        }

        private List<CatLugarModels> _ListaLugares;

        public List<CatLugarModels> ListaLugares
        {
            get { return _ListaLugares; }
            set { _ListaLugares = value; }
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