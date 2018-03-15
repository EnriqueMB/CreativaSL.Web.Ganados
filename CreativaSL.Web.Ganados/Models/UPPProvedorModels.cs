using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class UPPProvedorModels
    {
        private string _id_proveedor;

        public string id_proveedor
        {
            get { return _id_proveedor; }
            set { _id_proveedor = value; }
        }
        private string _UPP;

        public string UPP
        {
            get { return _UPP; }
            set { _UPP = value; }
        }
        private DateTime _fechaAlta;

        public DateTime fechaAlta
        {
            get { return _fechaAlta; }
            set { _fechaAlta = value; }
        }
        private int _id_pais;

        public int id_pais
        {
            get { return _id_pais; }
            set { _id_pais = value; }
        }
        private int _id_estado;

        public int id_estado
        {
            get { return _id_estado; }
            set { _id_estado = value; }
        }
        private int _id_municipio;

        public int id_municipio
        {
            get { return _id_municipio; }
            set { _id_municipio = value; }
        }
        private string _nombrePredio;
                
        public string nombrePredio
        {
            get { return _nombrePredio; }
            set { _nombrePredio = value; }
        }
        private string _propietario;

        public string propietario
        {
            get { return _propietario; }
            set { _propietario = value; }
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