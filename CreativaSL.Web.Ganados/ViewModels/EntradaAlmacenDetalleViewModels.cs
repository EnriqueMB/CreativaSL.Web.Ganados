using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.ViewModels
{
    public class EntradaAlmacenDetalleViewModels
    {

        private string _IDEntrega;

        public string IDEntrega
        {
            get { return _IDEntrega; }
            set { _IDEntrega = value; }
        }

        private List<CantidadEntregaViewModels> _ListaDetalle;

        public List<CantidadEntregaViewModels> ListaDetalle
        {
            get { return _ListaDetalle; }
            set { _ListaDetalle = value; }
        }

    }
}