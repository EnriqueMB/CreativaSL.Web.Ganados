using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class AsistenciaEmpleadoModels
    {
        public AsistenciaEmpleadoModels() {
            _fecha = DateTime.Now;
            _listaEmpleados = new List<CatEmpleadoModels>();

            //Datos de control
            activo = false;
            user = string.Empty;
            conexion = string.Empty;
            Completado = false;
            opcion = 0;
            resultado = string.Empty;
        }
        private string _IDFalta;

        public string IDFalta
        {
            get { return _IDFalta; }
            set { _IDFalta = value; }
        }

        private DateTime _fecha;
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        private List<CatEmpleadoModels> _listaEmpleados;

        public List<CatEmpleadoModels> listaEmpleados
        {
            get { return _listaEmpleados; }
            set { _listaEmpleados = value; }
        }
        private DataTable  _tablaAsistencia;

        public DataTable tablaAsistencia
        {
            get { return _tablaAsistencia; }
            set { _tablaAsistencia = value; }
        }

        #region Datos de control
        public bool activo { get; set; }
        public string user { get; set; }
        public string conexion { get; set; }
        public string resultado { get; set; }
        public int Result { get; set; }
        public int opcion { get; set; }
        public bool Completado { get; set; }
        #endregion
    }
}