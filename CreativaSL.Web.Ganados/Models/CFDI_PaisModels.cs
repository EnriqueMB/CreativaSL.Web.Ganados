using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CFDI_PaisModels
    {
        public CFDI_PaisModels()
        {
            _Clave = string.Empty;
            _Descripcion = string.Empty;
            _FormatoCodigoPostal = string.Empty;
            _ValidadcionRegistroIdenTributaria = string.Empty;
            _Agrupaciones = string.Empty;
            _ListaPais = new List<CFDI_PaisModels>();
            Conexion = string.Empty;
            Usuario = string.Empty;
        }
        private string _Clave;

        public string Clave
        {
            get { return _Clave; }
            set { _Clave = value; }
        }

        private string _Descripcion;

        public string Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        private string _FormatoCodigoPostal;

        public string FormatoCodigoPostal
        {
            get { return _FormatoCodigoPostal; }
            set { _FormatoCodigoPostal = value; }
        }

        private string _ValidadcionRegistroIdenTributaria;

        public string ValidacionRegistriIdenTributaria
        {
            get { return _ValidadcionRegistroIdenTributaria; }
            set { _ValidadcionRegistroIdenTributaria = value; }
        }

        private string _Agrupaciones;

        public string Agrupaciones
        {
            get { return _Agrupaciones; }
            set { _Agrupaciones = value; }
        }

        private List<CFDI_PaisModels> _ListaPais;

        public List<CFDI_PaisModels> ListaPais
        {
            get { return _ListaPais; }
            set { _ListaPais = value; }
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