using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class NominaDetalleModels
    {
        private string _id_nomina;

        public string id_nomina
        {
            get { return _id_nomina; }
            set { _id_nomina = value; }
        }
        private string _id_empleado;

        public string id_empleado
        {
            get { return _id_empleado; }
            set { _id_empleado = value; }
        }
        private decimal _percepciones;

        public decimal percepciones
        {
            get { return _percepciones; }
            set { _percepciones = value; }
        }
        private decimal _deducciones;

        public decimal deducciones
        {
            get { return _deducciones; }
            set { _deducciones = value; }
        }
        private decimal _total;

        public decimal total
        {
            get { return _total; }
            set { _total = value; }
        }
        private bool _pagado;

        public bool pagado
        {
            get { return _pagado; }
            set { _pagado = value; }
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