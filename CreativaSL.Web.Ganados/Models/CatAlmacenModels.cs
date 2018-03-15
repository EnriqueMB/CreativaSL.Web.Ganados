using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatAlmacenModels
    {

        /// <summary>
        /// Inicializar los datos del modelo
        /// </summary>
        public CatAlmacenModels()
        {
            this._IDAlmacen = string.Empty;
            this._ClaveAlmacen = string.Empty;
            //Se da por hecho que CatSucursalModels tiene un constructor que inicializa sus datos
            this._Sucursal = new CatSucursalesModels();
            this._Descripcion = string.Empty;
            this._ListaSucursales = new List<CatSucursalesModels>();
            this._ListaAlmacen = new List<CatAlmacenModels>();
            this.Conexion = string.Empty;
            this.Resultado = 0;
            this.Completado = false;
            this.Usuario = string.Empty;
            this.Opcion = 0;
        }


        
        private string _IDAlmacen;
        /// <summary>
        /// Identificador primario del almacén
        /// </summary>
        public string IDAlmacen
        {
            get { return _IDAlmacen; }
            set { _IDAlmacen = value; }
        }

        
        private string _ClaveAlmacen;
        /// <summary>
        /// Identificador interno del almacén
        /// </summary>
        public string ClaveAlmacen
        {
            get { return _ClaveAlmacen; }
            set { _ClaveAlmacen = value; }
        }


        private CatSucursalesModels _Sucursal;
        /// <summary>
        /// Objeto que contiene la información de la sucursal a la que pertenece la Bodega o almacén
        /// </summary>
        public CatSucursalesModels Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }

        /// <summary>
        /// Descripción del almacen 
        /// </summary>
        private string _Descripcion;
        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        /// <summary>
        /// Lista de sucursales para llenar combo Sucursales
        /// </summary>
        private List<CatSucursalesModels> _ListaSucursales;
        public List<CatSucursalesModels> ListaSucursales
        {
            get { return _ListaSucursales; }
            set { _ListaSucursales = value; }
        }

        /// <summary>
        /// Lista de almacenes a dibujar en la tabla principal
        /// </summary>
        private List<CatAlmacenModels> _ListaAlmacen;
        public List<CatAlmacenModels> ListaAlmacen
        {
            get { return _ListaAlmacen; }
            set { _ListaAlmacen = value; }
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