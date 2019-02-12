using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CalendarioModels
    {

        public CalendarioModels()
        {
            IDProveedor = string.Empty;
            listaCalendario = new List<CalendarioModels>();
            GuiaTransito = string.Empty;
            FechaHoraProgramada = DateTime.Now;
            Estatus = 0;
            estatusDesc = string.Empty;
            GanadosPactadoHembras = 0;
            GanadosPactadoMachos = 0;
            Conexion = string.Empty;
            Resultado = 0;
            Completado = false;
            Opcion = 0;
            Usuario = string.Empty;
            Letras = string.Empty;
        }

        public DateTime fechaStart { get; set; }
        public DateTime fechaEnd { get; set; }
        public string start { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        public string IDProveedor { get; set; }
        public List<CalendarioModels> listaCalendario { get; set; }
        public string GuiaTransito { get; set; }
        public DateTime FechaHoraProgramada { get; set; }
        public int Estatus { get; set; }
        public int GanadosPactadoMachos { get; set; }
        public int GanadosPactadoHembras { get; set; }
        public string estatusDesc { get; set; }

        public string AgenteCompra { get; set; }
        public string Lugar { get; set; }
        public string Sucursal { get; set; }
        public string HoraProgramada { get; set; }
        public string Bascula { get; set; }
        public string Nota { get; set; }
        public string Unidad { get; set; }
        public string Chofer { get; set; }

        public string Letras { get; set; }

        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion

    }
}