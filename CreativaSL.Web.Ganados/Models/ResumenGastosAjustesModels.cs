using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ResumenGastosAjustesModels
    {
        private string _id_ajuste;

        public string id_ajuste
        {
            get { return _id_ajuste; }
            set { _id_ajuste = value; }
        }
        private string _id_sucursal;

        public string id_sucursal
        {
            get { return _id_sucursal; }
            set { _id_sucursal = value; }
        }
        private int _id_tipoClasificacionGasto;

        public int id_tipoClasificacionGasto
        {
            get { return _id_tipoClasificacionGasto; }
            set { _id_tipoClasificacionGasto = value; }
        }
        private DateTime _fecha;

        public DateTime fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        private decimal _total;

        public decimal total
        {
            get { return _total; }
            set { _total = value; }
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