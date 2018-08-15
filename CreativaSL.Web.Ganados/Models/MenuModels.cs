using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class MenuModels
    {

        public MenuModels() {
            _NombreMenu = string.Empty;
            _UrlMenu = string.Empty;
            _IconMenu = string.Empty;
            _TablaDatos = new DataTable();
            _ListaMenu = new List<MenuModels>();
            _ListaMenuDetalle = new List<MenuModels>();
            _ListaPermisoDetalle = new List<MenuModels>();
            _ListaPermisos = new List<MenuModels>();
            _IDPermiso = string.Empty;
            Conexion = string.Empty;
        }

        private int _MenuID;

        public int MenuID
        {
            get { return _MenuID; }
            set { _MenuID = value; }
        }

        private string _NombreMenu;

        public string NombreMenu
        {
            get { return _NombreMenu; }
            set { _NombreMenu = value; }
        }

        private int _IDSubMenu;

        public int IDSubMenu
        {
            get { return _IDSubMenu; }
            set { _IDSubMenu = value; }
        }

        private int _OrdenMenu;

        public int OrdenMenu
        {
            get { return _OrdenMenu; }
            set { _OrdenMenu = value; }
        }

        private string _UrlMenu;

        public string UrlMenu
        {
            get { return _UrlMenu; }
            set { _UrlMenu = value; }
        }

        private string _IconMenu;

        public string IconMenu
        {
            get { return _IconMenu; }
            set { _IconMenu = value; }
        }

        public string Conexion { get; set; }

        private DataTable _TablaDatos;

        public DataTable TablaDatos
        {
            get { return _TablaDatos; }
            set { _TablaDatos = value; }
        }

        private List<MenuModels> _ListaMenu;

        public List<MenuModels> ListaMenu
        {
            get { return _ListaMenu; }
            set { _ListaMenu = value; }
        }

        private List<MenuModels> _ListaMenuDetalle;

        public List<MenuModels> ListaMenuDetalle
        {
            get { return _ListaMenuDetalle; }
            set { _ListaMenuDetalle = value; }
        }
        public string IDPersona { get; set; }
        public string User { get; set; }

        private List<MenuModels> _ListaPermisos;

        public List<MenuModels> ListaPermisos
        {
            get { return _ListaPermisos; }
            set { _ListaPermisos = value; }
        }

        private List<MenuModels> _ListaPermisoDetalle;

        public List<MenuModels> ListaPermisoDetalle
        {
            get { return _ListaPermisoDetalle; }
            set { _ListaPermisoDetalle = value; }
        }

        private string _IDPermiso;

        public string IDPermiso
        {
            get { return _IDPermiso; }
            set { _IDPermiso = value; }
        }

        private bool _ver;

        public bool ver
        {
            get { return _ver; }
            set { _ver = value; }
        }

        private int _TipoMenu;

        public int TipoMenu
        {
            get { return _TipoMenu; }
            set { _TipoMenu = value; }
        }

        private int _ParentMenuID;

        public int ParentMenuID
        {
            get { return _ParentMenuID; }
            set { _ParentMenuID = value; }
        }

        private int _NumRow;

        public int NumRow
        {
            get { return _NumRow; }
            set { _NumRow = value; }
        }

    }
}