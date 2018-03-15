using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class EntradaAlmacenModels
    {
        private string _IDEntradaAlmacen;

        public string IDEntradaAlmacen
        {
            get { return _IDEntradaAlmacen; }
            set { _IDEntradaAlmacen = value; }
        }

        private string _IDCompraAlmacen;

        public string IDCompraAlmacen
        {
            get { return _IDCompraAlmacen; }
            set { _IDCompraAlmacen = value; }
        }

        private string _IDAlmacen;

        public string IDAlmacen
        {
            get { return _IDAlmacen; }
            set { _IDAlmacen = value; }
        }

        private DateTime _FechaEntrada;

        public DateTime FechaEntrada
        {
            get { return _FechaEntrada; }
            set { _FechaEntrada = value; }
        }

        private string _FolioEntrega;

        public string FolioEntrega
        {
            get { return _FolioEntrega; }
            set { _FolioEntrega = value; }
        }

        private string _Comentario;

        public string Comentario
        {
            get { return _Comentario; }
            set { _Comentario = value; }
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