using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class _Comprobante_Datos
    {
        public ComprobanteCabeceraModels Comprobante_spCSLDB_get_Cabecera(int modulo, string id, string conexion)
        {
            try
            {
                ComprobanteCabeceraModels Item = new ComprobanteCabeceraModels();
                object[] parametros = { modulo, id };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "comprobante.spCSLDB_get_Cabecera", parametros);

                while (dr.Read())
                {
                    Item.LogoEmpresa = !dr.IsDBNull(dr.GetOrdinal("logoEmpresa")) ? dr.GetString(dr.GetOrdinal("logoEmpresa")) : string.Empty;
                    Item.NombreEmpresa = !dr.IsDBNull(dr.GetOrdinal("nombreEmpresa")) ? dr.GetString(dr.GetOrdinal("nombreEmpresa")) : string.Empty;
                    Item.RubroEmpresa = !dr.IsDBNull(dr.GetOrdinal("rubroEmpresa")) ? dr.GetString(dr.GetOrdinal("rubroEmpresa")) : string.Empty;
                    Item.TelefonoEmpresa = !dr.IsDBNull(dr.GetOrdinal("telefonoEmpresa")) ? dr.GetString(dr.GetOrdinal("telefonoEmpresa")) : string.Empty;
                    Item.DireccionEmpresa = !dr.IsDBNull(dr.GetOrdinal("direccionEmpresa")) ? dr.GetString(dr.GetOrdinal("direccionEmpresa")) : string.Empty;
                    Item.AnnoImpresion = !dr.IsDBNull(dr.GetOrdinal("annoComprobante")) ? dr.GetString(dr.GetOrdinal("annoComprobante")) : string.Empty;
                    Item.MesImpresion = !dr.IsDBNull(dr.GetOrdinal("mesComprobante")) ? dr.GetString(dr.GetOrdinal("mesComprobante")) : string.Empty;
                    Item.DiaImpresion = !dr.IsDBNull(dr.GetOrdinal("diaComprobante")) ? dr.GetString(dr.GetOrdinal("diaComprobante")) : string.Empty;
                    Item.Folio = !dr.IsDBNull(dr.GetOrdinal("folio")) ? dr.GetString(dr.GetOrdinal("folio")) : string.Empty;

                    if (modulo == 1) //pesajeGanado
                    {
                        Item.NombreCliente = !dr.IsDBNull(dr.GetOrdinal("nombreCliente")) ? dr.GetString(dr.GetOrdinal("nombreCliente")) : string.Empty;
                        Item.RFCCliente = !dr.IsDBNull(dr.GetOrdinal("rfcCliente")) ? dr.GetString(dr.GetOrdinal("rfcCliente")) : string.Empty;
                        Item.TelefonoCliente = !dr.IsDBNull(dr.GetOrdinal("telefonoCliente")) ? dr.GetString(dr.GetOrdinal("telefonoCliente")) : string.Empty;
                        Item.KilosPesajeGanado = !dr.IsDBNull(dr.GetOrdinal("kilosPesaje")) ? dr.GetDecimal(dr.GetOrdinal("kilosPesaje")) : 0;
                        Item.CostoPesajeGanado = !dr.IsDBNull(dr.GetOrdinal("montoPorCobrarPesaje")) ? dr.GetDecimal(dr.GetOrdinal("montoPorCobrarPesaje")) : 0;
                    }
                    else if(modulo == 2) //venta general
                    {
                        Item.NombreCliente = !dr.IsDBNull(dr.GetOrdinal("nombreCliente")) ? dr.GetString(dr.GetOrdinal("nombreCliente")) : string.Empty;
                        Item.RFCCliente = !dr.IsDBNull(dr.GetOrdinal("rfcCliente")) ? dr.GetString(dr.GetOrdinal("rfcCliente")) : string.Empty;
                        Item.TelefonoCliente = !dr.IsDBNull(dr.GetOrdinal("telefonoCliente")) ? dr.GetString(dr.GetOrdinal("telefonoCliente")) : string.Empty;
                    }

                }
                dr.Close();
                return Item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ComprobantePagosModels> Comprobante_spCIDDB_get_detallesPagos(int modulo, string id, string conexion)
        {
            try
            {
                List<ComprobantePagosModels> Lista = new List<ComprobantePagosModels>();
                ComprobantePagosModels Item;
                object[] parametros = { modulo, id };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "comprobante.spCIDDB_get_detallesPagos", parametros);

                while (dr.Read())
                {
                    Item = new ComprobantePagosModels();
                    Item.FormaPago = !dr.IsDBNull(dr.GetOrdinal("descripcion")) ? dr.GetString(dr.GetOrdinal("descripcion")) : string.Empty;
                    Item.FechaPago = !dr.IsDBNull(dr.GetOrdinal("fecha")) ? dr.GetString(dr.GetOrdinal("fecha")) : string.Empty;
                    Item.MontoPagado = !dr.IsDBNull(dr.GetOrdinal("monto")) ? dr.GetDecimal(dr.GetOrdinal("monto")) : 0;

                    Lista.Add(Item);
                }
                dr.Close();
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ComprobanteGanadoModels> Comprobante_spCIDDB_get_detallesGanadosCompra(bool esMacho, int modulo, string id, string conexion)
        {
            try
            {
                List<ComprobanteGanadoModels> Lista = new List<ComprobanteGanadoModels>();
                ComprobanteGanadoModels Item;
                object[] parametros = { esMacho, modulo, id };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "comprobante.spCIDDB_get_detallesGanado", parametros);

                while (dr.Read())
                {
                    Item = new ComprobanteGanadoModels();
                    Item.RangoPeso = !dr.IsDBNull(dr.GetOrdinal("rangoPeso")) ? dr.GetString(dr.GetOrdinal("rangoPeso")) : string.Empty;
                    Item.Cabezas = !dr.IsDBNull(dr.GetOrdinal("cabezas")) ? dr.GetInt32(dr.GetOrdinal("cabezas")) : 0;
                    Item.TipoGanado = !dr.IsDBNull(dr.GetOrdinal("tipoGanado")) ? dr.GetString(dr.GetOrdinal("tipoGanado")) : string.Empty;
                    Item.Kilos = !dr.IsDBNull(dr.GetOrdinal("kilos")) ? dr.GetDecimal(dr.GetOrdinal("kilos")) : 0;
                    Item.Precio = !dr.IsDBNull(dr.GetOrdinal("precio")) ? dr.GetDecimal(dr.GetOrdinal("precio")) : 0;
                    Item.Importe = !dr.IsDBNull(dr.GetOrdinal("importe")) ? dr.GetDecimal(dr.GetOrdinal("importe")) : 0;
                    Item.PesoPromedio = !dr.IsDBNull(dr.GetOrdinal("pesoPromedio")) ? dr.GetDecimal(dr.GetOrdinal("pesoPromedio")) : 0;
                    Lista.Add(Item);
                }
                dr.Close();
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}