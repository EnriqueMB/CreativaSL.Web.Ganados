﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models.Dto.Cliente
{
    public class ClienteGanadoDto
    {
        public string IdCliente { get; set; }
        public string Sucursal { get; set; }
        public string TipoProveedor { get; set; }
        public string RazonSocial_Nombre { get; set; }
        public string Rfc { get; set; }
        public string Direccion { get; set; }
        public string FechaIngreso { get; set; }
        public string Tolerancia { get; set; }
        public string FotoPerfilUrl { get; set; }
        public string Observacion { get; set; }
        public string Telefonos { get; set; }
        public string Email { get; set; }
        public string UppPsgBase64 { get; set; }
        public string IneBase64 { get; set; }
        public string ManifestacionFierroBase64 { get; set; }
    }
}