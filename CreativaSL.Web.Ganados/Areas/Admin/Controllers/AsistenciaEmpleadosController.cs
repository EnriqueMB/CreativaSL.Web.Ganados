﻿using ExcelDataReader;
using System;
using System.Collections.Generic;

using System.IO;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativaSL.Web.Ganados.Models;
using CreativaSL.Web.Ganados.Filters;
using System.Configuration;

namespace CreativaSL.Web.Ganados.Areas.Admin.Controllers
{
    [Autorizado]
    public class AsistenciaEmpleadosController : Controller
    {
        string Conexion = ConfigurationManager.AppSettings.Get("strConnection");
        // GET: Admin/DatosExcel
        public ActionResult Index()
        {
            AsistenciaEmpleadoModels Asistencia = new AsistenciaEmpleadoModels();
            _AsistenciaEmpleados_Datos AsistenciaDatos = new _AsistenciaEmpleados_Datos();
            _Combos_Datos Combo = new _Combos_Datos();
            Asistencia.ListaSucursales = Combo.ObtenerComboSucursales(Conexion);
            Asistencia.conexion = Conexion;
            Asistencia.IDSucursal = string.Empty;
            Asistencia.listaEmpleados = AsistenciaDatos.obtenerListaEmpleados(Asistencia);

            return View(Asistencia);
        }
        [HttpPost]
        public ActionResult Index(AsistenciaEmpleadoModels Asistencia)
        {
            _AsistenciaEmpleados_Datos AsistenciaDatos = new _AsistenciaEmpleados_Datos();
            _Combos_Datos Combo = new _Combos_Datos();
            Asistencia.ListaSucursales = Combo.ObtenerComboSucursales(Conexion);
            Asistencia.conexion = Conexion;
            
            Asistencia.listaEmpleados = AsistenciaDatos.obtenerListaEmpleados(Asistencia);

            return View(Asistencia);
        }

        public ActionResult Upload(string id)
        {
            AsistenciaEmpleadoModels Asistencia = new AsistenciaEmpleadoModels();
            Asistencia.fecha = Convert.ToDateTime(id);

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(string id, HttpPostedFileBase uploadfile)
        {
            AsistenciaEmpleadoModels Asistencia = new AsistenciaEmpleadoModels();
            Asistencia.conexion = Conexion;
            Asistencia.opcion = 1;
            Asistencia.fecha = Convert.ToDateTime(id);
            _AsistenciaEmpleados_Datos AsistenciaDatos = new _AsistenciaEmpleados_Datos();
            if (ModelState.IsValid)
            {
                if (uploadfile != null && uploadfile.ContentLength > 0)
                {
                    //ExcelDataReader works on binary excel file
                    Stream stream = uploadfile.InputStream;
                    //We need to written the Interface.
                    IExcelDataReader reader = null;
                    if (uploadfile.FileName.EndsWith(".xls"))
                    {
                        //reads the excel file with .xls extension
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (uploadfile.FileName.EndsWith(".xlsx"))
                    {
                        //reads excel file with .xlsx extension
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        //Shows error if uploaded file is not Excel file
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                    //treats the first row of excel file as Coluymn Names
                    //reader.IsFirstRowAsColumnNames = true;

                    //Adding reader data to DataSet()
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        //UseColumnDataType = false,
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                            //UseColumnDataType = false
                        }
                    });


                    reader.Close();
                    Asistencia.user = User.Identity.Name;
                    //Sending result data to View

                    DataTable firstTable = result.Tables[0];

                    DataTable dtCloned = firstTable.Clone();
                    dtCloned.Columns[3].DataType = typeof(string);
                    foreach (DataRow row in firstTable.Rows)
                    {
                        string fecha_row =  row[3].ToString();
                        string fecha = fecha_row.Substring(0, 10);
                        row[3] = fecha;

                        dtCloned.ImportRow(row);
                    }


                    Asistencia.tablaAsistencia = dtCloned;

                    string a = Asistencia.tablaAsistencia.Rows[0][3].ToString();
                    Asistencia.fecha = Convert.ToDateTime(a);
                    Asistencia.tablaAsistencia = result.Tables[0];
                    
                    //string a = Asistencia.tablaAsistencia.Rows[0][3].ToString();
                    //Asistencia.fecha = Convert.ToDateTime(a);
                    Asistencia.listaAsistencia=AsistenciaDatos.GenerarListaFaltas(Asistencia);
                    if (Asistencia.Completado == true)
                    {
                        TempData["typemessage"] = "1";
                        TempData["message"] = "Se han registrado las faltas del día.";
                        return View(Asistencia);
                    }
                    else
                    {
                        Asistencia.listaAsistencia = new List<AsistenciaEmpleadoModels>();
                        TempData["typemessage"] = "2";
                        TempData["message"] = "Ocurrió un error registrar las faltas.";
                        return View(Asistencia);
                    }
                  
                }
            }
            else
            {
                ModelState.AddModelError("File", "Please upload your file");
            }
            return View();
        }
        // GET: Admin/EntregaCombustible/Delete/5
        public ActionResult Actualizar(int id)
        {
            return View();
        }

        // POST: Admin/EntregaCombustible/Delete/5
        [HttpPost]
        public ActionResult Actualizar(string id, FormCollection collection)
        {
            try
            {
                AsistenciaEmpleadoModels Asistencia = new AsistenciaEmpleadoModels();
                _AsistenciaEmpleados_Datos AsistenciaDatos = new _AsistenciaEmpleados_Datos();
                Asistencia.conexion = Conexion;
                Asistencia.IDFalta = id;
                Asistencia.opcion = 1;
                Asistencia.user = User.Identity.Name;
                Asistencia = AsistenciaDatos.ActualizarListaFaltas(Asistencia);
                if (Asistencia.Completado) {
                    TempData["typemessage"] = "1";
                    TempData["message"] = "La lista de faltas fue actualizada";
                    return Json("");
                }

                else { return Json("false"); }
                    
                // TODO: Add delete logic here
            }
            catch
            {
                EntregaCombustibleModels Entrega = new EntregaCombustibleModels();
                TempData["typemessage"] = "2";
                TempData["message"] = "No se pudo borrar los datos. Por favor contacte a soporte técnico";
                return Json("");

            }
        }
    }
}