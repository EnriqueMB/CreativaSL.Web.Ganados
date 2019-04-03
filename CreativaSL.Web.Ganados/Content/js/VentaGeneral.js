var VentaGeneral = function () {
    "use strict";
    // Funcion para validar registrar
    var runValidator1 = function () {
        var form1 = $('#frm_ventaGeneral');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({
            errorElement: "dd", // contain the error msg in a span tag
            errorClass: 'help-block color',
            errorLabelContainer: $("#validation_summary"),
            errorPlacement: function (error, element) { // render error placement for each input type
                if (element.attr("type") === "radio" || element.attr("type") === "checkbox") { // for chosen elements, need to insert the error after the chosen container
                    error.insertAfter($(element).closest('.form-group').children('div').children().last());
                } else if (element.attr("name") === "dd" || element.attr("name") === "mm" || element.attr("name") === "yyyy") {
                    error.insertAfter($(element).closest('.form-group').children('div'));
                } else if (element.attr("type") === "text") {
                    error.insertAfter($(element).closest('.input-group').children('div'));
                } else {
                    error.insertAfter(element);
                    // for other inputs, just perform default behavior
                }
            },
            ignore: "",
            rules: {
                Id_cliente: { required: true },
                Id_sucursal: { required: true }
                
            },
            messages: {
                Id_cliente: { required: "Seleccione un cliente." },
                Id_sucursal: { required: "Seleccione una sucursal." }
            },
            invalidHandler: function (event, validator) { //display error alert on form submit
                successHandler1.hide();
                errorHandler1.show();
                //$("#validation_summary").text(validator.showErrors());
            },
            highlight: function (element) {
                $(element).closest('.help-block').removeClass('valid');
                // display OK icon
                $(element).closest('.form-group').removeClass('has-success').addClass('has-error').find('.symbol').removeClass('ok').addClass('required');
                // add the Bootstrap error class to the control group
            },
            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest('.form-group').removeClass('has-error');
                // set error class to the control group
            },
            success: function (label, element) {
                label.addClass('help-block valid');
                label.removeClass('color');
                // mark the current input as valid and display OK icon
                $(element).closest('.form-group').removeClass('has-error').addClass('has-success').find('.symbol').removeClass('required').addClass('ok');
            },
            submitHandler: function (form) {
                successHandler1.show();
                errorHandler1.hide();
                form.submit();
            }
        });
    };
    var eventos = function () {
        $('#div1').hide(0);
        $('#div2').hide(0);
        $('#div3').hide(0);
        $('#div4').hide(0);

        $("#TipoProducto").on("change", function () {
            var opcion = $(this).val();
            if (opcion === "1") {
                //los tipo 1, equivale a producto que hay en almacen por lo que hay que obtener los almacenes activos
                GetAlmacenes();
                $('#div1').show(1000);
                
            }
            else if (opcion === "2") {
                console.log("2");
            }
        });

        $("#Almacenes").on("change", function () {
            var opcion = $(this).val();

            if (opcion !== '') {
                GetProductos_Almacen(opcion);
                $('#div2').show(1000);
            }
        });

        $("#Producto").on("change", function () {
            var opcion = $(this).val();

            if (opcion !== '') {
                var existencia = $(this).children(":selected").attr("data-existencia");
                var unidadmedida = $(this).children(":selected").attr("data-unidadmedida");

                $("#cantidadExistencia").val(existencia + " " + unidadmedida);

                $('#div3').show(1000);
                $('#div4').show(1000);
            }
        });
        
    };

    function GetAlmacenes() {
        $.ajax({
            url: '/Admin/DocumentoXCobrar/GetAlmacenes/',
            type: "POST",
            dataType: 'json',
            data: {},
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "2");
            },
            success: function (result) {
                $("#Almacenes option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#Almacenes").append('<option value="' + result[i].IDAlmacen + '">' + result[i].Descripcion + '</option>');
                }
                $('#Almacenes.select').selectpicker('refresh');

            }
        });
    }

    function GetProductos_Almacen(almacen) {
        $.ajax({
            url: '/Admin/DocumentoXCobrar/GetProductosAlmacen/',
            type: "POST",
            dataType: 'json',
            data: { almacen: almacen },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "2");
            },
            success: function (result) {
                $("#Producto option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#Producto").append('<option value="' + result[i].IDProductoAlmacen + '" data-existencia="' + result[i].Existencia + '" data-preciounidad="' + result[i].PrecioUnidad + '" data-id_unidadproducto="' + result[i].Id_unidadProducto + '" data-unidadmedida="' + result[i].UnidadMedida + '" >' + result[i].Nombre + '</option>');
                }
                $('#Producto.select').selectpicker('refresh');
            }
        });
    }

    return {
        //main function to initiate template pages
        init: function () {
            runValidator1();
            eventos();
        }
    };
}();
