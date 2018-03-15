using System;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Data;
using System.Collections.Generic;

namespace CreativaSL.Web.Ganados.Models
{
    public class LoginDatos
    {

        public UsuarioModels ValidarUsuario(UsuarioModels Datos)
        {
            try
            {
                object[] parametros = { Datos.user, Datos.password };
                DataSet Ds = SqlHelper.ExecuteDataset(Datos.conexion, "Login_sp", parametros);
                if (Ds != null)
                {
                    if (Ds.Tables.Count > 0)
                    {
                        DataTableReader DTRD = Ds.Tables[0].CreateDataReader();
                        while (DTRD.Read())
                        {
                            Datos.opcion = Convert.ToInt32(DTRD["id"].ToString());
                        }
                        if (Datos.opcion == 1)
                        {
                            DataTableReader Dr = Ds.Tables[1].CreateDataReader();
                            while (Dr.Read())
                            {
                                Datos.id_usuario = Dr["Id_U"].ToString();
                                Datos.nombre = Dr["U_Nombre"].ToString();
                                Datos.apPat = Dr["U_Apellidop"].ToString();
                                Datos.apMat = Dr["U_Apellidom"].ToString();
                                Datos.user = Dr["Cu_User"].ToString();
                                Datos.password = Dr["Cu_Pass"].ToString();
                                Datos.nombreCompleto = Datos.nombre.ToUpper() + " " + Datos.apPat.ToUpper();
                            }
                            List<UsuarioModels> ListaPrinc = new List<UsuarioModels>();
                            UsuarioModels Item;
                            DataTableReader DTR = Ds.Tables[2].CreateDataReader();
                            DataTable Tbl1 = Ds.Tables[1];
                            while (DTR.Read())
                            {
                                Item = new UsuarioModels();
                                Item.NombreUrl = !DTR.IsDBNull(DTR.GetOrdinal("NombreUrl")) ? DTR.GetString(DTR.GetOrdinal("NombreUrl")) : string.Empty;
                                ListaPrinc.Add(Item);
                            }
                            Datos.ListaPermisos = ListaPrinc;
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

        public UsuarioModels ObtenerPermisos(UsuarioModels datos)
        {
            try
            {
                List<UsuarioModels> lista = new List<UsuarioModels>();
                UsuarioModels item;
                object[] parametros = { datos.cuenta };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.conexion, "spCSLDB_get_CatPermisoPorUsuario", parametros);
                while (dr.Read())
                {
                    item = new UsuarioModels();
                    item.NombreUrl = !dr.IsDBNull(dr.GetOrdinal("NombreUrl")) ? dr.GetString(dr.GetOrdinal("NombreUrl")) : string.Empty;
                    lista.Add(item);
                }
                datos.ListaPermisos = lista;
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public void xxxxxx(CatAdministrativoModels datos)
        //{
        //    try
        //    {
        //        SqlHelper.ExecuteNonQuery(datos.conexion, "spCSLDB_abc_XXXXXXX", datos.nombre);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //spCSLDB_abc_XXXXXXX
    }
}
