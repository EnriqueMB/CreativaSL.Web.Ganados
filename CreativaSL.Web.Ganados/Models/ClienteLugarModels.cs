using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ClienteLugarModels
    {
        private string _IDCliente;

        public string IDCliente
        {
            get { return _IDCliente; }
            set { _IDCliente = value; }
        }

        private string _IDClienteLugar;

        public string IDClienteLugar
        {
            get { return _IDClienteLugar; }
            set { _IDClienteLugar = value; }
        }

        private string _IDLugar;

        public string IDLugar
        {
            get { return _IDLugar; }
            set { _IDLugar = value; }
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