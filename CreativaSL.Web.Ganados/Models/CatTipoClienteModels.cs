using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatTipoClienteModels
    {
        public CatTipoClienteModels()
        {
            _id_tipoCliente = 0;
            _descripcion = string.Empty;
            _listaTIpoCliente = new List<CatTipoClienteModels>();
            Conexion = string.Empty;

        }
        private int _id_tipoCliente;

        public int id_tipoCliente
        {
            get { return _id_tipoCliente; }
            set { _id_tipoCliente = value; }
        }
        private string _id_sucursal;

        private string _descripcion;

        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        private List<CatTipoClienteModels> _listaTIpoCliente;

        public List<CatTipoClienteModels> listaTipoCliente
        {
            get { return _listaTIpoCliente; }
            set { _listaTIpoCliente = value; }
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