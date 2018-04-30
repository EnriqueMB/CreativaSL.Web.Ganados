using System;
using System.Collections.Generic;
using CreativaSL.Web.Ganados.Models;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CreativaSL.Web.Ganados.ViewModels
{
    public class CapturarCompraViewModels
    {
        public CapturarCompraViewModels()
        {
            this._IDCompraAlmcen = string.Empty;
            this._IDProveedorAlmacen = string.Empty;
            this._IDSucursal = string.Empty;
            this._NumFactNota = string.Empty;
            this._FechaCompra = DateTime.Today;
            this._ListaSucursal = new List<CatSucursalesModels>();
            this._ListaProveedor = new List<CatProveedorModels>();
        }
        private string _IDCompraAlmcen;
        
        public string IDCompraAlmacen
        {
            get { return _IDCompraAlmcen; }
            set { _IDCompraAlmcen = value; }
        }

        private string _IDSucursal;
        [Display(Name = "Sucursal")]
        [Required(ErrorMessage = "Seleccione una sucursal")]
        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }
        
        private string _IDProveedorAlmacen;
        [Display(Name = "Proveedor")]
        [Required(ErrorMessage = "Seleccione un proveedor")]
        public string IDProveedorAlmacen
        {
            get { return _IDProveedorAlmacen; }
            set { _IDProveedorAlmacen = value; }
        }

        private string _NumFactNota;
        [Display(Name = "Num Factura|Nota")]
        [Required(ErrorMessage = "Ingrese un número de Factura|Nota")]
        public string NumFactNota
        {
            get { return _NumFactNota; }
            set { _NumFactNota = value; }
        }
        private DateTime _FechaCompra;

        public DateTime FechaCompra
        {
            get { return _FechaCompra; }
            set { _FechaCompra = value; }
        }

        private List<CatSucursalesModels> _ListaSucursal;

        public List<CatSucursalesModels> ListaSucursal
        {
            get { return _ListaSucursal; }
            set { _ListaSucursal = value; }
        }

        private List<CatProveedorModels> _ListaProveedor;

        public List<CatProveedorModels> ListaProveedor
        {
            get { return _ListaProveedor; }
            set { _ListaProveedor = value; }
        }

    }
}