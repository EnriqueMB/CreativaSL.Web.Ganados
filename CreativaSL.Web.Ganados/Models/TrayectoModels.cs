using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class TrayectoModels
    {
        public TrayectoModels()
        {
            LugarOrigen = new CatLugarModels();
            LugarDestino = new CatLugarModels();
            Remitente = new CatClienteModels();
            Destinatario = new CatClienteModels();
     }

        //No tocar
        public string id_trayecto { get; set; }
        public string id_flete { get; set; }
        public string id_lugarOrigen { get; set; }
        public string id_lugarDestino { get; set; }

        //ID
        public CatLugarModels LugarOrigen { get; set; }
        public CatLugarModels LugarDestino { get; set; }
        public CatClienteModels Remitente { get; set; }
        public CatClienteModels Destinatario { get; set; }

        //Select
        public List<CatLugarModels> ListaLugarOrigen { get; set; }
        public List<CatLugarModels> ListaLugarDestino { get; set; }
        public List<CatClienteModels> ListaRemitente { get; set; }
        public List<CatClienteModels> ListaDestinatario { get; set; }

        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion
    }
}