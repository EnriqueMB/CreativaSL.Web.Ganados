using CreativaSL.Web.Ganados.ViewModels;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _RptProveedorMunicipio_Datos
    {
        public List<RptProveedorMunicipioModels> obtenerListaProveedoresFecha(RptProveedorMunicipioModels Datos)
        {
            try
            {
                List<RptProveedorMunicipioModels> lista = new List<RptProveedorMunicipioModels>();
                RptProveedorMunicipioModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.conexion, "spCSLDB_Reporte_get_ProveedorMunicipio",Datos.fechaInicio,Datos.fechaFin,Datos.IDEstado,Datos.IDMunicipio);
                while (dr.Read())
                {
                    item = new RptProveedorMunicipioModels();
                    item.ganadoPactadoMacho = !dr.IsDBNull(dr.GetOrdinal("GanadoPactadoMachos")) ? dr.GetInt32(dr.GetOrdinal("GanadoPactadoMachos")) : 0;
                    item.ganadoPactadoHembra = !dr.IsDBNull(dr.GetOrdinal("GanadoPactadoHembras")) ? dr.GetInt32(dr.GetOrdinal("GanadoPactadoHembras")) : 0;
                    item.ganadoCompradoMacho = !dr.IsDBNull(dr.GetOrdinal("GanadoCompradoMacho")) ? dr.GetInt32(dr.GetOrdinal("GanadoCompradoMacho")) : 0;
                    item.ganadoCompradoHembra = !dr.IsDBNull(dr.GetOrdinal("GanadoCompradoHembra")) ? dr.GetInt32(dr.GetOrdinal("GanadoCompradoHembra")) : 0;
                    //item.fechaProgramada= !dr.IsDBNull(dr.GetOrdinal("FechaHoraProgamada")) ? dr.GetDateTime(dr.GetOrdinal("FechaHoraProgamada")) : DateTime.Now;
                    item.Estado = !dr.IsDBNull(dr.GetOrdinal("nombreEstado")) ? dr.GetString(dr.GetOrdinal("nombreEstado")) : string.Empty;
                    item.totalPactado = !dr.IsDBNull(dr.GetOrdinal("GanadoPactatoTotal")) ? dr.GetInt32(dr.GetOrdinal("GanadoPactatoTotal")) : 0;
                    item.totalComprado = !dr.IsDBNull(dr.GetOrdinal("GanadoCompradoTotal")) ? dr.GetInt32(dr.GetOrdinal("GanadoCompradoTotal")) : 0;
                    item.municipio = !dr.IsDBNull(dr.GetOrdinal("NombreMunicipio")) ? dr.GetString(dr.GetOrdinal("NombreMunicipio")) : string.Empty;
                    lista.Add(item);
                }
                dr.Close();
                Datos.listaProveedor = lista;
                return Datos.listaProveedor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DatosEmpresaViewModels ObtenerDatosEmpresaTipo1(RptProveedorMunicipioModels Datos)
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
                Dr.Close();
                return Datos.DatosEmpresa;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatEstadoModels> obtenerListaEstados(RptProveedorMunicipioModels Datos)
        {
            try
            {
                List<CatEstadoModels> lista = new List<CatEstadoModels>();
                CatEstadoModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.conexion, "spCSLDB_get_ComboEstado","Mex");
                while (dr.Read())
                {
                    item = new CatEstadoModels();
                    item.id_estado = Convert.ToInt32(dr["id_estado"].ToString());
                    item.codigoEstado = dr["clave"].ToString();
                    item.descripcion = dr["descripcion"].ToString();

                    lista.Add(item);
                }
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<CatMunicipioModels> obtenerListaMunicipios(RptProveedorMunicipioModels Datos)
        {
            try
            {
                List<CatMunicipioModels> lista = new List<CatMunicipioModels>();
                CatMunicipioModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.conexion, "spCSLDB_get_ComboMunicipio2", "Mex", Datos.IDEstado,1);
                while (dr.Read())
                {
                    item = new CatMunicipioModels();
                    item.id_municipio = Convert.ToInt32(dr["id_municipio"].ToString());
                    item.codigoMunicipio = dr["clave"].ToString();
                    item.descripcion = dr["descripcion"].ToString();

                    lista.Add(item);
                }
                dr.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}