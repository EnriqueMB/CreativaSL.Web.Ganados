using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class SalidaAlmacenDetalleModels
    {
        public SalidaAlmacenDetalleModels()
        {
            _IDSalida = string.Empty;
            _IDSalidaDetalle = string.Empty;
            _Producto = new CatProductosAlmacenModels();
            _UnidadMedida = new UnidadesProductosAlmacenModels();
            _Cantidad = 0;
            NuevoRegistro = false;
            Conexion = string.Empty;
            Resultado = 0;
            Usuario = string.Empty;
            Opcion = 0;
        }
        
        private string _IDSalidaDetalle;
        public string IDSalidaDetalle
        {
            get { return _IDSalidaDetalle; }
            set { _IDSalidaDetalle = value; }
        }

        private string _IDSalida;
        public string IDSalida
        {
            get { return _IDSalida; }
            set { _IDSalida = value; }
        }

        private CatProductosAlmacenModels _Producto;
        public CatProductosAlmacenModels Producto
        {
            get { return _Producto; }
            set { _Producto = value; }
        }

        private UnidadesProductosAlmacenModels _UnidadMedida;
        public UnidadesProductosAlmacenModels UnidadMedida
        {
            get { return _UnidadMedida; }
            set { _UnidadMedida = value; }
        }

        private decimal _Cantidad;
        public decimal Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }


        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        public bool NuevoRegistro { get; set; }

        #endregion
    }
}