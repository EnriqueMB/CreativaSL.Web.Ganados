using CreativaSL.Web.Ganados.Models.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativaSL.Web.Ganados.ViewModels
{
    public class CantidadEntregaViewModels
    {

        private string _Producto;

        public string Producto
        {
            get { return _Producto; }
            set { _Producto = value; }
        }

        private decimal _CantidadCompra;

        public decimal CantidadCompra
        {
            get { return _CantidadCompra; }
            set { _CantidadCompra = value; }
        }

        private decimal _CantidadAsignada;

        public decimal CantidadAsignada
        {
            get { return _CantidadAsignada; }
            set { _CantidadAsignada = value; }
        }
        

        private decimal _Cantidad;
        [Display(Name = "Cantidad de entrega")]
        [Required(ErrorMessage = "Cantidad es requerida.")]
        public decimal Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        private string _UnidadMedida;

        public string UnidadMedida
        {
            get { return _UnidadMedida; }
            set { _UnidadMedida = value; }
        }

        private string _IDEntradaAlmacenDetalle;

        public string IDEntradaAlmacenDetalle
        {
            get { return _IDEntradaAlmacenDetalle; }
            set { _IDEntradaAlmacenDetalle = value; }
        }

        private string _IDCompraAlmacenDetalle;

        public string IDCompraAlmacenDetalle
        {
            get { return _IDCompraAlmacenDetalle; }
            set { _IDCompraAlmacenDetalle = value; }
        }


    }
}