var VentaEventoRecepcion = function () {
    "use strict"
    var Id_venta = $("#Id_venta").val();
   
    /*INICIA RECEPCION*/
    var initFuncionesRecepcion = function () {
        $('.Hora24hrs').timepicker({
            minuteStep: 1,
            showMeridian: false
        });

        $("#btnAddEvento").on("click", function () {
            window.location.href = '/Admin/Venta/VentaEvento?IDVenta=' + Id_venta;
        });


        //$(document).on('submit', 'form#frm_venta', function (e) {
        //    e.preventDefault();

        //    //datos de las tablas
        //    var ganado = tblGanadoJaula.rows().data();
        //    var objGanado, cantidad = 0;
        //    var listaGanado = new Array();

        //    console.log("ganado: " + ganado);
        //    if (ganado.length == 0) {
        //        $("#tblGanadoJaula").addClass("errorTableCSL");
        //        $("#validation_summary").append("");
        //        $("#validation_summary").append("<dd style='color: #ff004d !important; '>-Debe de seleccionar un ganado para su venta</dd>");
        //    }
        //    else {

        //        $("#validation_summary").append("");

        //        for (var i = 0 ; i < ganado.length ; i++) {
        //            listaGanado.unshift(ganado[i].id_ganado);
        //            cantidad = i + 1;
        //        }

        //        var formData = new FormData();
        //        //datos de las tablas
        //        formData.append('IDVenta', Id_venta);
        //        formData.append('ListaIDGanadosParaVender', listaGanado);

        //        $.ajax({
        //            type: 'POST',
        //            data: formData,
        //            url: '/Admin/Venta/VentaGanado/',
        //            contentType: false,
        //            processData: false,
        //            cache: false,
        //            success: function (response) {
        //                window.location.href = '/Admin/Venta/Index';
        //            },
        //            error: function (request, status, error) {
        //                window.location.href = '/Admin/Venta/Index';
        //            }
        //        });
        //    }
        //});

       
    };
    function ActualizarInputs() {
        
    }
    /*TERMINA RECEPCION*/





    return {
        init: function () {
            initFuncionesRecepcion();
            //initDataTables();
            //initFuncionesGanado();
        }
    };
}();