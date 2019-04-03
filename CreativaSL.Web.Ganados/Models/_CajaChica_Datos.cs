using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CajaChica_Datos
    {
        public List<CajaChicaModels> ObtenerCajasChicasAbiertas()
        {
            try
            {
                List<CajaChicaModels> Lista = new List<CajaChicaModels>();
                SqlDataReader Dr = SqlHelper.ExecuteReader(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_ObtenerCajasAbiertas");
                CajaChicaModels Item;
                while (Dr.Read())
                {
                    Item = new CajaChicaModels
                    {
                        IdCaja = !Dr.IsDBNull(Dr.GetOrdinal("IdCaja")) ? Dr.GetInt64(Dr.GetOrdinal("IdCaja")) : 0,
                        FechaApertura = !Dr.IsDBNull(Dr.GetOrdinal("FechaApertura")) ? Dr.GetDateTime(Dr.GetOrdinal("FechaApertura")) : DateTime.MinValue,
                        NombreEmpleado = !Dr.IsDBNull(Dr.GetOrdinal("NombreEmpleado")) ? Dr.GetString(Dr.GetOrdinal("NombreEmpleado")) : string.Empty,
                        MontoApertura = !Dr.IsDBNull(Dr.GetOrdinal("MontoApertura")) ? Dr.GetDecimal(Dr.GetOrdinal("MontoApertura")) : 0m,
                        Saldo = !Dr.IsDBNull(Dr.GetOrdinal("Saldo")) ? Dr.GetDecimal(Dr.GetOrdinal("Saldo")) : 0m,
                        PersonaEntrega = !Dr.IsDBNull(Dr.GetOrdinal("PersonaEntrega")) ? Dr.GetString(Dr.GetOrdinal("PersonaEntrega")) : string.Empty
                    };
                    Lista.Add(Item);
                }
                Dr.Close();
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CajaChicaModelsResult ObtenerCajasChicasHistorial(DataTableParameters oDataTableParameters)
        {
            try
            {
                object[] Parametros = { oDataTableParameters.Draw, oDataTableParameters.SearchValue, oDataTableParameters.Length, oDataTableParameters.Start };
                DataSet Ds = SqlHelper.ExecuteDataset(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_ObtenerCajasHistorial", Parametros);
                if (Ds != null)
                {
                    if (Ds.Tables.Count == 2)
                    {

                        DataTableReader Dr0 = Ds.Tables[0].CreateDataReader();
                        int TotalRows = 0, TotalRowsBusqueda = 0;
                        while (Dr0.Read())
                        {
                            TotalRows = Dr0.IsDBNull(Dr0.GetOrdinal("TotalRows")) ? Dr0.GetInt32(Dr0.GetOrdinal("TotalRows")) : 0;
                            TotalRowsBusqueda = Dr0.IsDBNull(Dr0.GetOrdinal("TotalRowsBusqueda")) ? Dr0.GetInt32(Dr0.GetOrdinal("TotalRowsBusqueda")) : 0;
                            break;
                        }
                        Dr0.Close();

                        List<CajaChicaModels> Lista = new List<CajaChicaModels>();
                        DataTableReader Dr = Ds.Tables[1].CreateDataReader();
                        CajaChicaModels Item;
                        while (Dr.Read())
                        {
                            Item = new CajaChicaModels
                            {
                                IdCaja = !Dr.IsDBNull(Dr.GetOrdinal("IdCaja")) ? Dr.GetInt64(Dr.GetOrdinal("IdCaja")) : 0,
                                FechaApertura = !Dr.IsDBNull(Dr.GetOrdinal("FechaApertura")) ? Dr.GetDateTime(Dr.GetOrdinal("FechaApertura")) : DateTime.MinValue,
                                NombreEmpleado = !Dr.IsDBNull(Dr.GetOrdinal("NombreEmpleado")) ? Dr.GetString(Dr.GetOrdinal("NombreEmpleado")) : string.Empty,
                                MontoApertura = !Dr.IsDBNull(Dr.GetOrdinal("MontoApertura")) ? Dr.GetDecimal(Dr.GetOrdinal("MontoApertura")) : 0m,
                                Saldo = !Dr.IsDBNull(Dr.GetOrdinal("Saldo")) ? Dr.GetDecimal(Dr.GetOrdinal("Saldo")) : 0m,
                                TotalArqueo = !Dr.IsDBNull(Dr.GetOrdinal("Arqueo")) ? Dr.GetDecimal(Dr.GetOrdinal("Arqueo")) : 0m,
                                Diferencia = !Dr.IsDBNull(Dr.GetOrdinal("Diferencia")) ? Dr.GetDecimal(Dr.GetOrdinal("Diferencia")) : 0m
                                //PersonaEntrega = !Dr.IsDBNull(Dr.GetOrdinal("PersonaEntrega")) ? Dr.GetString(Dr.GetOrdinal("PersonaEntrega")) : string.Empty
                            };
                            Lista.Add(Item);
                        }
                        Dr.Close();
                        return new CajaChicaModelsResult { TotalRecords = TotalRows, SearchRecords = TotalRowsBusqueda, Lista = Lista };
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CrearCajaChica(CajaChicaModels model, string IdUsuario)
        {
            try
            {
                object[] Parametros = { model.IdPropietario, model.MontoApertura, model.KeyWord, IdUsuario };
                object Result = SqlHelper.ExecuteScalar(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_AperturarCaja", Parametros);
                if (Result != null)
                {
                    int Resultado = 0;
                    int.TryParse(Result.ToString(), out Resultado);
                    return Resultado;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CatEmpleadoModels> LlenarComboEmpleadosContadores(bool IncluirSelect)
        {
            try
            {
                List<CatEmpleadoModels> Lista = new List<CatEmpleadoModels>();
                SqlDataReader Dr = SqlHelper.ExecuteReader(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_ComboContadores", IncluirSelect);
                CatEmpleadoModels Item;
                while (Dr.Read())
                {
                    Item = new CatEmpleadoModels
                    {
                        IDEmpleado = !Dr.IsDBNull(Dr.GetOrdinal("IdEmpleado")) ? Dr.GetString(Dr.GetOrdinal("IdEmpleado")) : string.Empty,
                        Nombre = !Dr.IsDBNull(Dr.GetOrdinal("NombreCompleto")) ? Dr.GetString(Dr.GetOrdinal("NombreCompleto")) : string.Empty
                    };
                    Lista.Add(Item);
                }
                Dr.Close();
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ArqueoCajaChicaModels> ObtenerDenominaciones(Int64 IdCaja)
        {
            try
            {
                List<ArqueoCajaChicaModels> Lista = new List<ArqueoCajaChicaModels>();
                SqlDataReader Dr = SqlHelper.ExecuteReader(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_get_DenominacionesArqueoCaja", IdCaja);
                ArqueoCajaChicaModels Item;
                while (Dr.Read())
                {
                    Item = new ArqueoCajaChicaModels
                    {
                        IdDenominacion = !Dr.IsDBNull(Dr.GetOrdinal("id_denominacion")) ? Dr.GetInt32(Dr.GetOrdinal("id_denominacion")) : 0,
                        IdTipo = !Dr.IsDBNull(Dr.GetOrdinal("tipo")) ? Dr.GetString(Dr.GetOrdinal("tipo")) : string.Empty,
                        Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("descripcion")) ? Dr.GetString(Dr.GetOrdinal("descripcion")) : string.Empty,
                        Valor = !Dr.IsDBNull(Dr.GetOrdinal("valor")) ? Dr.GetDecimal(Dr.GetOrdinal("valor")) : 0m,
                        Cantidad = !Dr.IsDBNull(Dr.GetOrdinal("Cantidad")) ? Dr.GetInt32(Dr.GetOrdinal("Cantidad")) : 0,
                        Subtotal = !Dr.IsDBNull(Dr.GetOrdinal("Subtotal")) ? Dr.GetDecimal(Dr.GetOrdinal("Subtotal")) : 0m
                    };

                    Lista.Add(Item);
                }
                Dr.Close();
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GuardarArqueoCaja(Int64 IdCaja, List<ArqueoCajaChicaModels> Lista, string IdUsuario)
        {
            try
            {
                object Result = SqlHelper.ExecuteScalar(_ConexionRepositorio.CadenaConexion, CommandType.StoredProcedure, "cajachica.spCIDDB_ArqueoCaja",
                    new SqlParameter("@IDCaja", IdCaja),
                    new SqlParameter("@TablaDetalle", GenerarTabla(Lista)),
                    new SqlParameter("@IDUsuario", IdUsuario)
                    );
                if (Result != null)
                {
                    int Resultado = 0;
                    int.TryParse(Result.ToString(), out Resultado);
                    return Resultado;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GenerarTabla(List<ArqueoCajaChicaModels> ListaDenominaciones)
        {
            try
            {
                DataTable tabla = new DataTable();
                tabla.Columns.Add("IdDenominacion", typeof(int));
                tabla.Columns.Add("Cantidad", typeof(int));
                foreach (var item in ListaDenominaciones)
                {
                    object[] fila = { item.IdDenominacion, item.Cantidad };
                    tabla.Rows.Add(fila);
                }
                return tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Int64 ObtenerIdCajaChica(string IdUsuario)
        {
            try
            {
                object Result = SqlHelper.ExecuteScalar(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_TieneCajaChica", IdUsuario);
                if (Result != null)
                {
                    Int64 Resultado = -1;
                    Int64.TryParse(Result.ToString(), out Resultado);
                    return Resultado;
                }
                return -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MovimientosCajaChicaModels> ObtenerDetalleXIdCaja(Int64 IdCaja)
        {
            try
            {
                List<MovimientosCajaChicaModels> Lista = new List<MovimientosCajaChicaModels>();
                SqlDataReader Dr = SqlHelper.ExecuteReader(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_get_MovimientosCajaChica", IdCaja);
                MovimientosCajaChicaModels Item;
                while (Dr.Read())
                {
                    Item = new MovimientosCajaChicaModels
                    {
                        IdMovimiento = !Dr.IsDBNull(Dr.GetOrdinal("IdMovimiento")) ? Dr.GetInt64(Dr.GetOrdinal("IdMovimiento")) : 0,
                        Fecha = !Dr.IsDBNull(Dr.GetOrdinal("Fecha")) ? Dr.GetDateTime(Dr.GetOrdinal("Fecha")) : DateTime.MinValue,
                        Entrega = !Dr.IsDBNull(Dr.GetOrdinal("Entrega")) ? Dr.GetString(Dr.GetOrdinal("Entrega")) : string.Empty,
                        Entrada = !Dr.IsDBNull(Dr.GetOrdinal("Entrada")) ? Dr.GetDecimal(Dr.GetOrdinal("Entrada")) : 0m,
                        Salida = !Dr.IsDBNull(Dr.GetOrdinal("Salida")) ? Dr.GetDecimal(Dr.GetOrdinal("Salida")) : 0m,
                        Recibe = !Dr.IsDBNull(Dr.GetOrdinal("Recibe")) ? Dr.GetString(Dr.GetOrdinal("Recibe")) : string.Empty,
                        Concepto = !Dr.IsDBNull(Dr.GetOrdinal("Concepto")) ? Dr.GetString(Dr.GetOrdinal("Concepto")) : string.Empty,
                        Saldo = !Dr.IsDBNull(Dr.GetOrdinal("Saldo")) ? Dr.GetDecimal(Dr.GetOrdinal("Saldo")) : 0m
                    };
                    Lista.Add(Item);
                }
                Dr.Close();
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConceptosCajaChicaModels> LlenarComboConceptos(bool IncluirSelect)
        {
            try
            {
                List<ConceptosCajaChicaModels> Lista = new List<ConceptosCajaChicaModels>();
                SqlDataReader Dr = SqlHelper.ExecuteReader(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_combo_CatConceptos", IncluirSelect);
                ConceptosCajaChicaModels Item;
                while (Dr.Read())
                {
                    Item = new ConceptosCajaChicaModels
                    {
                        IdConcepto = !Dr.IsDBNull(Dr.GetOrdinal("IDConcepto")) ? Dr.GetInt32(Dr.GetOrdinal("IDConcepto")) : 0,
                        Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("Descripcion")) ? Dr.GetString(Dr.GetOrdinal("Descripcion")) : string.Empty
                    };
                    Lista.Add(Item);
                }
                Dr.Close();
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FormaPagoCajaChicaModels> LlenarComboFormaPagos(bool IncluirSelect)
        {
            try
            {
                List<FormaPagoCajaChicaModels> Lista = new List<FormaPagoCajaChicaModels>();
                SqlDataReader Dr = SqlHelper.ExecuteReader(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_combo_CatFormasPago", IncluirSelect);
                FormaPagoCajaChicaModels Item;
                while (Dr.Read())
                {
                    Item = new FormaPagoCajaChicaModels
                    {
                        IdFormaPago = !Dr.IsDBNull(Dr.GetOrdinal("IdFormaPago")) ? Dr.GetInt32(Dr.GetOrdinal("IdFormaPago")) : 0,
                        Descripcion = !Dr.IsDBNull(Dr.GetOrdinal("Descripcion")) ? Dr.GetString(Dr.GetOrdinal("Descripcion")) : string.Empty
                    };
                    Lista.Add(Item);
                }
                Dr.Close();
                return Lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GuardarMovimiento(MovimientosCajaChicaModels model, string IdUsuario)
        {
            try
            {
                object[] Parametros = { model.IdCaja, model.IdCategoria, model.Concepto, model.Salida,
                                        model.IdFormaPago, model.Recibe, model.FolioCheque, IdUsuario };
                object Result = SqlHelper.ExecuteScalar(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_GuardarMovimiento", Parametros);
                if (Result != null)
                {
                    int Resultado = 0;
                    int.TryParse(Result.ToString(), out Resultado);
                    return Resultado;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ModificarMovimiento(MovimientosCajaChicaModels model, string IdUsuario)
        {
            try
            {
                object[] Parametros = { model.IdMovimiento, model.IdCategoria, model.Concepto, model.Salida,
                                        model.IdFormaPago, model.Recibe, model.FolioCheque, IdUsuario };
                object Result = SqlHelper.ExecuteScalar(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_ModificarMovimiento", Parametros);
                if (Result != null)
                {
                    int Resultado = 0;
                    int.TryParse(Result.ToString(), out Resultado);
                    return Resultado;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MovimientosCajaChicaModels ObtenerDetalleMovimientoXId(Int64 IdMovimiento)
        {
            try
            {
                MovimientosCajaChicaModels model = new MovimientosCajaChicaModels();
                SqlDataReader Dr = SqlHelper.ExecuteReader(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_ObtenerDatosCajaChicaDetalle", IdMovimiento);
                while (Dr.Read())
                {
                    model.IdMovimiento = IdMovimiento;
                    model.IdCategoria = !Dr.IsDBNull(Dr.GetOrdinal("IdConcepto")) ? Dr.GetInt32(Dr.GetOrdinal("IdConcepto")) : 0;
                    model.Concepto = !Dr.IsDBNull(Dr.GetOrdinal("Concepto")) ? Dr.GetString(Dr.GetOrdinal("Concepto")) : string.Empty;
                    model.Salida = !Dr.IsDBNull(Dr.GetOrdinal("Monto")) ? Dr.GetDecimal(Dr.GetOrdinal("Monto")) : 0m;
                    model.IdFormaPago = !Dr.IsDBNull(Dr.GetOrdinal("IdFormaPago")) ? Dr.GetInt32(Dr.GetOrdinal("IdFormaPago")) : 0;
                    model.Recibe = !Dr.IsDBNull(Dr.GetOrdinal("PersonaRecibe")) ? Dr.GetString(Dr.GetOrdinal("PersonaRecibe")) : string.Empty;
                    break;
                }
                Dr.Close();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EliminarMovimiento(Int64 IdMovimiento, string IdUsuario)
        {
            try
            {
                object[] Parametros = { IdMovimiento, IdUsuario };
                object Result = SqlHelper.ExecuteScalar(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_EliminarMovimiento", Parametros);
                if (Result != null)
                {
                    int Resultado = 0;
                    int.TryParse(Result.ToString(), out Resultado);
                    return Resultado;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EliminarCaja(Int64 IdCaja, string IdUsuario)
        {
            try
            {
                object[] Parametros = { IdCaja, IdUsuario };
                object Result = SqlHelper.ExecuteScalar(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_EliminarCajaChica", Parametros);
                if (Result != null)
                {
                    int Resultado = 0;
                    int.TryParse(Result.ToString(), out Resultado);
                    return Resultado;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReporteCajaChica ObtenerDatosReporteCajaChica(Int64 IdCaja)
        {
            try
            {
                DataSet Ds = SqlHelper.ExecuteDataset(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_get_ReporteCajaChica", IdCaja);
                if(Ds!= null)
                {
                    if(Ds.Tables.Count == 3)
                    {
                        ReporteCajaChica Resultado = new ReporteCajaChica();

                        #region Movimientos

                        List<MovimientosCajaChicaModels> Lista = new List<MovimientosCajaChicaModels>();
                        DataTableReader Dr = Ds.Tables[0].CreateDataReader();
                        MovimientosCajaChicaModels Item;
                        while (Dr.Read())
                        {
                            Item = new MovimientosCajaChicaModels
                            {
                                IdMovimiento = !Dr.IsDBNull(Dr.GetOrdinal("IdMovimiento")) ? Dr.GetInt64(Dr.GetOrdinal("IdMovimiento")) : 0,
                                Fecha = !Dr.IsDBNull(Dr.GetOrdinal("Fecha")) ? Dr.GetDateTime(Dr.GetOrdinal("Fecha")) : DateTime.MinValue,
                                Entrega = !Dr.IsDBNull(Dr.GetOrdinal("Entrega")) ? Dr.GetString(Dr.GetOrdinal("Entrega")) : string.Empty,
                                Entrada = !Dr.IsDBNull(Dr.GetOrdinal("Entrada")) ? Dr.GetDecimal(Dr.GetOrdinal("Entrada")) : 0m,
                                Salida = !Dr.IsDBNull(Dr.GetOrdinal("Salida")) ? Dr.GetDecimal(Dr.GetOrdinal("Salida")) : 0m,
                                Recibe = !Dr.IsDBNull(Dr.GetOrdinal("Recibe")) ? Dr.GetString(Dr.GetOrdinal("Recibe")) : string.Empty,
                                Concepto = !Dr.IsDBNull(Dr.GetOrdinal("Concepto")) ? Dr.GetString(Dr.GetOrdinal("Concepto")) : string.Empty,
                                Saldo = !Dr.IsDBNull(Dr.GetOrdinal("Saldo")) ? Dr.GetDecimal(Dr.GetOrdinal("Saldo")) : 0m
                            };
                            Lista.Add(Item);
                        }
                        Dr.Close();

                        Resultado.ListaMovimientos = Lista;
                        #endregion

                        #region Arqueo

                        List<ArqueoCajaChicaModels> ListaArqueo = new List<ArqueoCajaChicaModels>();
                        DataTableReader Dr2 = Ds.Tables[1].CreateDataReader();
                        ArqueoCajaChicaModels ItemArqueo;
                        while (Dr2.Read())
                        {
                            ItemArqueo = new ArqueoCajaChicaModels
                            {
                                Valor = !Dr2.IsDBNull(Dr2.GetOrdinal("Denominacion")) ? Dr2.GetDecimal(Dr2.GetOrdinal("Denominacion")) : 0m,
                                Cantidad = !Dr2.IsDBNull(Dr2.GetOrdinal("Cantidad")) ? Dr2.GetInt32(Dr2.GetOrdinal("Cantidad")) : 0,
                                Subtotal = !Dr2.IsDBNull(Dr2.GetOrdinal("Importe")) ? Dr2.GetDecimal(Dr2.GetOrdinal("Importe")) : 0m
                            };

                            ListaArqueo.Add(ItemArqueo);
                        }
                        Dr2.Close();

                        Resultado.ListaDenominaciones = ListaArqueo;

                        #endregion

                        #region Conceptos
                        List<ConceptosCajaChicaModels> ListaConceptos = new List<ConceptosCajaChicaModels>();
                        DataTableReader Dr3 = Ds.Tables[2].CreateDataReader();
                        ConceptosCajaChicaModels ItemConcepto;
                        while (Dr3.Read())
                        {
                            ItemConcepto = new ConceptosCajaChicaModels
                            {
                                Importe = !Dr3.IsDBNull(Dr3.GetOrdinal("Importe")) ? Dr3.GetDecimal(Dr3.GetOrdinal("Importe")) : 0m,
                                Descripcion = !Dr3.IsDBNull(Dr3.GetOrdinal("Concepto")) ? Dr3.GetString(Dr3.GetOrdinal("Concepto")) : string.Empty
                            };
                            ListaConceptos.Add(ItemConcepto);
                        }
                        Dr3.Close();

                        Resultado.ListaConceptos = ListaConceptos;
                        #endregion

                        return Resultado;
                    }
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}