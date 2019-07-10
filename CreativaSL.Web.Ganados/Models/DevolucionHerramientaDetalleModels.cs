using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class DevolucionHerramientaDetalleModels
    {
        public DevolucionHerramientaDetalleModels()
        {
            _IDPrestamoDetalle = 0;
            _IDPrestamo = 0;
            _estatus = 0;
            _Producto = new CatProductosAlmacenModels();
            _UnidadMedida = new UnidadesProductosAlmacenModels();
            _Cantidad = 0;
            _CantDevolver = 0;
            NuevoRegistro = false;
            Conexion = string.Empty;
            Resultado = 0;
            Usuario = string.Empty;
            Opcion = 0;
        }
        private int _IDPrestamoDetalle;

        public int IDPrestamoDetalle
        {
            get { return _IDPrestamoDetalle; }
            set { _IDPrestamoDetalle = value; }
        }

        private int _IDPrestamo;

        public int IDPrestamo
        {
            get { return _IDPrestamo; }
            set { _IDPrestamo = value; }
        }

        private int _estatus;

        public int Estatus
        {
            get { return _estatus; }
            set { _estatus = value; }
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
        private decimal _CantidadPendiente;

        public decimal CantidadPendiente
        {
            get { return _CantidadPendiente; }
            set { _CantidadPendiente = value; }
        }
        private decimal _CantDevolver;

        public decimal CantDevolver
        {
            get { return _CantDevolver; }
            set { _CantDevolver = value; }
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