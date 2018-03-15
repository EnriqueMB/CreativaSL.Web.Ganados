using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class AlmacenEntradaModels
    {
        private string _IDEntradaAlmacen;

        public string IDEntradaAlmacen
        {
            get { return _IDEntradaAlmacen; }
            set { _IDEntradaAlmacen = value; }
        }

        private string _IDAlmacen;

        public string IDAlmacen
        {
            get { return _IDAlmacen; }
            set { _IDAlmacen = value; }
        }

        private string _FolioEntrada;

        public string FolioEntrada
        {
            get { return _FolioEntrada; }
            set { _FolioEntrada = value; }
        }

        private DateTime _FechaIngreso;

        public DateTime FechaIngreso
        {
            get { return _FechaIngreso; }
            set { _FechaIngreso = value; }
        }

        private string _Comentarios;

        public string Comentarios
        {
            get { return _Comentarios; }
            set { _Comentarios = value; }
        }

        private string _NumFacturaNota;

        public string NumFacturaNota
        {
            get { return _NumFacturaNota; }
            set { _NumFacturaNota = value; }
        }

        private float _MontoTotal;

        public float MontoTotal
        {
            get { return _MontoTotal; }
            set { _MontoTotal = value; }
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