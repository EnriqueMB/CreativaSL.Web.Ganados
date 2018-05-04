using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class UnidadesProductosAlmacenModels
    {
        public UnidadesProductosAlmacenModels()  {
            id_unidadProducto = string.Empty;
            id_unidad = 0;
            id_unidadPrincipal = 0;
            factor = 0;
            }
        private string _id_unidadProducto;

        public string id_unidadProducto
        {
            get { return _id_unidadProducto; }
            set { _id_unidadProducto = value; }
        }
        private string _id_producto;

        public string id_producto
        {
            get { return _id_producto; }
            set { _id_producto = value; }
        }
        private int _id_unidadPrincipal;

        public int id_unidadPrincipal
        {
            get { return _id_unidadPrincipal; }
            set { _id_unidadPrincipal = value; }
        }
        private int _id_unidad;

        public int id_unidad
        {
            get { return _id_unidad; }
            set { _id_unidad = value; }
        }

        private string _NombreUnidad;

        public string NombreUnidad
        {
            get { return _NombreUnidad; }
            set { _NombreUnidad = value; }
        }

        private decimal _factor;

        public decimal factor
        {
            get { return _factor; }
            set { _factor = value; }
        }
        private bool _principal;

        public bool principal
        {
            get { return _principal; }
            set { _principal = value; }
        }
        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        private List<UnidadesProductosAlmacenModels> _LUnidad;

        public List<UnidadesProductosAlmacenModels> LUnidad
        {
            get { return _LUnidad; }
            set { _LUnidad = value; }
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