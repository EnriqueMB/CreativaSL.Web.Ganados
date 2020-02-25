using System.IO;
using System.Web.Hosting;

namespace CreativaSL.Web.Ganados.Models.System
{
    public static class ProjectSettings
    {
        public const int ModuloCliente = 1;
        public const int ModuloProveedor = 2;
        public const string BaseDirClienteFotoPerfil = "/Imagenes/Cliente/FotoPerfil/";
        public const string BaseDirClienteDocumentacionExtra = "/Imagenes/Cliente/DocumentacionExtra/";
        public const string BaseDirClienteCuentasBancarias = "/Imagenes/Cliente/FotoCuentas/";
        public const string BaseDirProveedorDocumentacionExtra = "/Imagenes/Proveedor/DocumentacionExtra/";
        public const string BaseDirProveedorCuentasBancarias = "/Imagenes/Proveedor/CuentasBancarias/";
        public const string BaseDirProveedorFotoPerfil = "/Imagenes/Proveedor/FotoPerfil/";
        public const string BaseDirProveedorINE = "/Imagenes/Proveedor/INE/";
        public const string BaseDirProveedorManifestacionFierro = "/Imagenes/Proveedor/ManifestacionFierro/";
        public const string BaseDirProveedorUppPsg = "/Imagenes/Proveedor/UppPsg/";

        public const string PathDefaultImage = "/Content/img/GrupoOcampo.png";
    }
}