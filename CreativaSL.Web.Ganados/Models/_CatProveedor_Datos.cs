using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using CreativaSL.Web.Ganados.Models.Datatable;
using CreativaSL.Web.Ganados.Models.Dto.Base;
using CreativaSL.Web.Ganados.Models.Dto.ProveedorGanado;
using CreativaSL.Web.Ganados.Models.System;
using Newtonsoft.Json;

namespace CreativaSL.Web.Ganados.Models
{
    public class _CatProveedor_Datos : BaseSQL
    {
        #region Notas
        public void GuardarNota(CatProveedorModels model)
        {
            using (var sqlcon = new SqlConnection(model.Conexion))
            {
                using (var cmd = new SqlCommand("spCIDDB_Catalogo_CatProveedor_GuardarNota", sqlcon))
                {
                    //parametros de entrada
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdProveedor", SqlDbType.Char).Value = model.IDProveedor;
                    cmd.Parameters.Add("@Notas", SqlDbType.NVarChar).Value = model.Notas;

                    // execute
                    sqlcon.Open();

                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        model.RespuestaAjax = new RespuestaAjax();

                        while (reader.Read())
                        {
                            model.RespuestaAjax.Success = bool.Parse(reader["Success"].ToString());
                            model.RespuestaAjax.Mensaje = reader["Mensaje"].ToString();
                        }
                    }
                    reader.Close();
                }
            }
        }
        public void ObtenerNota(CatProveedorModels model)
        {
            using (var sqlcon = new SqlConnection(model.Conexion))
            {
                using (var cmd = new SqlCommand("spCIDDB_Catalogo_CatProveedor_ObtenerNota", sqlcon))
                {
                    //parametros de entrada
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdProveedor", SqlDbType.Char).Value = model.IDProveedor;

                    // execute
                    sqlcon.Open();

                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        model.RespuestaAjax = new RespuestaAjax();

                        while (reader.Read())
                        {
                            model.RespuestaAjax.Success = bool.Parse(reader["Success"].ToString());
                            model.RespuestaAjax.Mensaje = reader["Mensaje"].ToString();

                            if (model.RespuestaAjax.Success)
                            {
                                model.Notas = reader["Notas"].ToString();
                            }
                            else
                            {
                                throw new Exception(model.RespuestaAjax.Mensaje);
                            }
                        }
                    }
                    reader.Close();
                }
            }
        }

        #endregion

        #region Proveedor Contactos

        public CatContactosModels ObtenerDetalleCatDatosXProveedor(CatContactosModels datos)
        {
            try
            {
                object[] parametros = { datos.IDProveedor, datos.IDContacto };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_DatosContactoXProveedorXID", parametros);
                while (dr.Read())
                {
                    datos.IDContacto = dr["id_contacto"].ToString();

                    datos.nombreContacto = dr["nombreContacto"].ToString();
                    datos.apMaterno = dr["apellidomaterno"].ToString();
                    datos.apPaterno = dr["apellidoPaterno"].ToString();

                    datos.telefonoContacto = dr["telefonoContacto"].ToString();
                    datos.correo = dr["correoElectronico"].ToString();
                    datos.celularContacto = dr["celularContacto"].ToString();
                    datos.direccion = dr["direccion"].ToString();

                    datos.observacion = !dr.IsDBNull(dr.GetOrdinal("observacion")) ? dr.GetString(dr.GetOrdinal("observacion")) : string.Empty;
                }
                dr.Close();
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatContactosModels AcContactoProveedor(CatContactosModels datos)
        {
            try
            {
                object[] parametros =
                {
                   datos.Opcion,
                   datos.IDProveedor ?? string.Empty,
                   datos.IDContacto ?? string.Empty,
                   datos.IDSucursal ?? string.Empty,
                   datos.nombreContacto ?? string.Empty,
                   datos.apPaterno ?? string.Empty,
                   datos.apMaterno?? string.Empty,
                   datos.correo ?? string.Empty,
                   datos.celularContacto ?? string.Empty,
                   datos.telefonoContacto ?? string.Empty,
                   datos.observacion ?? string.Empty,
                    datos.direccion ?? string.Empty,
                   datos.Usuario ?? string.Empty

                    };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatContactoXproveedor", parametros);
                datos.IDContacto = aux.ToString();

                if (!string.IsNullOrEmpty(datos.IDContacto))
                {
                    datos.Completado = true;
                }
                else
                {
                    datos.Completado = false;
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CatContactosModels> obtenerDatosContacto(CatProveedorModels datos)
        {
            try
            {
                List<CatContactosModels> lista = new List<CatContactosModels>();
                CatContactosModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_DatosContactoXProveedor", datos.IDProveedor);
                while (dr.Read())
                {
                    item = new CatContactosModels();

                    item.IDContacto = dr["id_contacto"].ToString();
                    item.nombreContacto = dr["nombre"].ToString();
                    item.correo = dr["correoElectronico"].ToString();
                    item.telefonoContacto = dr["telefonoContacto"].ToString();
                    item.celularContacto = !dr.IsDBNull(dr.GetOrdinal("celularContacto")) ? dr.GetString(dr.GetOrdinal("celularContacto")) : string.Empty;
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
        public CatContactosModels EliminarProveedorDatosContacto(CatContactosModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDContacto, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_shared_del_CatContactoXID", parametros);
                if (aux != null)
                {
                    int Resultado = 0;
                    int.TryParse(aux.ToString(), out Resultado);
                    if (Resultado == 1)
                    {
                        datos.Completado = true;
                    }
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Proveedor

        public CatProveedorModels AcCatProveedor(CatProveedorModels datos)
        {
            try
            {
                object[] parametros =
                {
                   datos.Opcion,
                    datos.IDProveedor ?? string.Empty,
                    datos.IDTipoProveedor,
                    datos.IDSucursal ?? string.Empty,
                    datos.NombreRazonSocial ?? string.Empty,
                    datos.RFC ?? string.Empty,
                    datos.ImgINE ?? string.Empty,
                    datos.ImgManifestacionFierro ?? string.Empty,
                    datos.BandINE,
                    datos.BandMF,
                    datos.Direccion ?? string.Empty,
                    datos.telefonoCelular ?? string.Empty,
                    datos.telefonoCasa ?? string.Empty,
                    datos.correo ?? string.Empty,
                    datos.sexo,
                    datos.FechaIngreso != null ? datos.FechaIngreso : DateTime.Today,
                    datos.EsEmpresa,
                    datos.Tolerancia,
                    //datos.merma,
                    datos.Observaciones ?? string.Empty,
                    datos.CantidadPeriodo,
                    datos.IDPeriodo,
                    datos.TodaSucursale,
                    datos.Usuario ?? string.Empty
                    };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatProveedor", parametros);
                datos.IDProveedor = aux.ToString();

                if (!string.IsNullOrEmpty(datos.IDProveedor))
                {
                    datos.Completado = true;
                }
                else
                {
                    datos.Completado = false;
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatProveedorModels EliminarProveedor(CatProveedorModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDProveedor, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatProveedor", parametros);
                datos.IDProveedor = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDProveedor))
                {
                    datos.Completado = true;
                }
                else
                {
                    datos.Completado = false;
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public CatProveedorModels ObtenerDetalleCatProveedor(CatProveedorModels datos)
        {
            try
            {
                object[] parametros = { datos.IDProveedor };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatProveedorXID", parametros);
                while (dr.Read())
                {
                    datos.IDProveedor = dr["id_proveedor"].ToString();
                    datos.IDTipoProveedor =Convert.ToInt32(dr["id_tipoProveedor"].ToString());
                    datos.IDSucursal = dr["id_sucursal"].ToString();
                    datos.NombreRazonSocial = dr["nombreRazonSocial"].ToString();
                    datos.RFC = dr["rfc"].ToString();
                    datos.ImgINE = dr["imgINE"].ToString();
                    datos.ImgManifestacionFierro = dr["imgManifestacionFierro"].ToString();
                    datos.Direccion = dr["direccion"].ToString();
                    datos.correo = dr["correo"].ToString();
                    datos.telefonoCasa = dr["telefonoCasa"].ToString();
                    datos.telefonoCelular = dr["telefonoCelular"].ToString();
                    datos.sexo = Convert.ToInt32(dr["sexo"].ToString());
                    datos.FechaIngreso = !dr.IsDBNull(dr.GetOrdinal("FechaIngreso")) ? dr.GetDateTime(dr.GetOrdinal("FechaIngreso")) : DateTime.Now;
                    datos.EsEmpresa = !dr.IsDBNull(dr.GetOrdinal("esEmpresa")) ? dr.GetBoolean(dr.GetOrdinal("esEmpresa")) : false;
                    datos.Tolerancia = !dr.IsDBNull(dr.GetOrdinal("tolerancia")) ? dr.GetDecimal(dr.GetOrdinal("tolerancia")) : 0;
                    datos.Observaciones = !dr.IsDBNull(dr.GetOrdinal("observaciones")) ? dr.GetString(dr.GetOrdinal("observaciones")) : string.Empty;
                    datos.CantidadPeriodo = !dr.IsDBNull(dr.GetOrdinal("cantidadPeriodo")) ? dr.GetInt32(dr.GetOrdinal("cantidadPeriodo")) : 0;
                    datos.IDPeriodo = !dr.IsDBNull(dr.GetOrdinal("id_periodo")) ? dr.GetInt32(dr.GetOrdinal("id_periodo")) : 0;
                    datos.FotoPerfil = !dr.IsDBNull(dr.GetOrdinal("UrlFotoPerfil")) ? dr.GetString(dr.GetOrdinal("UrlFotoPerfil")) : string.Empty;
                }
                dr.Close();
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        //LISTA DE PROVEEDORES MOSTRADA EN EL INDEX DE 'CatProveedor'
        public string ObtenerCatProveedores(CatProveedorModels Datos, DataTableAjaxPostModel dataTableAjaxPostModel)
        {
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(Datos.Conexion))
                {
                    using (SqlCommand cmd = new SqlCommand("spCSLDB_Catalogo_get_CatProveedores", sqlcon))
                    {
                        //parametros de entrada
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Start", SqlDbType.Int).Value = dataTableAjaxPostModel.start;
                        cmd.Parameters.Add("@Length", SqlDbType.Int).Value = dataTableAjaxPostModel.length;
                        cmd.Parameters.Add("@SearchValue", SqlDbType.NVarChar).Value = dataTableAjaxPostModel.search.value;
                        cmd.Parameters.Add("@Draw", SqlDbType.Int).Value = dataTableAjaxPostModel.draw;
                        cmd.Parameters.Add("@ColumnNumber", SqlDbType.Int).Value = dataTableAjaxPostModel.order[0].column;
                        cmd.Parameters.Add("@ColumnDir", SqlDbType.NVarChar).Value = dataTableAjaxPostModel.order[0].dir;

                        // execute
                        sqlcon.Open();

                        SqlDataReader reader = cmd.ExecuteReader();

                        //datatable = Auxiliar.SqlReaderToJson(reader);

                        var indexDatatableDto = new IndexDatatableDto();

                        if (reader.HasRows)
                        {
                            indexDatatableDto.Data = new List<object>();
                            bool firstData = true;
                            IFormatProvider culture = new CultureInfo("es-MX", true);

                            while (reader.Read())
                            {
                                if (firstData)
                                {
                                    indexDatatableDto.Draw = int.Parse(reader["Draw"].ToString()); ;
                                    indexDatatableDto.RecordsFiltered = int.Parse(reader["RecordsFiltered"].ToString());
                                    indexDatatableDto.RecordsTotal = int.Parse(reader["RecordsTotal"].ToString());
                                    firstData = false;
                                }

                                var indexDto = new IndexProveedorGanadoDto();

                                indexDto.DT_RowId = reader["DT_RowId"].ToString();
                                indexDto.IdProveedor = reader["IdProveedor"].ToString();
                                indexDto.NombreRazonSocial = reader["NombreRazonSocial"].ToString();
                                indexDto.IdSucursal = reader["IdSucursal"].ToString();
                                indexDto.TipoProveedor = reader["TipoProveedor"].ToString();
                                indexDto.NombreSucursal = reader["NombreSucursal"].ToString();
                                indexDto.Tolerancia = decimal.Parse(reader["Tolerancia"].ToString());

                                indexDatatableDto.Data.Add(indexDto);
                            }
                        }

                        var json = JsonConvert.SerializeObject(indexDatatableDto);

                        reader.Close();

                        return json;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<CatSucursalesModels> obtenerListaSucursales(CatProveedorModels Datos)
        {
            try
            {
                List<CatSucursalesModels> lista = new List<CatSucursalesModels>();
                CatSucursalesModels item;
                SqlDataReader dr = null;
                lista.Add(new CatSucursalesModels { IDSucursal = string.Empty, NombreSucursal = " - Seleccione -" });
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_get_ComboSucursales");
                while (dr.Read())
                {
                    item = new CatSucursalesModels();
                    item.IDSucursal = dr["id_sucursal"].ToString();
                    item.NombreSucursal = dr["nombre"].ToString();

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
        public List<CatTipoProveedorModels> obtenerListaTipoProveedor(CatProveedorModels Datos)
        {
            try
            {
                List<CatTipoProveedorModels> lista = new List<CatTipoProveedorModels>();
                CatTipoProveedorModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_TipoProveedor");
                while (dr.Read())
                {
                    item = new CatTipoProveedorModels();
                    item.IDTipoProveedor = Convert.ToInt32(dr["id_tipoProveedor"].ToString());
                    item.Descripcion = dr["nombre"].ToString();

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
        public List<CatGeneroModels> ObteneComboCatGenero(CatProveedorModels Datos)
        {
            try
            {
                List<CatGeneroModels> lista = new List<CatGeneroModels>();
                CatGeneroModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatGenero");
                // lista.Add(new CatGeneroModels { IDGenero = string.Empty, NombreSucursal = " - Seleccione -" });
                while (dr.Read())
                {
                    item = new CatGeneroModels();
                    item.IDGenero = !dr.IsDBNull(dr.GetOrdinal("IDGenero")) ? dr.GetInt32(dr.GetOrdinal("IDGenero")) : 0;
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

        public List<CatPeriodoModels> ObteneComboCatPeriodo(CatProveedorModels Datos)
        {
            try
            {
                List<CatPeriodoModels> lista = new List<CatPeriodoModels>();
                CatPeriodoModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatPeriodo");
                while (dr.Read())
                {
                    item = new CatPeriodoModels();
                    item.IDPeriodo = !dr.IsDBNull(dr.GetOrdinal("IDPeriodo")) ? dr.GetInt32(dr.GetOrdinal("IDPeriodo")) : 0;
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

        #endregion

        #region Cuentas Bancarias Por Provedor

        public List<CuentaBancariaProveedorModels> ObtenerCuentasBancarias(CuentaBancariaProveedorModels Datos)
        {
            try
            {
                List<CuentaBancariaProveedorModels> lista = new List<CuentaBancariaProveedorModels>();
                CuentaBancariaProveedorModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Catalogo_get_CatProveedoresDatosBancarios", Datos.IDProveedor);
                while (dr.Read())
                {
                    item = new CuentaBancariaProveedorModels();
                    item.IDDatosBancarios = !dr.IsDBNull(dr.GetOrdinal("IDDatosBancarios")) ? dr.GetString(dr.GetOrdinal("IDDatosBancarios")) : string.Empty;
                    item.Banco.Descripcion = !dr.IsDBNull(dr.GetOrdinal("NombreBanco")) ? dr.GetString(dr.GetOrdinal("NombreBanco")) : string.Empty;
                    item.Titular = !dr.IsDBNull(dr.GetOrdinal("NombreTitular")) ? dr.GetString(dr.GetOrdinal("NombreTitular")) : string.Empty;
                    item.NumTarjeta = !dr.IsDBNull(dr.GetOrdinal("NumeroTarjeta")) ? dr.GetString(dr.GetOrdinal("NumeroTarjeta")) : string.Empty;
                    item.NumCuenta = !dr.IsDBNull(dr.GetOrdinal("NumeroCuenta")) ? dr.GetString(dr.GetOrdinal("NumeroCuenta")) : string.Empty;
                    item.Clabe = !dr.IsDBNull(dr.GetOrdinal("ClaveInterbancaria")) ? dr.GetString(dr.GetOrdinal("ClaveInterbancaria")) : string.Empty;
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

        public List<CatBancoModels> ObteneComboCatBancos(CuentaBancariaProveedorModels Datos)
        {
            try
            {
                List<CatBancoModels> lista = new List<CatBancoModels>();
                CatBancoModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_combo_get_CatBancos");
                while (dr.Read())
                {
                    item = new CatBancoModels();
                    item.IDBanco = !dr.IsDBNull(dr.GetOrdinal("IDBanco")) ? dr.GetInt32(dr.GetOrdinal("IDBanco")) : 0;
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

        public void ACDatosBancariosProveedor(CuentaBancariaProveedorModels datos)
        {
            try
            {
                object[] parametros = { datos.Opcion,
                                        datos.IDDatosBancarios ?? string.Empty,
                                        datos.IDProveedor ?? string.Empty,
                                        datos.IDBanco,
                                        datos.Titular ?? string.Empty,
                                        datos.NumCuenta ?? string.Empty,
                                        datos.NumTarjeta ?? string.Empty,
                                        datos.Clabe ?? string.Empty,
                                        datos.Usuario ?? string.Empty};
                object result = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogos_ac_DatosBancariosProveedor", parametros);
                if (result != null)
                {
                    if (!string.IsNullOrEmpty(result.ToString()))
                    {
                        datos.Completado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CuentaBancariaProveedorModels ObtenerDetalleCuentaBancaria(CuentaBancariaProveedorModels datos)
        {
            try
            {
                object[] parametros = { datos.IDDatosBancarios, datos.IDProveedor };
                SqlDataReader Dr = null;
                Dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatProveedorDatosBancoXID", parametros);
                while (Dr.Read())
                {
                    datos.IDBanco = !Dr.IsDBNull(Dr.GetOrdinal("Banco")) ? Dr.GetInt32(Dr.GetOrdinal("Banco")) : -1;
                    datos.Titular = !Dr.IsDBNull(Dr.GetOrdinal("Titular")) ? Dr.GetString(Dr.GetOrdinal("Titular")) : string.Empty;
                    datos.NumTarjeta = !Dr.IsDBNull(Dr.GetOrdinal("NumTarjeta")) ? Dr.GetString(Dr.GetOrdinal("NumTarjeta")) : string.Empty;
                    datos.NumCuenta = !Dr.IsDBNull(Dr.GetOrdinal("NumCuenta")) ? Dr.GetString(Dr.GetOrdinal("NumCuenta")) : string.Empty;
                    datos.Clabe = !Dr.IsDBNull(Dr.GetOrdinal("Clabe")) ? Dr.GetString(Dr.GetOrdinal("Clabe")) : string.Empty;
                    datos.Completado = true;
                }
                Dr.Close();
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CuentaBancariaProveedorModels EliminarDatosBancariosProveedor(CuentaBancariaProveedorModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDDatosBancarios, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_shared_del_CuentaBancariaXID", parametros);
                if (aux != null)
                {
                    int Resultado = 0;
                    int.TryParse(aux.ToString(), out Resultado);
                    if (Resultado == 1)
                    {
                        datos.Completado = true;
                    }
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Lugar Por Proveedor

        public List<ProveedorLugarModels> ObtenerLugaresProveedor(ProveedorLugarModels Datos)
        {
            try
            {
                List<ProveedorLugarModels> lista = new List<ProveedorLugarModels>();
                ProveedorLugarModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Catalogo_get_CatProveedoresLugar", Datos.IDProveedor);
                while (dr.Read())
                {
                    item = new ProveedorLugarModels();
                    item.IDProveedorLugar = !dr.IsDBNull(dr.GetOrdinal("IDProveedorLugar")) ? dr.GetString(dr.GetOrdinal("IDProveedorLugar")) : string.Empty;
                    item.NombreLugar = !dr.IsDBNull(dr.GetOrdinal("NombreLugar")) ? dr.GetString(dr.GetOrdinal("NombreLugar")) : string.Empty;
                    item.Bascula = !dr.IsDBNull(dr.GetOrdinal("Bascula")) ? dr.GetBoolean(dr.GetOrdinal("Bascula")) : false;
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
       
        public List<CatLugarModels> obtenerComboLugares(ProveedorLugarModels Datos)
        {
            try
            {
                List<CatLugarModels> lista = new List<CatLugarModels>();
                CatLugarModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatLugar", Datos.IDSucursal, Datos.IDProveedor);
                while (dr.Read())
                {
                    item = new CatLugarModels();
                    item.id_lugar = dr["IDLugar"].ToString();
                    item.descripcion = dr["NombreLugar"].ToString();

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

        public void ACProveedorLugares(ProveedorLugarModels datos)
        {
            try
            {
                object[] parametros = { datos.IDProveedorLugar ?? string.Empty,
                                        datos.IDProveedor ?? string.Empty,
                                        datos.IDLugar ?? string.Empty,
                                        datos.Usuario ?? string.Empty};
                object result = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_ProveedorLugar", parametros);
                if (result != null)
                {
                    if (!string.IsNullOrEmpty(result.ToString()))
                    {
                        datos.Completado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProveedorLugarModels EliminarProveedorLugar(ProveedorLugarModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDProveedorLugar, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_ProveedorLugar", parametros);
                if (aux != null)
                {
                    int Resultado = 0;
                    int.TryParse(aux.ToString(), out Resultado);
                    if (Resultado == 1)
                    {
                        datos.Completado = true;
                    }
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Precio Por Rango Peso Proveedor

        public List<RangoPrecioProveedorModels> ObtenerPrecioXRangoPesoProveedor(RangoPrecioProveedorModels Datos)
        {
            try
            {
                List<RangoPrecioProveedorModels> lista = new List<RangoPrecioProveedorModels>();
                RangoPrecioProveedorModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Catalogo_get_CatProveedoresXRangoPrecio", Datos.IDProveedor);
                while (dr.Read())
                {
                    item = new RangoPrecioProveedorModels();
                    item.IDRango = !dr.IsDBNull(dr.GetOrdinal("IDRango")) ? dr.GetInt16(dr.GetOrdinal("IDRango")) : 0;
                    item.PesoMinimo = !dr.IsDBNull(dr.GetOrdinal("PesoMinimo")) ? dr.GetDecimal(dr.GetOrdinal("PesoMinimo")) : 0;
                    item.PesoMaximo = !dr.IsDBNull(dr.GetOrdinal("PesoMaximo")) ? dr.GetDecimal(dr.GetOrdinal("PesoMaximo")) : 0;
                    item.EsMacho = !dr.IsDBNull(dr.GetOrdinal("EsMacho")) ? dr.GetBoolean(dr.GetOrdinal("EsMacho")) : false;
                    item.Precio = !dr.IsDBNull(dr.GetOrdinal("Precio")) ? dr.GetDecimal(dr.GetOrdinal("Precio")) : 0;
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

        public RangoPrecioProveedorModels ObtenerDetallePrecioXProveedor(RangoPrecioProveedorModels datos)
        {
            try
            {
                object[] parametros = { datos.IDProveedor, datos.IDRango };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatProveedoresXRangoPrecioXID", parametros);
                while (dr.Read())
                {
                    datos.IDRango = !dr.IsDBNull(dr.GetOrdinal("IDRango")) ? dr.GetInt16(dr.GetOrdinal("IDRango")) : 0;
                    datos.PesoMinimo = !dr.IsDBNull(dr.GetOrdinal("PesoMinimo")) ? dr.GetDecimal(dr.GetOrdinal("PesoMinimo")) : 0;
                    datos.PesoMaximo = !dr.IsDBNull(dr.GetOrdinal("PesoMaximo")) ? dr.GetDecimal(dr.GetOrdinal("PesoMaximo")) : 0;
                    datos.EsMacho = !dr.IsDBNull(dr.GetOrdinal("EsMacho")) ? dr.GetBoolean(dr.GetOrdinal("EsMacho")) : false;
                    datos.Precio = !dr.IsDBNull(dr.GetOrdinal("Precio")) ? dr.GetDecimal(dr.GetOrdinal("Precio")) : 0;
                    datos.Completado = true;
                }
                dr.Close();
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ACPrecioPoRangoPesoProveedor(RangoPrecioProveedorModels datos)
        {
            try
            {
                object[] parametros = { 
                                        datos.IDProveedor ?? string.Empty,
                                        datos.IDRango,
                                        datos.Precio,
                                        datos.Usuario ?? string.Empty};
                object result = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_a_RangoPesoProveedorPrecio", parametros);
                if (result != null)
                {
                    int Resultado = 0;
                    int.TryParse(result.ToString(), out Resultado);
                    if (Resultado == 1 || Resultado == 2)
                    {
                        datos.Completado = true;
                        datos.Resultado = Resultado;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region UPPProveedor
        public UPPProvedorModels ObtenerUPPProveedor(UPPProvedorModels Datos)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_get_UPPProveedor", Datos.id_proveedor);
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Datos.UPP = !dr.IsDBNull(dr.GetOrdinal("UPP")) ? dr.GetString(dr.GetOrdinal("UPP")) : string.Empty;
                        Datos.fechaAlta = !dr.IsDBNull(dr.GetOrdinal("fechaAlta")) ? dr.GetDateTime(dr.GetOrdinal("fechaAlta")) : DateTime.Today;
                        Datos.id_pais = !dr.IsDBNull(dr.GetOrdinal("IDPais")) ? dr.GetString(dr.GetOrdinal("IDPais")) : string.Empty;
                        Datos.id_estadoCodigo = !dr.IsDBNull(dr.GetOrdinal("C_Estado")) ? dr.GetString(dr.GetOrdinal("C_Estado")) : string.Empty;
                        Datos.id_estado = !dr.IsDBNull(dr.GetOrdinal("IDEstado")) ? dr.GetInt32(dr.GetOrdinal("IDEstado")) : 0;
                        Datos.id_municipio = !dr.IsDBNull(dr.GetOrdinal("IDMunicipio")) ? dr.GetInt32(dr.GetOrdinal("IDMunicipio")) : 0;
                        Datos.nombrePredio = !dr.IsDBNull(dr.GetOrdinal("nombrePredio")) ? dr.GetString(dr.GetOrdinal("nombrePredio")) : string.Empty;
                        Datos.propietario = !dr.IsDBNull(dr.GetOrdinal("Propietario")) ? dr.GetString(dr.GetOrdinal("Propietario")) : string.Empty;
                        Datos.Imagen = !dr.IsDBNull(dr.GetOrdinal("imagenUPP")) ? dr.GetString(dr.GetOrdinal("imagenUPP")) : string.Empty;
                        Datos.ImagenServer = !dr.IsDBNull(dr.GetOrdinal("imagenServer")) ? dr.GetInt32(dr.GetOrdinal("imagenServer")) : 0;
                    }
                }
                else
                {
                    Datos.fechaAlta = new DateTime();
                    Datos.fechaAlta = DateTime.Today;
                }
                dr.Close();
                return Datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UPPProvedorModels CUPPProveedor(UPPProvedorModels Datos)
        {
            try
            {
                object[] parametros =
                {
                   Datos.Opcion,
                   Datos.id_proveedor,
                   Datos.UPP,
                   Datos.fechaAlta,
                   Datos.id_pais,
                   Datos.id_estadoCodigo,
                   Datos.id_municipio,
                   Datos.nombrePredio,
                   Datos.propietario,
                   Datos.Imagen,
                   Datos.BandImg,
                   Datos.Usuario
                    };
                object aux = SqlHelper.ExecuteScalar(Datos.Conexion, "spCSLDB_ac_UPPProveedor", parametros);
                Datos.id_proveedor = aux.ToString();
                int completo = 0;
                int.TryParse(Datos.id_proveedor.ToString(), out completo);
                if (completo==1)
                {
                    Datos.Completado = true;
                }
                else
                {
                    Datos.Completado = false;
                }
                return Datos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<CatPaisModels> obtenerListaPaises(UPPProvedorModels Datos)
        {
            try
            {
                List<CatPaisModels> lista = new List<CatPaisModels>();
                CatPaisModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_get_ComboPaises");
                while (dr.Read())
                {
                    item = new CatPaisModels();
                    item.id_pais = dr["clave"].ToString();
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
        public List<CatEstadoModels> obtenerListaEstados(UPPProvedorModels Datos)
        {
            try
            {
                List<CatEstadoModels> lista = new List<CatEstadoModels>();
                CatEstadoModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_get_ComboEstado", Datos.id_pais);
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
        public List<CatMunicipioModels> obtenerListaMunicipios(UPPProvedorModels Datos)
        {
            try
            {
                List<CatMunicipioModels> lista = new List<CatMunicipioModels>();
                CatMunicipioModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_get_ComboMunicipio", Datos.id_pais, Datos.id_estadoCodigo);
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
        #endregion

        #region Proveedor Foto Perfil
        public RespuestaAjax ActualizarFotoPerfil(string idProveedor, string idUsuario, string urlFotoPerfil, string conexion)
        {
            var respuesta = new RespuestaAjax();
            try
            {
                object[] parametros =
                {
                    idProveedor, idUsuario, urlFotoPerfil
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "spCIDDB_Catalogo_a_CatProveedor_fotoPerfil", parametros);


                while (dr.Read())
                {
                    respuesta.Success = !dr.IsDBNull(dr.GetOrdinal("Success"))
                        ? dr.GetBoolean(dr.GetOrdinal("Success"))
                        : false;
                    respuesta.Mensaje = !dr.IsDBNull(dr.GetOrdinal("Mensaje"))
                        ? dr.GetString(dr.GetOrdinal("Mensaje"))
                        : string.Empty;
                    respuesta.MensajeErrorSQL = !dr.IsDBNull(dr.GetOrdinal("MensajeErrorSQL"))
                        ? dr.GetString(dr.GetOrdinal("MensajeErrorSQL"))
                        : string.Empty;
                }

                dr.Close();

             
            }
            catch (Exception ex)
            {
                respuesta.Mensaje = ex.Message;
                respuesta.Success = false;
            }
            
            return respuesta;
        }
        #endregion

        #region Documentos Extras

        public string DocumentosExtras_ObtenerIndex(DataTableAjaxPostModel dataTableAjaxPostModel)
        {
            try
            {
                using (var sqlcon = new SqlConnection(ConexionSql))
                {
                    using (var cmd = new SqlCommand("spCSLDB_Catalogo_DocumentacionExtra_CatProveedores_ObtenerIndex", sqlcon))
                    {
                        //parametros de entrada
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@Start", SqlDbType.Int).Value = dataTableAjaxPostModel.start;
                        cmd.Parameters.Add("@Length", SqlDbType.Int).Value = dataTableAjaxPostModel.length;
                        cmd.Parameters.Add("@SearchValue", SqlDbType.NVarChar).Value = dataTableAjaxPostModel.search.value;
                        cmd.Parameters.Add("@Draw", SqlDbType.Int).Value = dataTableAjaxPostModel.draw;
                        cmd.Parameters.Add("@ColumnNumber", SqlDbType.Int).Value = dataTableAjaxPostModel.order[0].column;
                        cmd.Parameters.Add("@ColumnDir", SqlDbType.NVarChar).Value = dataTableAjaxPostModel.order[0].dir;

                        // execute
                        sqlcon.Open();

                        var reader = cmd.ExecuteReader();

                        var indexDatatableDto = new IndexDatatableDto();

                        if (reader.HasRows)
                        {
                            indexDatatableDto.Data = new List<object>();
                            var firstData = true;

                            while (reader.Read())
                            {
                                if (firstData)
                                {
                                    indexDatatableDto.Draw = int.Parse(reader["Draw"].ToString()); ;
                                    indexDatatableDto.RecordsFiltered = int.Parse(reader["RecordsFiltered"].ToString());
                                    indexDatatableDto.RecordsTotal = int.Parse(reader["RecordsTotal"].ToString());
                                    firstData = false;
                                }

                                var indexDto = new IndexProveedorGanadoDocumentacionExtraDto();

                                indexDto.IdDocumentacionExtra = reader["IdDocumentacionExtra"].ToString();
                                indexDto.IdTipoDocumentacionExtra = int.Parse(reader["IdTipoDocumentacionExtra"].ToString());
                                indexDto.NombreTipoDocumentacionExtra = reader["NombreTipoDocumentacionExtra"].ToString();
                                indexDto.IdProveedor = reader["IdProveedor"].ToString();
                                indexDto.UrlArchivo = ProjectSettings.BaseDirProveedorDocumentacionExtra + reader["UrlArchivo"];

                                indexDatatableDto.Data.Add(indexDto);
                            }
                        }

                        var json = JsonConvert.SerializeObject(indexDatatableDto);

                        reader.Close();

                        return json;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public RespuestaAjax GuardarDocumentoExtra(DocumentacionExtra_CatProveedorModel model)
        {
            var respuesta = new RespuestaAjax();
            try
            {
                using (var sqlcon = new SqlConnection(ConexionSql))
                {
                    using (var cmd = new SqlCommand("spCSLDB_DocumentacionExtra_CatProveedores_GuardarArchivo",
                        sqlcon))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@UrlArchivo", SqlDbType.NVarChar).Value = model.Archivo;
                        cmd.Parameters.Add("@IdProveedor", SqlDbType.Char).Value = model.IdProveedor;
                        cmd.Parameters.Add("@IdTipoDocumentacionExtra", SqlDbType.Int).Value =
                            model.IdTipoDocumentacionExtra;
                        cmd.Parameters.Add("@IdUsuario", SqlDbType.Char).Value = UsuarioActual;
                        cmd.Parameters.Add("@IdDocumentacionExtra", SqlDbType.Char).Value = model.IdDocumentacionExtra;
                        

                        sqlcon.Open();

                        var reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                respuesta.Success = (bool) reader["Success"];
                                respuesta.Mensaje = reader["Mensaje"].ToString();
                                if (!respuesta.Success)
                                    respuesta.MensajeErrorSQL = reader["MensejeErrorSQL"].ToString();
                            }
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception e)
            {
                respuesta.Success = false;
                respuesta.MensajeErrorSQL = e.Message;
                respuesta.Mensaje =
                    "Se ha producido un error al guardar el documento, intentelo más tarde o contacte con soporte técnico.";
            }

            return respuesta;
        }

        public DocumentacionExtra_CatProveedorModel ObtenerDocumentacionExtra(string idProveedor, string idDocumentacionExtra)
        {
            var model = new DocumentacionExtra_CatProveedorModel();

            using (var sqlcon = new SqlConnection(ConexionSql))
            {
                using (var cmd = new SqlCommand("spCIDDB_DocumentacionExtra_CatProveedorspCSLDB_DocumentacionExtra_CatProveedores_ObtenerArchivo",
                    sqlcon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@IdProveedor", SqlDbType.Char).Value = idProveedor;
                    cmd.Parameters.Add("@IdDocumentacionExtra", SqlDbType.Char).Value = idDocumentacionExtra;
                    
                    sqlcon.Open();

                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            model.IdTipoDocumentacionExtra = int.Parse(reader["IdTipoDocumentacionExtra"].ToString());
                            model.Archivo = ProjectSettings.BaseDirProveedorDocumentacionExtra  + reader["UrlArchivo"];
                            model.IdProveedor = reader["IdProveedor"].ToString();
                            model.IdDocumentacionExtra = reader["IdDocumentacionExtra"].ToString();
                        }
                    }

                    reader.Close();
                }
            }

            return model;
        }


        public RespuestaAjax EliminarDocumentoExtra(string idProveedor, string idDocumentacionExtra, string urlArchivo)
        {
            var respuesta = new RespuestaAjax();
            try
            {
                using (var sqlcon = new SqlConnection(ConexionSql))
                {
                    using (var cmd = new SqlCommand("spCSLDB_DocumentacionExtra_CatProveedores_EliminarDocumentacionExtra",
                        sqlcon))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@IdProveedor", SqlDbType.Char).Value = idProveedor;
                        cmd.Parameters.Add("@IdDocumentacionExtra", SqlDbType.Char).Value = idDocumentacionExtra;
                        cmd.Parameters.Add("@UrlArchivo", SqlDbType.NVarChar).Value = urlArchivo;


                        sqlcon.Open();

                        var reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                respuesta.Success = (bool)reader["Success"];
                                respuesta.Mensaje = reader["Mensaje"].ToString();
                                if (!respuesta.Success)
                                    respuesta.MensajeErrorSQL = reader["MensejeErrorSQL"].ToString();
                            }
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception e)
            {
                respuesta.Success = false;
                respuesta.MensajeErrorSQL = e.Message;
                respuesta.Mensaje =
                    "Se ha producido un error al guardar el documento, intentelo más tarde o contacte con soporte técnico.";
            }

            return respuesta;
        }
        #endregion
    }
}