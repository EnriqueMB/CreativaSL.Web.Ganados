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
                object[] parametros = { Datos.FechaInicio, Datos.FechaFin, Datos.IDSucursalActual };
                List<RptProveedorMermaAltaModels> lista = new List<RptProveedorMermaAltaModels>();
                RptProveedorMermaAltaModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Reporte_get_ProveedoresConMerma", parametros);
                while (dr.Read())
                {
                    item = new RptProveedorMermaAltaModels();
                    item.IDProveedor= !dr.IsDBNull(dr.GetOrdinal("id_proveedor")) ? dr.GetString(dr.GetOrdinal("id_proveedor")) : string.Empty;
                    item.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NombreSucursal")) ? dr.GetString(dr.GetOrdinal("NombreSucursal")) : string.Empty;
                    item.nombreProveedor = !dr.IsDBNull(dr.GetOrdinal("nombreRazonSocial")) ? dr.GetString(dr.GetOrdinal("nombreRazonSocial")) : string.Empty;
                    item.NombreCompra = !dr.IsDBNull(dr.GetOrdinal("Compra")) ? dr.GetString(dr.GetOrdinal("Compra")) : string.Empty;
                    item.merma = !dr.IsDBNull(dr.GetOrdinal("toleranciaProveedor")) ? dr.GetInt32(dr.GetOrdinal("toleranciaProveedor")) : 0;
                    item.toleranciaCompra = !dr.IsDBNull(dr.GetOrdinal("toleranciaCompra")) ? dr.GetDecimal(dr.GetOrdinal("toleranciaCompra")) : 0;
                    item.mermaTotal = !dr.IsDBNull(dr.GetOrdinal("mermaTotal")) ? dr.GetDecimal(dr.GetOrdinal("mermaTotal")) : 0;
                    item.GanadoMacho = !dr.IsDBNull(dr.GetOrdinal("GanadoCompradoMacho")) ? dr.GetInt32(dr.GetOrdinal("GanadoCompradoMacho")) : 0;
                    item.GanadoHembra = !dr.IsDBNull(dr.GetOrdinal("GanadoCompradoHembras")) ? dr.GetInt32(dr.GetOrdinal("GanadoCompradoHembras")) : 0;
                    item.TalGanadoComprado = !dr.IsDBNull(dr.GetOrdinal("TotalGanado")) ? dr.GetInt32(dr.GetOrdinal("TotalGanado")) : 0;
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
        public DatosEmpresaViewModels ObtenerDatosEmpresaTipo1(string Cadena)
        {
            try
            {
                DatosEmpresaViewModels Datos = new DatosEmpresaViewModels();
                SqlDataReader Dr = SqlHelper.ExecuteReader(Cadena, "spCSLDB_EMPRESA_get_CatEmpresasIDTIPO1");
                while (Dr.Read())
                {
                    Datos.LogoEmpresa = !Dr.IsDBNull(Dr.GetOrdinal("LogoEmpresa")) ? Dr.GetString(Dr.GetOrdinal("LogoEmpresa")) : string.Empty;
                    Datos.RazonFiscal = !Dr.IsDBNull(Dr.GetOrdinal("RazonFiscal")) ? Dr.GetString(Dr.GetOrdinal("RazonFiscal")) : string.Empty;
                    Datos.DireccionFiscal = !Dr.IsDBNull(Dr.GetOrdinal("DireccionFiscal")) ? Dr.GetString(Dr.GetOrdinal("DireccionFiscal")) : string.Empty;
                    Datos.RFC = !Dr.IsDBNull(Dr.GetOrdinal("RFC")) ? Dr.GetString(Dr.GetOrdinal("RFC")) : string.Empty;
                    Datos.Representante = !Dr.IsDBNull(Dr.GetOrdinal("Representante")) ? Dr.GetString(Dr.GetOrdinal("Representante")) : string.Empty;
                    Datos.NumTelefonico1 = !Dr.IsDBNull(Dr.GetOrdinal("NumTelefono1")) ? Dr.GetString(Dr.GetOrdinal("NumTelefono1")) : string.Empty;
                    Datos.NumTelefonico2 = !Dr.IsDBNull(Dr.GetOrdinal("NumTelefono2")) ? Dr.GetString(Dr.GetOrdinal("NumTelefono2")) : string.Empty;
                    Datos.Email = !Dr.IsDBNull(Dr.GetOrdinal("Correo")) ? Dr.GetString(Dr.GetOrdinal("Correo")) : string.Empty;
                    Datos.HorarioAtencion = !Dr.IsDBNull(Dr.GetOrdinal("HorarioAtencion")) ? Dr.GetString(Dr.GetOrdinal("HorarioAtencion")) : string.Empty;
                    Datos.NombreSucursal = !Dr.IsDBNull(Dr.GetOrdinal("NombreSucursal")) ? Dr.GetString(Dr.GetOrdinal("NombreSucursal")) : string.Empty;
                    //Bitmap bmpFromString = Datos.DatosEmpresa.LogoEmpresa.Base64StringToBitmap();
                    //Datos.DatosEmpresa.ImagenContruida = bmpFromString.ToBase64ImageReport(ImageFormat.Png);
                }
                Dr.Close();
                return Datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}