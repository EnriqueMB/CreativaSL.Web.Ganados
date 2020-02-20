﻿using System.IO;
using System.Web.Hosting;

namespace CreativaSL.Web.Ganados.Models.System
{
    public static class ProjectSettings
    {
        public const int ModuloCliente = 1;
        public const int ModuloProveedor = 2;
        public const string BaseDirClienteDocumentacionExtra = "/Imagenes/Cliente/DocumentacionExtra/";
        public const string BaseDirProveedorDocumentacionExtra = "/Imagenes/Proveedor/DocumentacionExtra/";
        public const string BaseDirProveedorCuentasBancarias = "/Imagenes/Proveedor/CuentasBancarias/";
        public const string BaseDirProveedorFotoPerfil = "/Imagenes/Proveedor/FotoPerfil/";

        public const string PathDefaultImage = "/Content/img/GrupoOcampo.png";
    }
}