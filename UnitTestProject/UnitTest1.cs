using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CreativaSL.Web.Ganados.Models;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {   
        // unit test code 
        [TestMethod]
        public void _Process_Out()
        {
            // arrange 
            string IDSalida = "192830848", Conexion = "Data Source=192.168.1.150;Initial Catalog=CSLDB_GRUPOOCAMPO; user=leyder; password=12345678", Usuario = "0001" ;
            _SalidaAlmacen_Datos Data = new _SalidaAlmacen_Datos();
            SalidaAlmacenModels Resultado =  Data.ProcesarSalidaAlmacen(Conexion, IDSalida, Usuario);
            Assert.IsTrue(Resultado.Completado, "No se completó la salida");
            //Assert.Fail("Error al procesar la salida.");
        }  
    }
}
