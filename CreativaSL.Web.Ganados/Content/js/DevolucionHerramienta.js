var Devoluciones = function () {
    "use strict";

    var uiDatatable = function () {
        if ($(".datatable2").length > 0) {
            $(".datatable2").dataTable({
                "order": [],
                "language": {
                    "url": "/Content/assets/json/Spanish.json"
                },
                responsive: true
            });
            $(".datatable2").on('page.dt', function () {
                onresize(100);
            });
        }
    };//END Datatable


    var runModalProcess = function () {

        $('a.processRow').on('click', function (e) {
            e.preventDefault();
            var url = $(this).attr('href');
            var box = $('#mb-process-row');
            box.addClass('open');
            box.find('.mb-control-yes').off('click').on('click', function (e) {
                e.preventDefault();
                box.removeClass('open');
                $.ajax({
                    url: url,
                    type: 'POST',
                    dataType: 'json',
                    success: function (result) {
                        if (result == 'true') {
                            Mensaje("Devolución de herramienta finalizada correctamente", "1");
                            location.reload(true);
                        }
                        else {
                            Mensaje("Error al finalizar la devolución de herramientas. Intente nuevamente.", "2");
                        }
                    },
                    error: function () {
                        Mensaje("Error al finalizar la devolución de herramientas. Intente nuevamente.", "2");
                    }
                });
            });
        });
    };


    //var runDelete = function () {

    //    $('a.deleteRow').on('click', function (e) {
    //        e.preventDefault();
    //        var url = $(this).attr('href');
    //        var box = $('#mb-remove-row');
    //        var row = $(this).attr('data-sku');
    //        box.addClass('open');
    //        box.find('.mb-control-yes').off('click').on('click', function (e) {
    //            e.preventDefault();
    //            box.removeClass('open');
    //            $.ajax({
    //                url: url,
    //                type: 'POST',
    //                dataType: 'json',
    //                success: function (result) {
    //                    if (result == 'true') {
    //                        $("#" + row).hide("slow", function () {
    //                            box.find(".mb-control-yes").prop('onclick', null).off('click');
    //                            $("#" + row).remove();
    //                            Mensaje("Registro Eliminado Correctamente", "1");
    //                        });
    //                    }
    //                    else {
    //                        Mensaje("Error al eliminar el registro", "2");
    //                    }
    //                },
    //                error: function () {
    //                    $('#Error').html('<h3>Ocurrio un error al eliminar este registro<h3>');
    //                    $('#Error').css("display", "block");
    //                    $('#Error').delay(400).slideDown(400).delay(2000).slideUp(400);
    //                    $('#Error').css("display", "block");
    //                }
    //            });

    //        });
    //    });

    //};


    return {
        //main function to initiate template pages
        init: function () {
            runModalProcess();
        }
    };
}();