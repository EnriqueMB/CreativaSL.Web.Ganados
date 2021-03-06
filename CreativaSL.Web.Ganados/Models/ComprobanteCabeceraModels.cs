﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ComprobanteCabeceraModels
    {
        public string LogoEmpresa { get; set; }
        public string NombreEmpresa { get; set; }
        public string RubroEmpresa { get; set; }
        public string TelefonoEmpresa { get; set; }
        public string DireccionEmpresa { get; set; }
        public string Folio { get; set; }
        public string NombreCliente { get; set; }
        public string TelefonoCliente { get; set; }
        public string RFCCliente { get; set; }
        public string DiaImpresion { get; set; }
        public string MesImpresion { get; set; }
        public string AnnoImpresion { get; set; }

        #region Pesaje ganado
        public decimal CostoPesajeGanado { get; set; }
        public decimal KilosPesajeGanado { get; set; }
        #endregion
    }
}