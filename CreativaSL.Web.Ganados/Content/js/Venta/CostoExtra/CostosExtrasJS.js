var CostoExtra = function () {
    "use strict";

    //otros
    var idVenta = $("#IdVenta").val();

    var Eventos = function () {
        $("#btnCostoExtra_AC").on("click", function () {
            window.location.href = '/Admin/Venta/CostoExtra_AC?idVenta=' + idVenta + '&id=0';
        });
    };

    /*TERMINA COBROS*/

    return {
        init: function () {
            Eventos();
        }
    };
}();