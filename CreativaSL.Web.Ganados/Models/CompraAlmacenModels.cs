using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CompraAlmacenModels
    {

        /// <summary>
        /// Inicializar los datos del modelo
        /// </summary>
        public CompraAlmacenModels()
        {
            this._IDCompraAlmacen = string.Empty;
            //Se da por hecho que CatSucursalModels tiene un constructor que inicializa sus datos
            this._Sucursal = new CatSucursalesModels();
            this._Proveedor = new CatProveedorModels();
            this._NumFacturaNota = string.Empty;
            this._MontoTotal = 0;
            this._fecha = DateTime.Today;//**+
            this._IDEstatusCompra = 0;
            this._IDDocumentoXPagar = string.Empty;
            this._ListaSucursales = new List<CatSucursalesModels>();
            this._ListaProveedores = new List<CatProveedorModels>();
            this._ListaCompras = new List<CompraAlmacenModels>();
            this.Conexion = string.Empty;
            this.Resultado = 0;
            this.Completado = false;
            this.Usuario = string.Empty;
            this.Opcion = 0;
        }

        
        private string _IDCompraAlmacen;
        /// <summary>
        /// Identificador primario de la compra para almacén
        /// </summary>
        public string IDCompraAlmacen
        {
            get { return _IDCompraAlmacen; }
            set { _IDCompraAlmacen = value; }
        }

        private CatSucursalesModels _Sucursal;
        /// <summary>
        /// Objeto que contiene la información de la sucursal que efectúo la compra
        /// </summary>
        public CatSucursalesModels Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }

        private CatProveedorModels _Proveedor;
        /// <summary>
        /// Objeto que contiene la información del proveedor 
        /// </summary>
        public CatProveedorModels Proveedor
        {
            get { return _Proveedor; }
            set { _Proveedor = value; }
        }
        
        private string _NumFacturaNota;
        /// <summary>
        /// Número de factura o nota de compra
        /// </summary>
        public string NumFacturaNota
        {
            get { return _NumFacturaNota; }
            set { _NumFacturaNota = value; }
        }
        
        private decimal _MontoTotal;
        /// <summary>
        /// Monto total de la compra. Monto equivalente al documento por pagar
        /// </summary>
        public decimal MontoTotal
        {
            get { return _MontoTotal; }
            set { _MontoTotal = value; }
        }
        //CultureInfo currentCulture = new CultureInfo("es-MX")
        /// <summary>
        /// Monto total de la compra en string con formato de moneda
        /// </summary>
        public string MontoTotalFormato
        {
            
            get { return string.Format(new CultureInfo("es-MX"), "{0:c}", _MontoTotal); }
        }
        
        private int _IDEstatusCompra;
        /// <summary>
        /// Identificador del estatus de la compra
        /// </summary>
        public int IDEstatusCompra
        {
            get { return _IDEstatusCompra; }
            set { _IDEstatusCompra = value; }
        }

        private string _statusCompra;

        public string StatusCompra
        {
            get { return _statusCompra; }
            set { _statusCompra = value; }
        }
        private DateTime _fecha;

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }


        private string _IDDocumentoXPagar;
        /// <summary>
        /// Identificador del documento por pagar generado a partir de la compra
        /// </summary>
        public string IDDocumentoXPagar
        {
            get { return _IDDocumentoXPagar; }
            set { _IDDocumentoXPagar = value; }
        }
        
        private List<CompraAlmacenModels> _ListaCompras;
        /// <summary>
        /// Lista de compras a dibujar en la tabla principal
        /// </summary>
        public List<CompraAlmacenModels> ListaCompras
        {
            get { return _ListaCompras; }
            set { _ListaCompras = value; }
        }

        private List<CatSucursalesModels> _ListaSucursales;
        /// <summary>
        /// Lista de sucursales para llenar combo Sucursales
        /// </summary>
        public List<CatSucursalesModels> ListaSucursales
        {
            get { return _ListaSucursales; }
            set { _ListaSucursales = value; }
        }

        private List<CatProveedorModels> _ListaProveedores;
        /// <summary>
        /// Lista de proveedores para llenar combo Proveedores
        /// </summary>
        public List<CatProveedorModels> ListaProveedores
        {
            get { return _ListaProveedores; }
            set { _ListaProveedores = value; }
        }

        private List<CompraAlmacenDetalleModels> _DetalleCompra;

        public List<CompraAlmacenDetalleModels> DetalleCompra
        {
            get { return _DetalleCompra; }
            set { _DetalleCompra = value; }
        }

        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion


        private List<CompraAlmacenModels> _Lista;

        public List<CompraAlmacenModels> Lista
        {
            get { return _Lista; }
            set { _Lista = value; }
        }

        private int _TotalRecords;

        public int TotalRecords
        {
            get { return _TotalRecords; }
            set { _TotalRecords = value; }
        }

        private int _SearchRecords;

        public int SearchRecords
        {
            get { return _SearchRecords; }
            set { _SearchRecords = value; }
        }




    }
}