using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.Models
{
    public class ClienteLugarModels
    {
        public ClienteLugarModels() {
            _nombreCliente = string.Empty;
            _nombreLugar = string.Empty;
            _descripcionLugar = string.Empty;
            _IDCliente = string.Empty;
            _IDClienteLugar = string.Empty;
            _IDLugar = string.Empty;
            _IDSucursal = string.Empty;
            //Datos de control
            Conexion = string.Empty;
            Resultado = 0;
            Completado = false;
            Usuario = string.Empty;
            Opcion = 0;
        }
        private List<CatLugarModels> _listaLugares;

        public List<CatLugarModels> listaLugares
        {
            get { return _listaLugares; }
            set { _listaLugares = value; }
        }

        private List<ClienteLugarModels> _listaClienteLugar;

        public List<ClienteLugarModels> ListaClienteLugar
        {
            get { return _listaClienteLugar; }
            set { _listaClienteLugar = value; }
        }
        private string _IDSucursal;

        public string IDSucursal
        {
            get { return _IDSucursal; }
            set { _IDSucursal = value; }
        }

        private string _nombreLugar;

        public string nombreLugar
        {
            get { return _nombreLugar; }
            set { _nombreLugar = value; }
        }
        private string _nombreCliente;

        public string nombreCliente
        {
            get { return _nombreCliente; }
            set { _nombreCliente = value; }
        }
        private string _descripcionLugar;

        public string descripcionLugar
        {
            get { return _descripcionLugar; }
            set { _descripcionLugar = value; }
        }

        private string _IDCliente;

        public string IDCliente
        {
            get { return _IDCliente; }
            set { _IDCliente = value; }
        }

        private string _IDClienteLugar;

        public string IDClienteLugar
        {
            get { return _IDClienteLugar; }
            set { _IDClienteLugar = value; }
        }

        private string _IDLugar;

        public string IDLugar
        {
            get { return _IDLugar; }
            set { _IDLugar = value; }
        }
        #region Datos De Control
        public string Conexion { get; set; }
        public int Resultado { get; set; }
        public bool Completado { get; set; }
        public string Usuario { get; set; }
        public int Opcion { get; set; }
        #endregion

    }
}