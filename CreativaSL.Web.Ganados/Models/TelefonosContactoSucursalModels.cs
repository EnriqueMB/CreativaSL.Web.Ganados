using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class TelefonosContactoSucursalModels
    {
        private string _id_contacto;

        public string id_contacto
        {
            get { return _id_contacto; }
            set { _id_contacto = value; }
        }
        private string _id_sucursal;

        public string id_sucursal
        {
            get { return _id_sucursal; }
            set { _id_sucursal = value; }
        }
        private string _telefono;

        public string telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
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