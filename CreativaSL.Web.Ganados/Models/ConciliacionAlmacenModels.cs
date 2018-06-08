using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ConciliacionAlmacenModels
    {
        public ConciliacionAlmacenModels()
        {
            _IDConciliacionAlmacen = string.Empty;
            _Sucursal = new CatSucursalesModels();
            _Almacen = new CatAlmacenModels();
            _FechaConciliacion = DateTime.Today;
            _TipoConciliacion = new CatTipoConciliacionModels();
            _Comentario = string.Empty;
            _IDEstatus = 0;
            _Estatus = string.Empty;
            _CssClass = string.Empty;
            _ListaDetalle = new List<ConciliacionAlmacenDetalleModel>();

            NuevoRegistro = false;
            Conexion = string.Empty;
            Resultado = 0;
            Completado = false;
            Usuario = string.Empty;
            Opcion = 0;
        }

        private string _IDConciliacionAlmacen;
        public string IDConciliacionAlmacen
        {
            get { return _IDConciliacionAlmacen; }
            set { _IDConciliacionAlmacen = value; }
        }

        private CatSucursalesModels _Sucursal;
        public CatSucursalesModels Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }

        private CatAlmacenModels _Almacen;
        public CatAlmacenModels Almacen
        {
            get { return _Almacen; }
            set { _Almacen = value; }
        }
        
        private DateTime _FechaConciliacion;
        public DateTime FechaConciliacion
        {
            get { return _FechaConciliacion; }
            set { _FechaConciliacion = value; }
        }
        
        public string FechaConciliacionFormat
        {
            get { return _FechaConciliacion.ToShortDateString(); }
        }

        private string _FolioConciliacion;
        public string FolioConciliacion
        {
            get { return _FolioConciliacion; }
            set { _FolioConciliacion = value; }
        }

        private CatTipoConciliacionModels _TipoConciliacion;
        public CatTipoConciliacionModels TipoConciliacion
        {
            get { return _TipoConciliacion; }
            set { _TipoConciliacion = value; }
        }
        
        private string _Comentario;
        public string Comentario
        {
            get { return _Comentario; }
            set { _Comentario = value; }
        }

        private int _IDEstatus;
        public int IDEstatus
        {
            get { return _IDEstatus; }
            set { _IDEstatus = value; }
        }

        private string _Estatus;
        public string Estatus
        {
            get { return _Estatus; }
            set { _Estatus = value; }
        }

        private string _CssClass;
        public string CssClass
        {
            get { return _CssClass; }
            set { _CssClass = value; }
        }

        private List<ConciliacionAlmacenDetalleModel> _ListaDetalle;

        public List<ConciliacionAlmacenDetalleModel> ListaDetalle
        {
            get { return _ListaDetalle; }
            set { _ListaDetalle = value; }
        }


        #region Datos De Control
        public bool NuevoRegistro { get; set; }
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}