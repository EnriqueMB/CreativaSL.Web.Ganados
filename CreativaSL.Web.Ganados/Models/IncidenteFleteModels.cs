using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class IncidenteFleteModels
    {
        private string _id_incidenteFlete;

        public string id_incidenteFlete
        {
            get { return _id_incidenteFlete; }
            set { _id_incidenteFlete = value; }
        }
        private string _id_flete;

        public string id_flete
        {
            get { return _id_flete; }
            set { _id_flete = value; }
        }
        private string _id_documentoXPagar;

        public string id_documentoXPagar
        {
            get { return _id_documentoXPagar; }
            set { _id_documentoXPagar = value; }
        }
        private string _descripcion;

        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        private string _costo;

        public string costo
        {
            get { return _costo; }
            set { _costo = value; }
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