var Clasificacion = function () {
    "use strict";

    var runDelete = function () {
        $('.table-responsive').delegate('a.deleteRow', 'click', function (e) {
            e.preventDefault();
            var url = $(this).attr('href');
            var row = $(this).attr('data-sku');
            console.log(row);
            var box = $('#mb-remove-row');
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
                            $("#" + row).hide("slow", function () {
                                box.find(".mb-control-yes").prop('onclick', null).off('click');
                                $("#" + row).remove();
                                Mensaje("Registro eliminado correctamente", "1");
                            });
                        }
                        else {
                            Mensaje("Error al eliminar el registro", "2");
                        }
                    },
                    error: function () {
                        $('#Error').html('<h3>Ocurrió un error al eliminar este registro<h3>');
                        $('#Error').css("display", "block");
                        $('#Error').delay(400).slideDown(400).delay(2000).slideUp(400);
                        $('#Error').css("display", "block");
                    }
                });

            });
        });

    };

    return {
        //main function to initiate template pages
        init: function () {
            runDelete();
        }
    };
}();