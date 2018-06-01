using CreativaSL.Web.Ganados.ViewModels;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _RptSalidas_Datos
    {
        public List<RptSalidasModels> obtenerListaProveedoresFecha(RptSalidasModels Datos)
        {
            try
            {
                List<RptSalidasModels> lista = new List<RptSalidasModels>();
                RptSalidasModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.conexion, "spCSLDB_Reporte_get_SalidasDocXpagar", Datos.fechaInicio, Datos.fechaFin);
                while (dr.Read())
                {
                    item = new RptSalidasModels();
                    
                    item.concepto = !dr.IsDBNull(dr.GetOrdinal("id_conceptoDocumento")) ? dr.GetInt32(dr.GetOrdinal("id_conceptoDocumento")) : 0;
                   
                   
                    item.fecha = !dr.IsDBNull(dr.GetOrdinal("fecha")) ? dr.GetDateTime(dr.GetOrdinal("fecha")) : DateTime.Now;
                    item.subtotal = !dr.IsDBNull(dr.GetOrdinal("SubTotal")) ? dr.GetDecimal(dr.GetOrdinal("SubTotal")) : 0;
                    
                    lista.Add(item);
                }


                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RptSalidasModels obtenerListaProveedoresFecha2(RptSalidasModels Datos)
        {
            try
            {
                DataSet ds = null;
                ds = SqlHelper.ExecuteDataset(Datos.conexion, "spCSLDB_Reporte_get_SalidasDocXpagar2", Datos.fechaInicio, Datos.fechaFin);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0] != null)
                        {
                            Datos.TablaDatos = ds.Tables[0];

                            DataTableReader tabla = ds.Tables[1].CreateDataReader();
                            while (tabla.Read())
                            {
                                Datos.Descripcion= !tabla.IsDBNull(tabla.GetOrdinal("tipoclasificacion")) ? tabla.GetString(tabla.GetOrdinal("tipoclasificacion")) : string.Empty;
                                break;
                            }
                        }
                      
                    }
                }
                return Datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DatosEmpresaViewModels ObtenerDatosEmpresaTipo1(RptSalidasModels Datos)
        {
            try
            {

                SqlDataReader Dr = SqlHelper.ExecuteReader(Datos.conexion, "spCSLDB_EMPRESA_get_CatEmpresasIDTIPO1");
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
                    Bitmap bmpFromString = Datos.DatosEmpresa.LogoEmpresa.Base64StringToBitmap();
                    Datos.DatosEmpresa.ImagenContruida = bmpFromString.ToBase64ImageReport(ImageFormat.Png);
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