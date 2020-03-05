var CostoExtra = function () {
    "use strict";

    //otros
    var idVenta = $("#IdVenta").val();

    var Eventos = function () {
        $("#btnCostoExtra_AC").on("click", function () {
            window.location.href = '/Admin/CostoExtra/CostoExtra_AC?idVenta=' + idVenta + '&id=0';
        });
        $(".eliminarCostoextra").on("click", function () {
            //Elimino la venta seleccionada

            var url = $(this).attr('data-hrefa');
            var id = $(this).attr('data-id');

            var box = $("#mb-eliminar");
            box.addClass("open");
            box.find(".mb-control-yes").on("click", function () {
                box.removeClass("open");
                $.ajax({
                    url: url,
                    data: { id: id, idVenta: idVenta },
                    type: 'POST',
                    dataType: 'json',
                    success: function (result) {
                        window.location.href = '/Admin/CostoExtra/CostosExtra?idVenta=' + idVenta;
                    },
                    error: function (result) {
                        window.location.href = '/Admin/CostoExtra/CostosExtra?idVenta=' + idVenta;
                    }
                });
            });

        });
    };

    /*TERMINA COBROS*/

    return {
        init: function () {
            Eventos();
        }
    };
}();