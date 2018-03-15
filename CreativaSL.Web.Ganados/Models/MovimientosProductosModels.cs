using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class MovimientosProductosModels
    {
        private string _id_movimientoAlmacen;

        public string id_movimientoAlmacen
        {
            get { return _id_movimientoAlmacen; }
            set { _id_movimientoAlmacen = value; }
        }
        private string _id_almacen;

        public string id_almacen
        {
            get { return _id_almacen; }
            set { _id_almacen = value; }
        }
        private string _id_producto;

        public string id_producto
        {
            get { return _id_producto; }
            set { _id_producto = value; }
        }
        private int _id_tipoMovimineto;

        public int id_tipoMovimiento
        {
            get { return _id_tipoMovimineto; }
            set { _id_tipoMovimineto = value; }
        }
        private decimal _cantidadInicial;

        public decimal cantidadInicial
        {
            get { return _cantidadInicial; }
            set { _cantidadInicial = value; }
        }
        private decimal _cantidadMovimiento;

        public decimal cantidadMovimiento
        {
            get { return _cantidadMovimiento; }
            set { _cantidadMovimiento = value; }
        }
        private decimal _cantidadFinal;

        public decimal cantidadFinal
        {
            get { return _cantidadFinal; }
            set { _cantidadFinal = value; }
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