using CreativaSL.Web.Ganados.ViewModels;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _RptProveedorMermaAlta_Datos
    {
        public List<RptProveedorMermaAltaModels> obtenerListaProveedoresMermaAlta(RptProveedorMermaAltaModels Datos) {
            try
            {
                List<RptProveedorMermaAltaModels> lista = new List<RptProveedorMermaAltaModels>();
                RptProveedorMermaAltaModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Reporte_get_ProveedoresConMerma");
                while (dr.Read())
                {
                    item = new RptProveedorMermaAltaModels();
                    item.IDProveedor= !dr.IsDBNull(dr.GetOrdinal("nombreRazonSocial")) ? dr.GetString(dr.GetOrdinal("nombreRazonSocial")) : string.Empty;
                    item.nombreProveedor = !dr.IsDBNull(dr.GetOrdinal("nombreRazonSocial")) ? dr.GetString(dr.GetOrdinal("nombreRazonSocial")) : string.Empty;
                    item.toleranciaCompra = !dr.IsDBNull(dr.GetOrdinal("toleranciaCompra")) ? dr.GetDecimal(dr.GetOrdinal("toleranciaCompra")) : 0;
                    item.merma = !dr.IsDBNull(dr.GetOrdinal("toleranciaProveedor")) ? dr.GetInt32(dr.GetOrdinal("toleranciaProveedor")) : 0;
                    item.mermaTotal = !dr.IsDBNull(dr.GetOrdinal("mermaTotal")) ? dr.GetDecimal(dr.GetOrdinal("mermaTotal")) : 0;
                    lista.Add(item);
                }
                Datos.listaRptProveedorMerma = lista;
                Datos.listaProveedores = lista;
                return Datos.listaRptProveedorMerma;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public DatosEmpresaViewModels ObtenerDatosEmpresaTipo1(RptProveedorMermaAltaModels Datos)
        {
            try
            {

                SqlDataReader Dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_EMPRESA_get_CatEmpresasIDTIPO1");
                while (Dr.Read())
                {
                    Datos.DatosEmpresa.LogoEmpresa = !Dr.IsDBNull(Dr.GetOrdinal("LogoEmpresa")) ? Dr.GetString(Dr.GetOrdinal("LogoEmpresa")) : string.Empty;
                    Datos.DatosEmpresa.RazonFiscal = !Dr.IsDBNull(Dr.GetOrdinal("RazonFiscal")) ? Dr.GetString(Dr.GetOrdinal("RazonFiscal")) : string.Empty;
                    Datos.DatosEmpresa.DireccionFiscal = !Dr.IsDBNull(Dr.GetOrdinal("DireccionFiscal")) ? Dr.GetString(Dr.GetOrdinal("DireccionFiscal")) : string.Empty;
                    Datos.DatosEmpresa.RFC = !Dr.IsDBNull(Dr.GetOrdinal("RFC")) ? Dr.GetString(Dr.GetOrdinal("RFC")) : string.Empty;
                    Datos.DatosEmpresa.Representante = !Dr.IsDBNull(Dr.GetOrdinal("Representante")) ? Dr.GetString(Dr.GetOrdinal("Representante")) : string.Empty;
                    Datos.DatosEmpresa.NumTelefonico1 = !Dr.IsDBNull(Dr.GetOrdinal("NumTelefono1")) ? Dr.GetString(Dr.GetOrdinal("NumTelefono1")) : string.Empty;
                    Datos.DatosEmpresa.NumTelefonico2 = !Dr.IsDBNull(Dr.GetOrdinal("NumTelefono2")) ? Dr.GetString(Dr.GetOrdinal("NumTelefono2")) : string.Empty;
                    Datos.DatosEmpresa.Email = !Dr.IsDBNull(Dr.GetOrdinal("Correo")) ? Dr.GetString(Dr.GetOrdinal("Correo")) : string.Empty;
                    Datos.DatosEmpresa.HorarioAtencion = !Dr.IsDBNull(Dr.GetOrdinal("HorarioAtencion")) ? Dr.GetString(Dr.GetOrdinal("HorarioAtencion")) : string.Empty;
                    Datos.DatosEmpresa.NombreSucursal = !Dr.IsDBNull(Dr.GetOrdinal("NombreSucursal")) ? Dr.GetString(Dr.GetOrdinal("NombreSucursal")) : string.Empty;
                    //Bitmap bmpFromString = Datos.DatosEmpresa.LogoEmpresa.Base64StringToBitmap();
                    //Datos.DatosEmpresa.ImagenContruida = bmpFromString.ToBase64ImageReport(ImageFormat.Png);
                }
                return Datos.DatosEmpresa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}