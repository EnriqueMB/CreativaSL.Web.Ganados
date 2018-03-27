using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Xml;
using System.Linq;

namespace CreativaSL.Web.Ganados.Models
{
    public class _Usuario_Datos
    {
        public List<TipoUsuarioModels> ObtenerComboTipoUsuario(UsuarioModels datos) 
        {
            try
            {
                List<TipoUsuarioModels> lista = new List<TipoUsuarioModels>();
                TipoUsuarioModels item;
                object[] parametros = { datos.opcion };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.conexion, "spCSLDB_get_ComboCatTipoUsuario");
                while (dr.Read())
                {
                    item = new TipoUsuarioModels();
                    item.id_tipoUsuario = Int32.Parse(dr["id_tipoUsuario"].ToString());
                    item.tipoUsuario = dr["tipoUsuario"].ToString();
                    lista.Add(item);
                }
                return lista;
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }
        public UsuarioModels ObtenerUsuarios(UsuarioModels datos)
        {
            try
            {
                DataSet ds = null;
                ds = SqlHelper.ExecuteDataset(datos.conexion, "spCSLDB_get_CatUsuariosAdmin");
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0] != null)
                        {
                            datos.tablaUsuario = ds.Tables[0];
                        }
                    }
                }
                return datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UsuarioModels ObtenerDetalleUsuarioxID(UsuarioModels datos)
        {
            try
            {
                object[] parametros = { datos.id_usuario };
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(datos.conexion, "spCSLDB_get_DetalleUsuarioxID", parametros);
                while (dr.Read())
                {
                    datos.id_usuario = dr["id_persona"].ToString();
                    datos.id_tipoUsuario = Int32.Parse(dr["id_tipoPersona"].ToString());
                    //datos.tipoUsuario = dr["tipoUsuario"].ToString();
                    datos.nombre = dr["nombre"].ToString();
                    datos.apPat = dr["apPaterno"].ToString();
                    datos.apMat = dr["apMaterno"].ToString();
                    //datos.fechaNac = DateTime.Parse(dr["fechaNac"].ToString());
                    datos.direccion = dr["direccion"].ToString();
                    //datos.codigoPostal = dr["codigoPostal"].ToString();
                    datos.telefono = dr["telefono"].ToString();
                    datos.email = dr["correo"].ToString();
                    //datos.url_foto = dr["url_foto"].ToString();
                    //datos.cuenta = dr["cuenta"].ToString();
                    //datos.password = "dc89sd989sdd";
                }
                return datos;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UsuarioModels EliminarUsuario(UsuarioModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.id_usuario, datos.user
                };
                object aux = SqlHelper.ExecuteScalar(datos.conexion, "spCSLDB_Catalogo_del_Usuarios", parametros);
                datos.id_usuario = aux.ToString();
                if (!string.IsNullOrEmpty(datos.id_usuario))
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

        public UsuarioModels AbcCatUsuarios(UsuarioModels datos)
        {
            try
            {
                object[] parametros =
                {
                    datos.opcion, datos.id_usuario, datos.id_tipoUsuario, datos.nombre, datos.apPat,
                    datos.apMat, datos.fechaNac, datos.direccion, datos.codigoPostal, datos.telefono, datos.email,
                    datos.url_foto, datos.cuenta, datos.password, datos.user
                    };
                object aux = SqlHelper.ExecuteScalar(datos.conexion, "spCSLDB_abc_CatUsuarios", parametros);
                datos.id_usuario = aux.ToString();
                if (!string.IsNullOrEmpty(datos.id_usuario))
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

        public int ObtenerTipoUsuarioByUserName(UsuarioModels usuario)
        {
            try
            {
                object aux = SqlHelper.ExecuteScalar(usuario.conexion, "spCSLDB_get_TipoUsuarioByUserName", usuario.cuenta);
                return Convert.ToInt32(aux.ToString());
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool CheckUserName(UsuarioModels usuario)
        {
            try
            {
                object aux = SqlHelper.ExecuteScalar(usuario.conexion, "spCSLDB_get_CheckValidarUsuario", usuario.cuenta, usuario.id_usuario);
                return aux.ToString().Equals("1") ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckEmail(UsuarioModels usuario)
        {
            try
            {
                object aux = SqlHelper.ExecuteScalar(usuario.conexion, "spCSLDB_get_CheckEmail", usuario.email, usuario.id_usuario);
                return aux.ToString().Equals("1") ? true : false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public UsuarioModels ResetPassword(UsuarioModels usuario)
        {
            try
            {
                SqlDataReader dr = null;
                dr = SqlHelper.ExecuteReader(usuario.conexion, "spCSLDB_get_PasswordReset", usuario.email2);
                while (dr.Read())
                {
                    usuario.activo = dr["activo"].ToString().Equals("1") ? true : false;
                    usuario.cuenta = dr["clvUser"].ToString();
                    usuario.password = dr["pass"].ToString();
                }
                return usuario;
            }
            catch (Exception ex)
            {
                usuario.activo = false;
                return usuario;
            }
        }

        public static UsuarioModels ObtenerUsuario(UsuarioModels datos)
        {
            try
            {
                object[] parametros = { datos.id_usuario, datos.id_tipoUsuario };
                DataSet ds = null;
                ds = SqlHelper.ExecuteDataset(datos.conexion, "EM_spCSLDB_get_CatCuentaUser", parametros);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0] != null)
                        {
                            datos.tablaUsuario = ds.Tables[0];
                        }
                    }
                }
                return datos;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //OBTIENE LISTA DE RUTAS PARA SER DESPLEGADOS EN PERMISOS

        public UsuarioModels ObtenerPermisoUsuario(UsuarioModels Datos)
        {
            try
            {
                DataSet Ds = SqlHelper.ExecuteDataset(Datos.conexion, "spCSLDB_get_PermisosXID", Datos.id_usuario, Datos.id_tipoUsuario);
                if (Ds != null)
                {
                    if (Ds.Tables.Count == 1)
                    {
                        List<UsuarioModels> ListaPrinc = new List<UsuarioModels>();
                        UsuarioModels Item;
                        DataTableReader DTR = Ds.Tables[0].CreateDataReader();
                        DataTable Tbl1 = Ds.Tables[0];
                        while (DTR.Read())
                        {
                            Item = new UsuarioModels();
                            Item.ListaPermisosDetalle = new List<UsuarioModels>();
                            Item.IDPermiso = !DTR.IsDBNull(DTR.GetOrdinal("IDPermiso")) ? DTR.GetString(DTR.GetOrdinal("IDPermiso")) : string.Empty;
                            Item.IDMenu = !DTR.IsDBNull(DTR.GetOrdinal("MenuID")) ? DTR.GetInt32(DTR.GetOrdinal("MenuID")) : 0;
                            Item.NombreMenu = !DTR.IsDBNull(DTR.GetOrdinal("NombreMenu")) ? DTR.GetString(DTR.GetOrdinal("NombreMenu")) : string.Empty;
                            Item.ver = DTR.GetBoolean(DTR.GetOrdinal("ver"));
                            //string Aux = DTR.GetString(2);
                            string Aux = !DTR.IsDBNull(DTR.GetOrdinal("TablaPermiso")) ? DTR.GetString(DTR.GetOrdinal("TablaPermiso")) : string.Empty;
                            Aux = string.Format("<Main>{0}</Main>", Aux);
                            XmlDocument xm = new XmlDocument();
                            xm.LoadXml(Aux);
                            XmlNodeList Registros = xm.GetElementsByTagName("Main");
                            XmlNodeList Lista = ((XmlElement)Registros[0]).GetElementsByTagName("C");
                            List<UsuarioModels> ListaAux = new List<UsuarioModels>();
                            UsuarioModels ItemAux;
                            foreach (XmlElement Nodo in Lista)
                            {
                                ItemAux = new UsuarioModels();
                                XmlNodeList MenuID = Nodo.GetElementsByTagName("MenuID");
                                XmlNodeList NombreMenu = Nodo.GetElementsByTagName("NombreMenu");
                                XmlNodeList ver = Nodo.GetElementsByTagName("ver");
                                XmlNodeList IDPermiso = Nodo.GetElementsByTagName("IDPermiso");
                                ItemAux.IDMenu = Convert.ToInt32(MenuID[0].InnerText);
                                ItemAux.NombreMenu = NombreMenu[0].InnerText;
                                int Visto = 0;
                                int.TryParse(ver[0].InnerText, out Visto);
                                if (Visto == 1)
                                {
                                    ItemAux.ver = true;
                                }
                                else
                                {
                                    ItemAux.ver = false;
                                }
                                ItemAux.IDPermiso = IDPermiso[0].InnerText;
                                Item.ListaPermisosDetalle.Add(ItemAux);
                            }
                            ListaPrinc.Add(Item);
                        }
                        Datos.ListaPermisos = ListaPrinc;
                    }
                }
                return Datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MenuModels> ObtenerAllPermisoUsuario(UsuarioModels Datos)
        {
            try
            {
                SqlDataReader Dr = SqlHelper.ExecuteReader(Datos.conexion, "spCSLDB_get_Menu2", Datos.id_usuario, Datos.id_tipoUsuario);
                List<MenuModels> ListaPrinc = new List<MenuModels>();
                MenuModels Item;
                int cont = 0;
                while (Dr.Read())
                {
                    Item = new MenuModels();
                    Item.ListaPermisoDetalle = new List<MenuModels>();
                    Item.IDPermiso = !Dr.IsDBNull(Dr.GetOrdinal("IDPermiso")) ? Dr.GetString(Dr.GetOrdinal("IDPermiso")) : string.Empty;
                    Item.UrlMenu = !Dr.IsDBNull(Dr.GetOrdinal("MenuURL")) ? Dr.GetString(Dr.GetOrdinal("MenuURL")) : string.Empty;
                    Item.IconMenu = !Dr.IsDBNull(Dr.GetOrdinal("MenuIcon")) ? Dr.GetString(Dr.GetOrdinal("MenuIcon")) : string.Empty;
                    Item.MenuID = !Dr.IsDBNull(Dr.GetOrdinal("MenuID")) ? Dr.GetInt32(Dr.GetOrdinal("MenuID")) : 0;
                    Item.ParentMenuID = !Dr.IsDBNull(Dr.GetOrdinal("ParentMenuID")) ? Dr.GetInt32(Dr.GetOrdinal("ParentMenuID")) : 0;
                    Item.NombreMenu = !Dr.IsDBNull(Dr.GetOrdinal("NombreMenu")) ? Dr.GetString(Dr.GetOrdinal("NombreMenu")) : string.Empty;
                    Item.ver = Dr.GetBoolean(Dr.GetOrdinal("ver"));
                    Item.NumRow = cont;
                    cont++;
                    ListaPrinc.Add(Item);
                }
                Datos.ListaMenuPermisos = ListaPrinc;
                Datos.listaMenu = this.ObtenerListaSubMenus(0, ListaPrinc);
                return Datos.listaMenu;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MenuModels> ObtenerListaPermisosUsuario(UsuarioModels Datos)
        {
            try
            {
                SqlDataReader Dr = SqlHelper.ExecuteReader(Datos.conexion, "spCSLDB_get_MenuPermission", Datos.id_usuario, Datos.id_tipoUsuario);
                List<MenuModels> ListaPrinc = new List<MenuModels>();
                MenuModels Item;
                int cont = 0;
                while (Dr.Read())
                {
                    Item = new MenuModels();
                    Item.ListaPermisoDetalle = new List<MenuModels>();
                    Item.IDPermiso = !Dr.IsDBNull(Dr.GetOrdinal("IDPermiso")) ? Dr.GetString(Dr.GetOrdinal("IDPermiso")) : string.Empty;
                    Item.UrlMenu = !Dr.IsDBNull(Dr.GetOrdinal("MenuURL")) ? Dr.GetString(Dr.GetOrdinal("MenuURL")) : string.Empty;
                    Item.IconMenu = !Dr.IsDBNull(Dr.GetOrdinal("MenuIcon")) ? Dr.GetString(Dr.GetOrdinal("MenuIcon")) : string.Empty;
                    Item.MenuID = !Dr.IsDBNull(Dr.GetOrdinal("MenuID")) ? Dr.GetInt32(Dr.GetOrdinal("MenuID")) : 0;
                    Item.OrdenMenu = !Dr.IsDBNull(Dr.GetOrdinal("OrderNumber")) ? Dr.GetInt32(Dr.GetOrdinal("OrderNumber")) : 0;
                    Item.ParentMenuID = !Dr.IsDBNull(Dr.GetOrdinal("ParentMenuID")) ? Dr.GetInt32(Dr.GetOrdinal("ParentMenuID")) : 0;
                    Item.NombreMenu = !Dr.IsDBNull(Dr.GetOrdinal("NombreMenu")) ? Dr.GetString(Dr.GetOrdinal("NombreMenu")) : string.Empty;
                    Item.ver = Dr.GetBoolean(Dr.GetOrdinal("ver"));
                    Item.NumRow = cont;
                    cont++;
                    ListaPrinc.Add(Item);
                }
                Datos.ListaMenuPermisos = ListaPrinc;
                Datos.listaMenu = this.ObtenerListaSubMenus(0, ListaPrinc);
                return Datos.listaMenu;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<MenuModels> ObtenerListaSubMenus(int ParentID, List<MenuModels> FullList)
        {
            try
            {
                List<MenuModels> ListaSubMenu = new List<MenuModels>();
                List<MenuModels> ListaAux = FullList.FindAll(x => x.ParentMenuID == ParentID).OrderBy(x => x.OrdenMenu).ToList<MenuModels>();
                foreach (MenuModels ItemMenu in ListaAux)
                {
                    ItemMenu.ListaMenu = ObtenerListaSubMenus(ItemMenu.MenuID, FullList);
                    ListaSubMenu.Add(ItemMenu);
                }
                return ListaSubMenu;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GuardarPermisos(UsuarioModels datos)
        {
            try
            {
                DataSet dt = SqlHelper.ExecuteDataset(datos.conexion, CommandType.StoredProcedure, "spCSLDB_abc_ActualizarPermiso",
                new SqlParameter("@IDPersona", datos.id_usuario),
                new SqlParameter("@Permisos", datos.TablaPermisos),
                new SqlParameter("@usuario", datos.user));
                return Convert.ToInt32(dt.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
