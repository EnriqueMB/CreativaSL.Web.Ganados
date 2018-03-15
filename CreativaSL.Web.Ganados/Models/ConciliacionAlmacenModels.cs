using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ConciliacionAlmacenModels
    {
        private string _IDConciliacionAlmacen;

        public string IDConciliacionAlmacen
        {
            get { return _IDConciliacionAlmacen; }
            set { _IDConciliacionAlmacen = value; }
        }

        private string _IDAlmacen;

        public string IDAlmacen
        {
            get { return _IDAlmacen; }
            set { _IDAlmacen = value; }
        }

        private DateTime _FechaConciliacion;

        public DateTime FechaConciliacion
        {
            get { return _FechaConciliacion; }
            set { _FechaConciliacion = value; }
        }

        private string _FolioConciliacion;

        public string FolicoConciliacion
        {
            get { return _FolioConciliacion; }
            set { _FolioConciliacion = value; }
        }

        private string _IDTipoConciliacion;

        public string IDTipoConciliacion
        {
            get { return _IDTipoConciliacion; }
            set { _IDTipoConciliacion = value; }
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