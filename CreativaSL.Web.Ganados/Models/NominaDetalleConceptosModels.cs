using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class NominaDetalleConceptosModels
    {
        private string _id_nominaConceptos;

        public string id_nominaConceptos
        {
            get { return _id_nominaConceptos; }
            set { _id_nominaConceptos = value; }
        }
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
        private int _id_concepto;

        public int id_concepto
        {
            get { return _id_concepto; }
            set { _id_concepto = value; }
        }
        private decimal _monto;

        public decimal monto
        {
            get { return _monto; }
            set { _monto = value; }
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