using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class TrayectoModels
    {
        private string _id_trayecto;

        public string id_trayecto
        {
            get { return _id_trayecto; }
            set { _id_trayecto = value; }
        }
        private string _id_flete;

        public string id_flete
        {
            get { return _id_flete; }
            set { _id_flete = value; }
        }
        private string _id_lugarOrigen;

        public string id_lugarOrigen
        {
            get { return _id_lugarOrigen; }
            set { _id_lugarOrigen = value; }
        }
        private string _id_lugarDestino;

        public string id_lugarDestino
        {
            get { return _id_lugarDestino; }
            set { _id_lugarDestino = value; }
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