using CreativaSL.Web.Ganados.ViewModels;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class CatEmpleado_Datos
    {

        #region CatEmpleado

        public List<CatEmpleadoModels> ObtenerCatEmpleado(CatEmpleadoModels datos)
        {
            try
            {
                List<CatEmpleadoModels> Lista = new List<CatEmpleadoModels>();
                CatEmpleadoModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatEmpleado");
                while (dr.Read())
                {
                    Item = new CatEmpleadoModels();
                    Item.IDEmpleado = !dr.IsDBNull(dr.GetOrdinal("IDEmpleado")) ? dr.GetString(dr.GetOrdinal("IDEmpleado")) : string.Empty;
                    Item.CodigoUsuario = !dr.IsDBNull(dr.GetOrdinal("CodigoUsuario")) ? dr.GetString(dr.GetOrdinal("CodigoUsuario")) : string.Empty;
                    Item.NombreCompleto = !dr.IsDBNull(dr.GetOrdinal("NombreCompleto")) ? dr.GetString(dr.GetOrdinal("NombreCompleto")) : string.Empty;
                    Item.AltaNominal = !dr.IsDBNull(dr.GetOrdinal("AltaNomina")) ? dr.GetBoolean(dr.GetOrdinal("AltaNomina")) : false;
                    Item.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NombreSucursal")) ? dr.GetString(dr.GetOrdinal("NombreSucursal")) : string.Empty;
                    Item.NombrePuesto = !dr.IsDBNull(dr.GetOrdinal("NombrePuesto")) ? dr.GetString(dr.GetOrdinal("NombrePuesto")) : string.Empty;
                    Item.NombreCategoriaP = !dr.IsDBNull(dr.GetOrdinal("CategoriaPuesto")) ? dr.GetString(dr.GetOrdinal("CategoriaPuesto")) : string.Empty;
                    Item.GrupoSanguineo = !dr.IsDBNull(dr.GetOrdinal("GrupoSanguineo")) ? dr.GetString(dr.GetOrdinal("GrupoSanguineo")) : string.Empty;
                    Item.DirCalle = !dr.IsDBNull(dr.GetOrdinal("Direccion")) ? dr.GetString(dr.GetOrdinal("Direccion")) : string.Empty;
                    Item.TieneCuenta = !dr.IsDBNull(dr.GetOrdinal("TieneCuenta")) ? dr.GetBoolean(dr.GetOrdinal("TieneCuenta")) : false;
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

        public CatEmpleadoModels AbcCatEmpleado(CatEmpleadoModels datos)
        {
            try
            {
                object[] parametros = {
                    datos.Opcion,
                    datos.IDEmpleado ?? string.Empty,
                    datos.IDTipoUsuario,
                    datos.AbrirCaja,
                    datos.CodigoUsuario ?? string.Empty,
                    datos.Nombre ?? string.Empty,
                    datos.ApellidoPat ?? string.Empty,
                    datos.ApellidoMat ?? string.Empty,
                    datos.AltaNominal,
                    datos.IDPuesto,
                    datos.IDCategoriaPuesto ?? string.Empty,
                    datos.IDSucursalActual ?? string.Empty,
                    datos.DirCalle ?? string.Empty,
                    datos.DirColonia ?? string.Empty,
                    datos.DirNumero ?? string.Empty,
                    datos.Telefono ?? string.Empty,
                    datos.IDGrupoSanguineo,
                    datos.Usuario ?? string.Empty,
                    datos.FechaNacimiento,
                    datos.Licencia
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_ac_CatEmpleado", parametros);
                datos.IDEmpleado = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDEmpleado))
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

        public CatEmpleadoModels ObtenerDetalleCatEmpleado(CatEmpleadoModels datos)
        {
            try
            {
                object[] parametros = { datos.IDEmpleado };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Catalogo_get_CatEmpleadoXID", parametros);
                while (dr.Read())
                {
                    datos.IDEmpleado = !dr.IsDBNull(dr.GetOrdinal("IDEmpleado")) ? dr.GetString(dr.GetOrdinal("IDEmpleado")) : string.Empty;
                    datos.IDTipoUsuario = !dr.IsDBNull(dr.GetOrdinal("IDTipoUsuario")) ? dr.GetInt16(dr.GetOrdinal("IDTipoUsuario")) : 0;
                    datos.AbrirCaja = !dr.IsDBNull(dr.GetOrdinal("AbrirCaja")) ? dr.GetBoolean(dr.GetOrdinal("AbrirCaja")) : false;
                    datos.CodigoUsuario = !dr.IsDBNull(dr.GetOrdinal("CodigoUsuario")) ? dr.GetString(dr.GetOrdinal("CodigoUsuario")) : string.Empty;
                    datos.Nombre = !dr.IsDBNull(dr.GetOrdinal("Nombre")) ? dr.GetString(dr.GetOrdinal("Nombre")) : string.Empty;
                    datos.ApellidoPat = !dr.IsDBNull(dr.GetOrdinal("ApellidoPat")) ? dr.GetString(dr.GetOrdinal("ApellidoPat")) : string.Empty;
                    datos.ApellidoMat = !dr.IsDBNull(dr.GetOrdinal("ApellidoMat")) ? dr.GetString(dr.GetOrdinal("ApellidoMat")) : string.Empty;
                    datos.AltaNominal = !dr.IsDBNull(dr.GetOrdinal("AltaNomina")) ? dr.GetBoolean(dr.GetOrdinal("AltaNomina")) : false;
                    datos.IDPuesto = !dr.IsDBNull(dr.GetOrdinal("IDPuesto")) ? dr.GetInt32(dr.GetOrdinal("IDPuesto")) : 0;
                    datos.IDCategoriaPuesto = !dr.IsDBNull(dr.GetOrdinal("IDCategoriaPuesto")) ? dr.GetString(dr.GetOrdinal("IDCategoriaPuesto")) : string.Empty;
                    datos.IDSucursalActual = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty;
                    datos.DirCalle = !dr.IsDBNull(dr.GetOrdinal("DirCalle")) ? dr.GetString(dr.GetOrdinal("DirCalle")) : string.Empty;
                    datos.DirColonia = !dr.IsDBNull(dr.GetOrdinal("DirColonia")) ? dr.GetString(dr.GetOrdinal("DirColonia")) : string.Empty;
                    datos.DirNumero = !dr.IsDBNull(dr.GetOrdinal("DirNumero")) ? dr.GetString(dr.GetOrdinal("DirNumero")) : string.Empty;
                    datos.Telefono = !dr.IsDBNull(dr.GetOrdinal("Telefono")) ? dr.GetString(dr.GetOrdinal("Telefono")) : string.Empty;
                    datos.IDGrupoSanguineo = !dr.IsDBNull(dr.GetOrdinal("IDGrupoSanguineo")) ? dr.GetInt32(dr.GetOrdinal("IDGrupoSanguineo")) : 0;
                    datos.FechaNacimiento = !dr.IsDBNull(dr.GetOrdinal("fechaNacimiento")) ? dr.GetDateTime(dr.GetOrdinal("fechaNacimiento")) : DateTime.Today;
                    datos.Licencia = !dr.IsDBNull(dr.GetOrdinal("licencia")) ? dr.GetString(dr.GetOrdinal("licencia")) : string.Empty;
                }
                dr.Close();
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public CatEmpleadoModels EliminarEmpleado(CatEmpleadoModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDEmpleado, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Catalogo_del_CatEmpleado", parametros);
                datos.IDEmpleado = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDEmpleado))
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

        public List<CatSucursalesModels> ObteneComboCatSucursal(CatEmpleadoModels Datos)
        {
            try
            {
                List<CatSucursalesModels> lista = new List<CatSucursalesModels>();
                CatSucursalesModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatSucursal");
                lista.Add(new CatSucursalesModels { IDSucursal = string.Empty, NombreSucursal = " - Seleccione -" });
                while (dr.Read())
                {
                    item = new CatSucursalesModels();
                    item.IDSucursal = !dr.IsDBNull(dr.GetOrdinal("IDSucursal")) ? dr.GetString(dr.GetOrdinal("IDSucursal")) : string.Empty;
                    item.NombreSucursal = !dr.IsDBNull(dr.GetOrdinal("NombreSucursal")) ? dr.GetString(dr.GetOrdinal("NombreSucursal")) : string.Empty;
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

        public List<CatGrupoSanguineoModels> ObteneComboCatGrupoSanguineo(CatEmpleadoModels Datos)
        {
            try
            {
                List<CatGrupoSanguineoModels> lista = new List<CatGrupoSanguineoModels>();
                CatGrupoSanguineoModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatGrupoSanguineo");
                // lista.Add(new CatGeneroModels { IDGenero = string.Empty, NombreSucursal = " - Seleccione -" });
                while (dr.Read())
                {
                    item = new CatGrupoSanguineoModels();
                    item.IDGrupoSanguineo = !dr.IsDBNull(dr.GetOrdinal("id_grupoSanguineo")) ? dr.GetInt32(dr.GetOrdinal("id_grupoSanguineo")) : 0;
                    item.descripcion = !dr.IsDBNull(dr.GetOrdinal("TipoSanguineo")) ? dr.GetString(dr.GetOrdinal("TipoSanguineo")) : string.Empty;
                    lista.Add(item);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CatPuestoModels> obtenerComboCatPuesto(CatEmpleadoModels Datos)
        {
            try
            {
                List<CatPuestoModels> lista = new List<CatPuestoModels>();
                CatPuestoModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatPuestos");
                while (dr.Read())
                {
                    item = new CatPuestoModels();
                    item.IDPuesto = Convert.ToInt32(dr["IDPuesto"].ToString());
                    item.Descripcion = dr["Descripcion"].ToString();
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

        public List<CatCategoriaPuestoModels> ObteneComboCatCategoriaPuesto(CatEmpleadoModels Datos)
        {
            try
            {
                List<CatCategoriaPuestoModels> lista = new List<CatCategoriaPuestoModels>();
                CatCategoriaPuestoModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatCategoriaPuesto", Datos.IDPuesto);
                // lista.Add(new CatGeneroModels { IDGenero = string.Empty, NombreSucursal = " - Seleccione -" });
                while (dr.Read())
                {
                    item = new CatCategoriaPuestoModels();
                    item.id_categoria = !dr.IsDBNull(dr.GetOrdinal("IDCategoria")) ? dr.GetString(dr.GetOrdinal("IDCategoria")) : string.Empty;
                    item.descripcion = !dr.IsDBNull(dr.GetOrdinal("Descripcion")) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
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

        public string EMPLEADO_index_Archivo(string conexion, string id_empleado)
        {
            try
            {
                object[] parametros = { id_empleado };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "spCSLDB_EMPLEADO_index_Archivo", parametros);
                string datatable = Auxiliar.SqlReaderToJson(dr);

                dr.Close();
                return datatable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RespuestaAjax EMPLEADO_ac_Archivo(ArchivoEmpleadoModels item, string conexion, string usuario, int opcion)
        {
            try
            {
                object[] parametros =
                {
                    item.Id,
                    item.Id_empleado,
                    item.Descripcion,
                    item.UrlArchivo,
                    item.NombreArchivo,
                    usuario,
                    opcion
                };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "spCSLDB_EMPLEADO_ac_Archivo", parametros);
                RespuestaAjax respuesta = new RespuestaAjax();
                respuesta.Success = false;
                respuesta.Mensaje = "";
                while (dr.Read())
                {
                    respuesta.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    respuesta.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                    respuesta.Href = !dr.IsDBNull(dr.GetOrdinal("nombreEmpleado")) ? dr.GetString(dr.GetOrdinal("nombreEmpleado")) : string.Empty;
                }
                dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RespuestaAjax EMPLEADO_del_Archivo(string conexion, int id)
        {
            try
            {
                object[] parametros = { id };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(conexion, "spCSLDB_EMPLEADO_del_Archivo", parametros);
                RespuestaAjax respuesta = new RespuestaAjax();
                while (dr.Read())
                {
                    respuesta.Success = !dr.IsDBNull(dr.GetOrdinal("success")) ? dr.GetBoolean(dr.GetOrdinal("success")) : false;
                    respuesta.Mensaje = !dr.IsDBNull(dr.GetOrdinal("mensaje")) ? dr.GetString(dr.GetOrdinal("mensaje")) : string.Empty;
                }
                dr.Close();
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Empleado Nomina

        public CatEmpleadoAltaNominaModels GetNombreEmpleado(CatEmpleadoAltaNominaModels empleadoNomina)
        {
            try
            {
                object[] parametros = { empleadoNomina.IDEmpleado };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(empleadoNomina.Conexion, "spCSLDB_Catalogo_get_NombreEmpleadoXID", parametros);
                while (dr.Read())
                {
                    empleadoNomina.IDEmpleado = !dr.IsDBNull(dr.GetOrdinal("IDEmpleado")) ? dr.GetString(dr.GetOrdinal("IDEmpleado")) : string.Empty;
                    empleadoNomina.NombreCompleto = !dr.IsDBNull(dr.GetOrdinal("NombreCompleto")) ? dr.GetString(dr.GetOrdinal("NombreCompleto")) : string.Empty;
                    empleadoNomina.IDPuesto = !dr.IsDBNull(dr.GetOrdinal("id_puesto")) ? dr.GetInt32(dr.GetOrdinal("id_puesto")) : 0;
                }
                dr.Close();
                return empleadoNomina;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public CatEmpleadoAltaNominaModels GetEmpleadoAltaBaja(CatEmpleadoAltaNominaModels empleadoNomina)
        {
            try
            {
                object[] parametros = { empleadoNomina.IDEmpleado };
                SqlDataReader dr = null;
                object aux = SqlHelper.ExecuteScalar(empleadoNomina.Conexion, "spCSLDB_get_ALtaBajaEmpleado", parametros);
                empleadoNomina.Resultado = Convert.ToInt32(aux);
                if (empleadoNomina.Resultado.ToString() != string.Empty && empleadoNomina.Resultado==1)
                {
                    empleadoNomina.Baja = true;
                }
                else
                {
                    empleadoNomina.Baja = false;
                }
               // dr.Close();
                return empleadoNomina;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public CatEmpleadoAltaNominaModels GetSueldoBaseCategoriaPuesto(CatEmpleadoAltaNominaModels empleadoNomina)
        {
            try
            {
                object[] parametros = { empleadoNomina.IDCategoriaPuesto };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(empleadoNomina.Conexion, "spCSLDB_Catalogo_get_SueldoBaseCategoriaXID", parametros);
                while (dr.Read())
                {
                    empleadoNomina.sueldoBase = !dr.IsDBNull(dr.GetOrdinal("sueldoBase")) ? dr.GetDecimal(dr.GetOrdinal("sueldoBase")) : 0;
                }
                dr.Close();
                return empleadoNomina;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<CatMotivoBajaModels> ObteneComboCatMotivoBaja(CatEmpleadoBajaNominaModels Datos)
        {
            try
            {
                List<CatMotivoBajaModels> lista = new List<CatMotivoBajaModels>();
                CatMotivoBajaModels item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_Combo_get_CatMotivoBaja");
                //lista.Add(new CatMotivoBajaModels { IDMotivoBaja = 0, Descripcion = " - Seleccione -" });
                while (dr.Read())
                {
                    item = new CatMotivoBajaModels();
                    item.IDMotivoBaja = !dr.IsDBNull(dr.GetOrdinal("IDMotivoBaja")) ? dr.GetInt16(dr.GetOrdinal("IDMotivoBaja")) : 0;
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

        public CatEmpleadoAltaNominaModels AltaNominaEmpleado(CatEmpleadoAltaNominaModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDEmpleado, datos.IDPuesto, datos.IDCategoriaPuesto, datos.sueldoBase, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Nomina_set_AltaNominal", parametros);
                datos.Resultado = Convert.ToInt32(aux);
                if (datos.Resultado.ToString()!=string.Empty)
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

        public CatEmpleadoBajaNominaModels BajaNominaEmpleado(CatEmpleadoBajaNominaModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.IDEmpleado,datos.IDMotivoBaja,datos.Comentarios,datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Nomina_set_BajaNominal", parametros);
                datos.Resultado = Convert.ToInt32(aux);
                if (datos.Resultado.ToString() != string.Empty)
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

        public List<NominaVacacionesModels> ObtenerVacacionesNomina(NominaVacacionesModels datos)
        {
            try
            {
                List<NominaVacacionesModels> Lista = new List<NominaVacacionesModels>();
                NominaVacacionesModels Item;
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.Conexion, "spCSLDB_Nomina_get_Vacaciones", datos.IDEmpleado);
                while (dr.Read())
                {
                    Item = new NominaVacacionesModels();
                    Item.IDVacaciones = !dr.IsDBNull(dr.GetOrdinal("IDVacaciones")) ? dr.GetString(dr.GetOrdinal("IDVacaciones")) : string.Empty;
                    Item.IDEmpleado = !dr.IsDBNull(dr.GetOrdinal("IDEmpleado")) ? dr.GetString(dr.GetOrdinal("IDEmpleado")) : string.Empty;
                    Item.FechaInicio = !dr.IsDBNull(dr.GetOrdinal("FechaInicio")) ? dr.GetDateTime(dr.GetOrdinal("FechaInicio")) : DateTime.Today;
                    Item.FechaFin = !dr.IsDBNull(dr.GetOrdinal("FechaFin")) ? dr.GetDateTime(dr.GetOrdinal("FechaFin")) : DateTime.Today;
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

        public NominaVacacionesModels AVacacionesNomina(NominaVacacionesModels datos)
        {
            try
            {
                object[] parametros = {
                    datos.Opcion,
                    datos.IDVacaciones ?? string.Empty,
                    datos.IDEmpleado ?? string.Empty,
                    datos.FechaInicio != null ? datos.FechaInicio : DateTime.Today,
                    datos.FechaFin != null ? datos.FechaFin : DateTime.Today,
                    datos.Usuario ?? string.Empty
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Nomina_ac_Vacaciones", parametros);
                if (aux != null)
                {
                    int Aux = 0;
                    int.TryParse(aux.ToString(), out Aux);
                    if (Aux == 1)
                        datos.Completado = true;
                    else
                        datos.Resultado = Aux;
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NominaVacacionesModels EliminarVacacionesEmpleado(NominaVacacionesModels datos)
        {
            try
            {
                object[] parametros =
                {
                   datos.IDVacaciones, datos.IDEmpleado, datos.Usuario
                };
                object aux = SqlHelper.ExecuteScalar(datos.Conexion, "spCSLDB_Nomina_del_Vacaciones", parametros);
                datos.IDEmpleado = aux.ToString();
                if (!string.IsNullOrEmpty(datos.IDEmpleado))
                {
                    datos.Completado = true;
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        public int CrearCuenta(CuentaEmpleadoViewModels model, string IdUsuario)
        {
            try
            {
                object[] Parametros = { model.IdEmpleado, model.IdTipoUsuario, model.Cuenta, model.Password, model.Correo, IdUsuario };
                object Result = SqlHelper.ExecuteScalar(_ConexionRepositorio.CadenaConexion, "cajachica.spCIDDB_AsignarCuentaUsuarioAEmpleado", Parametros);
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

    }
}