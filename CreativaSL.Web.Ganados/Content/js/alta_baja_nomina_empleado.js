
    function alta_row(row) {
        var url = $("#alta-" + row).attr('data-hrefa');
        var box = $("#mb-alta-row");
        box.addClass("open");
        box.find(".mb-control-yes").on("click", function () {
            box.removeClass("open");
            $.ajax({
                url: url,
                type: 'POST',
                dataType: 'json',
                success: function (result) {
                        box.find(".mb-control-yes").prop('onclick', null).off('click');
                       // Mensaje("Empleado fue dado de alta", "1");
                        location.reload(true);
                },
                error: function () {
                    $('#Error').html('<h3>Ocurrio un error al dar de alta este registro<h3>');
                    $('#Error').css("display", "block");
                    $('#Error').delay(400).slideDown(400).delay(2000).slideUp(400);
                    $('#Error').css("display", "block");
                }
            });
        });
    }

  
    function baja_row(row) {
        var url = $("#baja-" + row).attr('data-hrefa');
        var box = $("#mb-baja-row");
        box.addClass("open");
        box.find(".mb-control-yes").on("click", function () {
            box.removeClass("open");
            $.ajax({
                url: url,
                type: 'POST',
                dataType: 'json',
                success: function (result) {
                        box.find(".mb-control-yes").prop('onclick', null).off('click');
                       // Mensaje("Empleado fue dado de baja", "1");
                        location.reload(true);
                },
                error: function () {
                    $('#Error').html('<h3>Ocurrio un error al dar de baja este registro<h3>');
                    $('#Error').css("display", "block");
                    $('#Error').delay(400).slideDown(400).delay(2000).slideUp(400);
                    $('#Error').css("display", "block");
                }
            });
        });
    }
   


    











   