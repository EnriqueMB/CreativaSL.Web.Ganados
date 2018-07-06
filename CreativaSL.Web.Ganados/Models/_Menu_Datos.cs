using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml;

namespace CreativaSL.Web.Ganados.Models
{
    public class Menu_Datos
    {
        public MenuModels ObtenerMenu2(MenuModels Datos)
        {
            try
            {
                DataSet Ds = SqlHelper.ExecuteDataset(Datos.Conexion, "spCSLDB_get_Menu", Datos.User, Datos.TipoMenu);
                if (Ds != null)
                {
                    if (Ds.Tables.Count == 1)
                    {
                        List<MenuModels> ListaPrinc = new List<MenuModels>();
                        MenuModels Item;
                        DataTableReader DTR = Ds.Tables[0].CreateDataReader();
                        DataTable Tbl1 = Ds.Tables[0];
                        while (DTR.Read())
                        {
                            Item = new MenuModels();
                            Item.ListaMenuDetalle = new List<MenuModels>();
                            Item.MenuID = !DTR.IsDBNull(DTR.GetOrdinal("MenuID")) ? DTR.GetInt32(DTR.GetOrdinal("MenuID")) : 0;
                            Item.NombreMenu = !DTR.IsDBNull(DTR.GetOrdinal("NombreMenu")) ? DTR.GetString(DTR.GetOrdinal("NombreMenu")) : string.Empty;
                            Item.OrdenMenu = !DTR.IsDBNull(DTR.GetOrdinal("OrdenMenu")) ? DTR.GetInt32(DTR.GetOrdinal("OrdenMenu")) : 0;
                            Item.IconMenu = !DTR.IsDBNull(DTR.GetOrdinal("Icon")) ? DTR.GetString(DTR.GetOrdinal("Icon")) : string.Empty;
                            Item.UrlMenu = !DTR.IsDBNull(DTR.GetOrdinal("UrlMenu")) ? DTR.GetString(DTR.GetOrdinal("UrlMenu")) : string.Empty;
                            //Item.IDFranquicia = !DTR.IsDBNull(0) ? DTR.GetString(0) : string.Empty;
                            //Item.Descripcion = !DTR.IsDBNull(1) ? DTR.GetString(1) : string.Empty;

                            //string Aux = DTR.GetString(2);
                            string Aux = !DTR.IsDBNull(DTR.GetOrdinal("TablaSubMenu")) ? DTR.GetString(DTR.GetOrdinal("TablaSubMenu")) : string.Empty;
                            Aux = string.Format("<Main>{0}</Main>", Aux);
                            XmlDocument xm = new XmlDocument();
                            xm.LoadXml(Aux);
                            XmlNodeList Registros = xm.GetElementsByTagName("Main");
                            XmlNodeList Lista = ((XmlElement)Registros[0]).GetElementsByTagName("C");
                            List<MenuModels> ListaAux = new List<MenuModels>();
                            MenuModels ItemAux;
                            foreach (XmlElement Nodo in Lista)
                            {
                                ItemAux = new MenuModels();
                                XmlNodeList MenuID = Nodo.GetElementsByTagName("MenuID");
                                XmlNodeList NombreMenu = Nodo.GetElementsByTagName("NombreMenu");
                                XmlNodeList MenuUrl = Nodo.GetElementsByTagName("MenuUrl");
                                XmlNodeList MenuIcon = Nodo.GetElementsByTagName("Icono");
                                ItemAux.MenuID = Convert.ToInt32(MenuID[0].InnerText);
                                ItemAux.NombreMenu = NombreMenu[0].InnerText;
                                ItemAux.UrlMenu = MenuUrl[0].InnerText;
                                ItemAux.IconMenu = MenuIcon[0].InnerText;
                                Item.ListaMenuDetalle.Add(ItemAux);
                            }
                            ListaPrinc.Add(Item);
                        }
                        DTR.Close();
                        Datos.ListaMenu = ListaPrinc;
                    }
                }
                
                return Datos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MenuModels ObtenerPermisoUsuario(MenuModels Datos)
        {
            try
            {
                DataSet Ds = SqlHelper.ExecuteDataset(Datos.Conexion, "spCSLDB_get_PermisosXID", Datos.User);
                if (Ds != null)
                {
                    if (Ds.Tables.Count == 1)
                    {
                        List<MenuModels> ListaPrinc = new List<MenuModels>();
                        MenuModels Item;
                        DataTableReader DTR = Ds.Tables[0].CreateDataReader();
                        DataTable Tbl1 = Ds.Tables[0];
                        while (DTR.Read())
                        {
                            Item = new MenuModels();
                            Item.ListaPermisoDetalle = new List<MenuModels>();
                            Item.IDPermiso = !DTR.IsDBNull(DTR.GetOrdinal("IDPermiso")) ? DTR.GetString(DTR.GetOrdinal("IDPermiso")) : string.Empty;
                            Item.MenuID = !DTR.IsDBNull(DTR.GetOrdinal("MenuID")) ? DTR.GetInt32(DTR.GetOrdinal("MenuID")) : 0;
                            Item.NombreMenu = !DTR.IsDBNull(DTR.GetOrdinal("NombreMenu")) ? DTR.GetString(DTR.GetOrdinal("NombreMenu")) : string.Empty;
                            Item.ver = DTR.GetBoolean(DTR.GetOrdinal("ver"));
                            //string Aux = DTR.GetString(2);
                            string Aux = !DTR.IsDBNull(DTR.GetOrdinal("TablaPermiso")) ? DTR.GetString(DTR.GetOrdinal("TablaPermiso")) : string.Empty;
                            Aux = string.Format("<Main>{0}</Main>", Aux);
                            XmlDocument xm = new XmlDocument();
                            xm.LoadXml(Aux);
                            XmlNodeList Registros = xm.GetElementsByTagName("Main");
                            XmlNodeList Lista = ((XmlElement)Registros[0]).GetElementsByTagName("C");
                            List<MenuModels> ListaAux = new List<MenuModels>();
                            MenuModels ItemAux;
                            foreach (XmlElement Nodo in Lista)
                            {
                                ItemAux = new MenuModels();
                                XmlNodeList MenuID = Nodo.GetElementsByTagName("MenuID");
                                XmlNodeList NombreMenu = Nodo.GetElementsByTagName("NombreMenu");
                                XmlNodeList ver = Nodo.GetElementsByTagName("ver");
                                XmlNodeList IDPermiso = Nodo.GetElementsByTagName("IDPermiso");
                                ItemAux.MenuID = Convert.ToInt32(MenuID[0].InnerText);
                                ItemAux.NombreMenu = NombreMenu[0].InnerText;
                                int Visto = 0;
                                int.TryParse(ver[0].InnerText, out Visto);
                                if (Visto == 1)
                                {
                                    ItemAux.ver = true;
                                }
                                else
                                {
                                    Item.ver = false;
                                }
                                ItemAux.IDPermiso = IDPermiso[0].InnerText;
                                Item.ListaPermisoDetalle.Add(ItemAux);
                            }
                            ListaPrinc.Add(Item);
                        }
                        DTR.Close();
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



        public MenuModels ObtenerAllPermisoUsuario(MenuModels Datos)
        {
            try
            {
                SqlDataReader Dr = SqlHelper.ExecuteReader(Datos.Conexion, "spCSLDB_get_Menu2", Datos.User, Datos.TipoMenu);
                List<MenuModels> ListaPrinc = new List<MenuModels>();
                MenuModels Item;
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
                    ListaPrinc.Add(Item);
                }
                Dr.Close();
                Datos.ListaMenu = this.ObtenerListaSubMenus(0, ListaPrinc);
                return Datos;
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
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}