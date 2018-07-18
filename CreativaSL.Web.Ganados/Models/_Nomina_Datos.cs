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
    public class Nomina_Datos
    {
        public List<NominaModels> ObtenerListaNomina(NominaModels Datos)
        {
            try
            {
                List<NominaModels> Lista = new List<NominaModels>();
                NominaModels Item;
                object[] parametros = {
                    Datos.EsBusqueda,
                    Datos.BandBusqClave,
                    Datos.BandIDSucursal,
                    Datos.BandBusqFechas,
                    Datos.ClaveNomina ?? string.Empty,
                    Datos.FechaInicio != null ? Datos.FechaInicio : DateTime.Today,
                    Datos.FechaFin != null ? Datos.FechaFin : DateTime.Today,
                    Datos.IDSucursal ?? string.Empty
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Nomina_get_ObtenerNominas", parametros);
                while (dr.Read())
                {
                    Item = new NominaModels();
                    Item.IDNomina = !dr.IsDBNull(dr.GetOrdinal("IDNomina")) ? dr.GetString(dr.GetOrdinal("IDNomina")) : string.Empty;
                    Item.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty; 
                    Item.ClaveNomina = !dr.IsDBNull(dr.GetOrdinal("ClaveNomina")) ? dr.GetString(dr.GetOrdinal("ClaveNomina")) : string.Empty;
                    Item.FechaInicio = !dr.IsDBNull(dr.GetOrdinal("FechaInicio")) ? dr.GetDateTime(dr.GetOrdinal("FechaInicio")) : DateTime.Today;
                    Item.FechaFin = !dr.IsDBNull(dr.GetOrdinal("FechaFin")) ? dr.GetDateTime(dr.GetOrdinal("FechaFin")) : DateTime.Today;
                    Item.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NombreSucursal")) ? dr.GetString(dr.GetOrdinal("NombreSucursal")) : string.Empty;
                    Item.Estatus = !dr.IsDBNull(dr.GetOrdinal("EstatusNomina")) ? dr.GetBoolean(dr.GetOrdinal("EstatusNomina")) : false;
                    Lista.Add(Item);
                }
                dr.Close();
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<NominaModels> ObtenerListaNominaEmpleado(NominaModels Datos)
        {
            try
            {
                List<NominaModels> Lista = new List<NominaModels>();
                NominaModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Nomina_get_Empleados", Datos.IDSucursal);
                while (dr.Read())
                {
                    Item = new NominaModels();
                    Item.IDEmpleado = !dr.IsDBNull(dr.GetOrdinal("IDEmpleado")) ? dr.GetString(dr.GetOrdinal("IDEmpleado")) : string.Empty;
                    Item.CodigoUsuario = !dr.IsDBNull(dr.GetOrdinal("Clave")) ? dr.GetString(dr.GetOrdinal("Clave")) : string.Empty;
                    Item.NombreEmpleado = !dr.IsDBNull(dr.GetOrdinal("Nombre")) ? dr.GetString(dr.GetOrdinal("Nombre")) : string.Empty;
                    Item.Puesto = !dr.IsDBNull(dr.GetOrdinal("Puesto")) ? dr.GetString(dr.GetOrdinal("Puesto")) : string.Empty;
                    Item.CategoriaPuesto = !dr.IsDBNull(dr.GetOrdinal("Categoria")) ? dr.GetString(dr.GetOrdinal("Puesto")) : string.Empty;
                    Item.Sueldo = !dr.IsDBNull(dr.GetOrdinal("Sueldo")) ? dr.GetDecimal(dr.GetOrdinal("Sueldo")) : 0;
                    Item.Percepciones = !dr.IsDBNull(dr.GetOrdinal("Percepciones")) ? dr.GetDecimal(dr.GetOrdinal("Percepciones")) : 0;
                    Item.Deducciones = !dr.IsDBNull(dr.GetOrdinal("Deducciones")) ? dr.GetDecimal(dr.GetOrdinal("Deducciones")) : 0;
                    Lista.Add(Item);
                }
                dr.Close();
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ANomina(NominaModels Datos)
        {
            try
            {
                object Resultado = SqlHelper.ExecuteScalar(Datos.Conexion, CommandType.StoredProcedure, "spCSLDB_Nomina_a_Nomina",
                     new SqlParameter("@IDSucursal", Datos.IDSucursal),
                     new SqlParameter("@FechaInicio", Datos.FechaInicio),
                     new SqlParameter("@FechaFin", Datos.FechaFin),
                     new SqlParameter("@TablaEmpleado", Datos.TablaEmpladoNomina),
                     new SqlParameter("@IDUsuario", Datos.Usuario)
                     );
                if (Resultado != null)
                {
                    if (!string.IsNullOrEmpty(Resultado.ToString()))
                    {
                        Datos.Completado = true;
                        Datos.IDNomina = Resultado.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<NominaModels> ObtenerListaDetalleNomina(NominaModels Datos)
        {
            try
            {
                List<NominaModels> Lista = new List<NominaModels>();
                NominaModels Item;
                object[] parametros = {
                    Datos.IDNomina ?? string.Empty,
                    Datos.IDSucursal ?? string.Empty
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Nomina_get_NominaEmpleadosXID", parametros);
                while (dr.Read())
                {
                    Item = new NominaModels();
                    Item.IDNomina = !dr.IsDBNull(dr.GetOrdinal("IDNomina")) ? dr.GetString(dr.GetOrdinal("IDNomina")) : string.Empty;
                    Item.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty;
                    Item.ClaveNomina = !dr.IsDBNull(dr.GetOrdinal("ClaveNomina")) ? dr.GetString(dr.GetOrdinal("ClaveNomina")) : string.Empty;
                    Item.CodigoUsuario = !dr.IsDBNull(dr.GetOrdinal("Clave")) ? dr.GetString(dr.GetOrdinal("Clave")) : string.Empty;
                    Item.NombreEmpleado = !dr.IsDBNull(dr.GetOrdinal("Nombre")) ? dr.GetString(dr.GetOrdinal("Nombre")) : string.Empty;
                    Item.Puesto = !dr.IsDBNull(dr.GetOrdinal("Puesto")) ? dr.GetString(dr.GetOrdinal("Puesto")) : string.Empty;
                    Item.CategoriaPuesto = !dr.IsDBNull(dr.GetOrdinal("Categoria")) ? dr.GetString(dr.GetOrdinal("Categoria")) : string.Empty;
                    Item.Sueldo = !dr.IsDBNull(dr.GetOrdinal("Sueldo")) ? dr.GetDecimal(dr.GetOrdinal("Sueldo")) : 0;
                    Item.Percepciones = !dr.IsDBNull(dr.GetOrdinal("Percepciones")) ? dr.GetDecimal(dr.GetOrdinal("Percepciones")) : 0;
                    Item.Deducciones = !dr.IsDBNull(dr.GetOrdinal("Deducciones")) ? dr.GetDecimal(dr.GetOrdinal("Deducciones")) : 0;
                    Item.IDEmpleado = !dr.IsDBNull(dr.GetOrdinal("IDEmpleado")) ? dr.GetString(dr.GetOrdinal("IDEmpleado")) : string.Empty;
                    Lista.Add(Item);
                }
                dr.Close();
                return Lista;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public NominaModels AgregarConceptoNomina(NominaModels Datos)
        {
            try
            {
                object[] parametros =
                {
                    Datos.EsFijo, Datos.IDEmpleado, Datos.IDConcepto, Datos.Sueldo, Datos.Usuario
                };
                object Resultado = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_Nomina_set_AgregarConcepto", parametros);
                if (Resultado != null)
                {
                    int IDRegistro = 0;
                    if (int.TryParse(Resultado.ToString(), out IDRegistro))
                    {
                        if (IDRegistro == 1)
                        {
                            Datos.Completado = true;
                        }
                        Datos.Resultado = IDRegistro;
                    }
                }
                return Datos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
       
        public List<CatConceptoNominaModels> ObtenerConceptosNomina(NominaModels Datos)
        {
            try
            {
                List<CatConceptoNominaModels> lista = new List<CatConceptoNominaModels>();
                CatConceptoNominaModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Nomina_Combo_get_CatConceptoNomina");
                while (dr.Read())
                {
                    item = new CatConceptoNominaModels();
                    item.IDConceptoNomina = !dr.IsDBNull(dr.GetOrdinal("IDConcepto")) ? dr.GetInt32(dr.GetOrdinal("IDConcepto")) : 0;
                    item.Descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
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

        public NominaModels ObtenerListasDeConceptosXID(NominaModels Datos)
        {
            try
            {
                object[] parametros = { Datos.IDEmpleado};
                DataSet Ds = SqlHelper.ExecuteDataset(Datos.Conexion, "spCSLDB_Nomina_get_ConceptosNomina", parametros);
                if (Ds != null)
                {
                    if (Ds.Tables.Count > 0)
                    {
                        List<NominaConceptosFijosModels> ListaFijo = new List<NominaConceptosFijosModels>();
                        NominaConceptosFijosModels Item2;
                        DataTableReader Dr = Ds.Tables[0].CreateDataReader();
                        while (Dr.Read())
                        {
                            Item2 = new NominaConceptosFijosModels();
                            Item2.IDConceptosFijo = !Dr.IsDBNull(Dr.GetOrdinal("IDConceptoFijo")) ? Dr.GetString(Dr.GetOrdinal("IDConceptoFijo")) : string.Empty;
                            Item2.IDConcepto = !Dr.IsDBNull(Dr.GetOrdinal("IDConcepto")) ? Dr.GetInt32(Dr.GetOrdinal("IDConcepto")) : 0;
                            Item2.NombreConcepto = !Dr.IsDBNull(Dr.GetOrdinal("Concepto")) ? Dr.GetString(Dr.GetOrdinal("Concepto")) : string.Empty;
                            Item2.Monto = !Dr.IsDBNull(Dr.GetOrdinal("Monto")) ? Dr.GetDecimal(Dr.GetOrdinal("Monto")) : 0;
                            Item2.Simbolo = !Dr.IsDBNull(Dr.GetOrdinal("Simbolo")) ? Dr.GetString(Dr.GetOrdinal("Simbolo")) : string.Empty;
                            ListaFijo.Add(Item2);
                        }
                        Datos.ListaConceptosFijo = ListaFijo;
                        List<NominaConceptosEmpModels> ListaVariable = new List<NominaConceptosEmpModels>();
                        NominaConceptosEmpModels Item;
                        DataTableReader DTR = Ds.Tables[1].CreateDataReader();
                        DataTable Tbl1 = Ds.Tables[1];
                        while (DTR.Read())
                        {
                            Item = new NominaConceptosEmpModels();
                            Item.IDConceptoEmpleado = !DTR.IsDBNull(DTR.GetOrdinal("IDConceptoVariable")) ? DTR.GetString(DTR.GetOrdinal("IDConceptoVariable")) : string.Empty;
                            Item.IDConcepto = !DTR.IsDBNull(DTR.GetOrdinal("IDConcepto")) ? DTR.GetInt32(DTR.GetOrdinal("IDConcepto")) : 0;
                            Item.NombreConcepto = !DTR.IsDBNull(DTR.GetOrdinal("Concepto")) ? DTR.GetString(DTR.GetOrdinal("Concepto")) : string.Empty;
                            Item.Monto = !DTR.IsDBNull(DTR.GetOrdinal("Monto")) ? DTR.GetDecimal(DTR.GetOrdinal("Monto")) : 0;
                            Item.Simbolo = !DTR.IsDBNull(DTR.GetOrdinal("Simbolo")) ? DTR.GetString(DTR.GetOrdinal("Simbolo")) : string.Empty;
                            ListaVariable.Add(Item);
                        }
                        Datos.ListaConceptosVariable = ListaVariable;
                    }
                }
                return Datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public NominaModels ElimnarConceptosNomina(NominaModels Datos)
        {
            try
            {
                object[] parametros =
                {
                    Datos.IDEmpleado, Datos.EsFijo, Datos.Usuario
                };
                object Resultado = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_Nomina_set_QuitarConceptoNomina", parametros);
                if (Resultado != null)
                {
                    int IDRegistro = 0;
                    if (int.TryParse(Resultado.ToString(), out IDRegistro))
                    {
                        if (IDRegistro > 0)
                        {
                            Datos.Completado = true;
                            Datos.Resultado = IDRegistro;
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

        public void ObtenerReporteNominaDetalle(NominaModels Datos)
        {
            try
            {
                object[] Parametros = { Datos.IDNomina, Datos.IDSucursal };
                DataSet Ds = SqlHelper.ExecuteDataset(Datos.Conexion, "spCSLDB_Nomina_get_RptNominaDetalle", Parametros);
                if (Ds != null)
                {
                    if (Ds.Tables.Count == 2)
                    {
                        DataTableReader Dr = Ds.Tables[0].CreateDataReader();
                        while (Dr.Read())
                        {
                            Datos.ClaveNomina = Dr.GetString(Dr.GetOrdinal("ClaveNomina"));
                            Datos.FechaInicio = Dr.GetDateTime(Dr.GetOrdinal("FechaInicio"));
                            Datos.FechaFin = Dr.GetDateTime(Dr.GetOrdinal("FechaFin"));
                            Datos.DiasPeriodo = Dr.GetInt32(Dr.GetOrdinal("DiasPeriodo"));
                            Datos.PeriodoFechas = "DEL " + Datos.FechaInicio.ToShortDateString() + " AL " + Datos.FechaFin.ToShortDateString();
                        }
                        if (Datos.Opcion == 1) //Quitar el if solo esta puesto para hacer dinamino Una parte
                        {
                            DataTableReader Dr2 = Ds.Tables[1].CreateDataReader();
                            List<NominaResumenDetalleModels> Lista = new List<NominaResumenDetalleModels>();
                            NominaResumenDetalleModels Item;
                            while (Dr2.Read())
                            {
                                Item = new NominaResumenDetalleModels();
                                Item.NombreEmpleado = Dr2.GetString(Dr2.GetOrdinal("Empleado"));
                                Item.DiasLaborados = Dr2.GetInt32(Dr2.GetOrdinal("DiasLaborados"));
                                Item.DiasDescanso = Dr2.GetInt32(Dr2.GetOrdinal("DiasDescanso"));
                                Item.Faltas = Dr2.GetInt32(Dr2.GetOrdinal("Faltas"));
                                Item.DiasDescuentoFaltas = Dr2.GetInt32(Dr2.GetOrdinal("DiasDescuentoFaltas"));
                                Item.Retardos = Dr2.GetInt32(Dr2.GetOrdinal("Retardos"));
                                Item.FaltasRetardo = Dr2.GetInt32(Dr2.GetOrdinal("FaltasRetardos"));
                                Item.DiasDescuentoRetardos = Dr2.GetInt32(Dr2.GetOrdinal("DiasDescuentoRetardos"));
                                Item.DiasDescuentoTotales = Dr2.GetInt32(Dr2.GetOrdinal("DiasDescuentoTotales"));
                                Item.DiasFestivos = Dr2.GetInt32(Dr2.GetOrdinal("DiasFestivos"));
                                Item.DiasDomingo = Dr2.GetInt32(Dr2.GetOrdinal("DiasDomingo"));
                                Item.DiasVacaciones = Dr2.GetInt32(Dr2.GetOrdinal("DiasVacaciones"));
                                Lista.Add(Item);
                            }
                            Datos.ListaResumenDetalleNomina = Lista;
                        }
                        Datos.Completado = true;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ObtenerReporteNominaSaldos(NominaModels Datos)
        {
            try
            {
                object[] Parametros = { Datos.IDNomina, Datos.IDSucursal };
                DataSet Ds = SqlHelper.ExecuteDataset(Datos.Conexion, "spCSLDB_Nomina_get_RptNominaSaldos", Parametros);
                if (Ds != null)
                {
                    if (Ds.Tables.Count == 3)
                    {
                        DataTableReader Dr = Ds.Tables[0].CreateDataReader();
                        while (Dr.Read())
                        {
                            Datos.ClaveNomina = Dr.GetString(Dr.GetOrdinal("ClaveNomina"));
                            Datos.FechaInicio = Dr.GetDateTime(Dr.GetOrdinal("FechaInicio"));
                            Datos.FechaFin = Dr.GetDateTime(Dr.GetOrdinal("FechaFin"));
                            Datos.DiasPeriodo = Dr.GetInt32(Dr.GetOrdinal("DiasPeriodo"));
                            Datos.PeriodoFechas = "DEL " + Datos.FechaInicio.ToShortDateString() + " AL " + Datos.FechaFin.ToShortDateString();
                        }
                       
                            DataTableReader Dr2 = Ds.Tables[1].CreateDataReader();
                            List<NominaDetalleModels> Lista = new List<NominaDetalleModels>();
                            NominaDetalleModels Item;
                            while (Dr2.Read())
                            {
                                Item = new NominaDetalleModels();
                                Item.NombreEmpleado = Dr2.GetString(Dr2.GetOrdinal("Empleado"));
                                Item.Percepciones = Dr2.GetDecimal(Dr2.GetOrdinal("Percepciones"));
                                Item.Deducciones = Dr2.GetDecimal(Dr2.GetOrdinal("Deducciones"));
                                Item.Total = Dr2.GetDecimal(Dr2.GetOrdinal("Total"));
                                Lista.Add(Item);
                            }
                            Datos.ListaNominaDetalle = Lista;

                            DataTableReader Dr3 = Ds.Tables[2].CreateDataReader();
                            List<NominaConceptosFijosModels> Lista02 = new List<NominaConceptosFijosModels>();
                             List<CatEmpleadoModels> ListaEm = new List<CatEmpleadoModels>();
                        CatEmpleadoModels Item003;
                        
                        NominaConceptosFijosModels Item02;
                        while (Dr3.Read())
                        {
                            Item003 = new CatEmpleadoModels();
                            Item003.IDEmpleado = Dr3.GetString(Dr3.GetOrdinal("IDEmpleado"));
                            Item003.Nombre = Dr3.GetString(Dr3.GetOrdinal("Empleado"));
                            ListaEm.Add(Item003);

                            Item02 = new NominaConceptosFijosModels();
                            Item02.IDEmpleado = Dr3.GetString(Dr3.GetOrdinal("IDEmpleado"));
                            Item02.NombreEmpleado = Dr3.GetString(Dr3.GetOrdinal("Empleado"));
                            Item02.NombreConcepto = Dr3.GetString(Dr3.GetOrdinal("Concepto"));
                            Item02.IDConcepto = Dr3.GetInt32(Dr3.GetOrdinal("ClaveConcepto"));
                            Item02.Monto = Dr3.GetDecimal(Dr3.GetOrdinal("Monto"));
                            Item02.Simbolo = Dr3.GetString(Dr3.GetOrdinal("Simbolo"));
                            Lista02.Add(Item02);

                            Datos.ListaConceptosFijo = Lista02;
                            Datos.listaEmpleado = ListaEm;
                        }
                        
                        Datos.Completado = true;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NominaModels ObtenerDatosEmpresaTipo1(NominaModels Datos)
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
                    Bitmap bmpFromString = Datos.DatosEmpresa.LogoEmpresa.Base64StringToBitmap();
                    Datos.DatosEmpresa.ImagenContruida = bmpFromString.ToBase64ImageReport(ImageFormat.Png);
                }
                return Datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NominaModels GenerarNomina(NominaModels Datos)
        {
            try
            {
                object[] parametros =
                {
                    Datos.IDNomina, Datos.Usuario
                };
                object Resultado = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_Nomina_set_GenerarNomina", parametros);
                if (Resultado != null)
                {
                    if(!string.IsNullOrEmpty(Resultado.ToString()))
                    {
                      Datos.Completado = true;
                        Datos.IDNomina = Resultado.ToString();
                    }
                }
                return Datos;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}