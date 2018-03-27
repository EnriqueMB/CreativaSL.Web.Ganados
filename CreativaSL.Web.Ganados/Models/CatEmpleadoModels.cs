using System;
using System.Collections.Generic;
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
        }

        private string _IDEmpleado;

        public string IDEmpleado
        {
            get { return _IDEmpleado; }
            set { _IDEmpleado = value; }
        }

        private int _IDTipoUsuario;

        public int IDTipoUsuario
        {
            get { return _IDTipoUsuario; }
            set { _IDTipoUsuario = value; }
        }

        private bool _AbrirCaja;

        public bool AbrirCaja
        {
            get { return _AbrirCaja; }
            set { _AbrirCaja = value; }
        }

        private string _CodigoUsuario;

        public string CodigoUsuario
        {
            get { return _CodigoUsuario; }
            set { _CodigoUsuario = value; }
        }
        private string _NombreCompleto;

        public string NombreCompleto
        {
            get { return _NombreCompleto; }
            set { _NombreCompleto = value; }
        }

        private string _Nombre;

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        private string _ApellidoPat;

        public string ApellidoPat
        {
            get { return _ApellidoPat; }
            set { _ApellidoPat = value; }
        }

        private string _ApellidoMat;

        public string ApellidoMat
        {
            get { return _ApellidoMat; }
            set { _ApellidoMat = value; }
        }

        private bool _AltaNominal;

        public bool AltaNominal
        {
            get { return _AltaNominal; }
            set { _AltaNominal = value; }
        }

        private int _IDPuesto;

        public int IDPuesto
        {
            get { return _IDPuesto; }
            set { _IDPuesto = value; }
        }

        private string _IDCategoriaPuesto;

        public string IDCategoriaPuesto
        {
            get { return _IDCategoriaPuesto; }
            set { _IDCategoriaPuesto = value; }
        }

        private string _IDSucursalActual;

        public string IDSucursalActual
        {
            get { return _IDSucursalActual; }
            set { _IDSucursalActual = value; }
        }

        private string _DirCalle;

        public string DirCalle
        {
            get { return _DirCalle; }
            set { _DirCalle = value; }
        }

        private string _DirColonia;

        public string DirColonia
        {
            get { return _DirColonia; }
            set { _DirColonia = value; }
        }

        private string _DirNumero;

        public string DirNumero
        {
            get { return _DirNumero; }
            set { _DirNumero = value; }
        }

        private int _IDGrupoSanguineo;

        public int IDGrupoSanguineo
        {
            get { return _IDGrupoSanguineo; }
            set { _IDGrupoSanguineo = value; }
        }

        private string _NombreSucursal;

        public string NombreSucursal
        {
            get { return _NombreSucursal; }
            set { _NombreSucursal = value; }
        }

        private string _NombrePuesto;

        public string NombrePuesto
        {
            get { return _NombrePuesto; }
            set { _NombrePuesto = value; }
        }

        private string _NombreCategoriaP;

        public string NombreCategoriaP
        {
            get { return _NombreCategoriaP; }
            set { _NombreCategoriaP = value; }
        }

        private string _GrupoSanguineo;

        public string GrupoSanguineo
        {
            get { return _GrupoSanguineo; }
            set { _GrupoSanguineo = value; }
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