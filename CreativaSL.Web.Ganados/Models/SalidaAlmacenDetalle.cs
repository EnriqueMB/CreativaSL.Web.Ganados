using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class SalidaAlmacenDetalle
    {
        private string _id_salidaAlmacen;

        public string id_salidaAlmacen
        {
            get { return _id_salidaAlmacen; }
            set { _id_salidaAlmacen = value; }
        }
        private string _id_almacen;

        public string id_almacen
        {
            get { return _id_almacen; }
            set { _id_almacen = value; }
        }
        private string _id_empleado;

        public string id_empleado
        {
            get { return _id_empleado; }
            set { _id_empleado = value; }
        }
        private DateTime _fechaSalida;

        public DateTime fechaSalida
        {
            get { return _fechaSalida; }
            set { _fechaSalida = value; }
        }
        private string _folioSalida;

        public string folioSalida
        {
            get { return _folioSalida; }
            set { _folioSalida = value; }
        }
        private string _comentarios;

        public string comentarios
        {
            get { return _comentarios; }
            set { _comentarios = value; }
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