var AddProductoServicio = function () {
    "use strict";
    var Cantidad = $("#Cantidad");
    var Existencia = $("#Existencia");
    var PrecioPromedio = $("#PrecioPromedio");
    var PrecioUnitario = $("#PrecioUnitario");
    var Id_unidadProducto = $("#Id_unidadProducto");
    var Subtotal = $("#Subtotal");

    var eventos = function () {
        $("#Id_conceptoDocumento").on("change", function () {
            var inventariado = $(this).find(':selected').data('inventario');
            if (inventariado.localeCompare('True') == 0) {
                $('#inventario').show(1000);
                GetAlmacenes();
            } else {
                $('#inventario').hide(1000);
                Cantidad.rules("remove", "max");
                Cantidad.closest(".controlError").removeClass("has-success has-error");
                $("#validation_summary").find("dd[for='Cantidad']").addClass('help-block valid').text('');
                Id_unidadProducto.val(0);
            }
        });

        $("#Cantidad").on("keyup keydown", function (e) {
            calcularSubtotal();
        });
        $("#PrecioUnitario").on("keyup keydown", function (e) {
            calcularSubtotal();
        });
    }
    function calcularSubtotal() {
        var sub = Cantidad.val() * PrecioUnitario.val();
        Subtotal.val(sub);
    }
    function divInventario() {
        $('#inventario').hide(0);
    }
    function GetAlmacenes() {
        $.ajax({
            url: '/Admin/DocumentoXCobrar/GetAlmacenes/',
            type: "POST",
            dataType: 'json',
            data: {  },
            error: function () {
                Mensaje("Ocurrió un error al cargar el combo", "1");
            },
            success: function (result) {
                $("#Id_almacen option").remove();
                for (var i = 0; i < result.length; i++) {
                    $("#Id_almacen").append('<option value="' + result[i].IDAlmacen + '">' + result[i].Descripcion + '</option>');
                }
                $('#Id_almacen.select').selectpicker('refresh');
                validacion();
                eventoProductos();
            }
        });
    }
    function eventoProductos() {

        $("#Id_almacen").on("change", function () {
            var almacen = $(this).val();
            $.ajax({
                url: '/Admin/DocumentoXCobrar/GetProductosAlmacen/',
                type: "POST",
                dataType: 'json',
                data: { almacen: almacen },
                error: function () {
                    Mensaje("Ocurrió un error al cargar el combo", "1");
                },
                success: function (result) {
                    $("#Id_producto option").remove();
                    for (var i = 0; i < result.length; i++) {
                        $("#Id_producto").append('<option value="' + result[i].IDProductoAlmacen + '" data-existencia="' + result[i].Existencia + '" data-preciounidad="' + result[i].PrecioUnidad + '" data-id_unidadproducto="' + result[i].Id_unidadProducto + '">' + result[i].Nombre + '</option>');
                    }
                    $('#Id_producto.select').selectpicker('refresh');

                    Existencia.val(0);
                    PrecioPromedio.val(0);
                    Cantidad.rules("remove", "max");
                    Cantidad.closest(".controlError").removeClass("has-success has-error");
                    $("#validation_summary").find("dd[for='Cantidad']").addClass('help-block valid').text('');
                    Id_unidadProducto.val(0);
                }
            });
        });
        $("#Id_producto").on("change", function () {
            var existencia = $(this).find(':selected').data('existencia');
            var preciounidad = $(this).find(':selected').data('preciounidad');
            var id_unidadproducto = $(this).find(':selected').data('id_unidadproducto');

            Existencia.val(existencia);
            PrecioPromedio.val(preciounidad);
            Cantidad.rules("add", { max: existencia });
            Id_unidadProducto.val(id_unidadproducto);
        });
    }

    var validacion = function () {
        var form1 = $('#frm_productoServicio');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({
            debug:true,
            errorElement: "dd", 
            errorClass: 'help-block color',
            errorLabelContainer: $("#validation_summary"),
            errorPlacement: function (error, element) { // render error placement for each input type
                if (element.attr("type") == "radio" || element.attr("type") == "checkbox") { // for chosen elements, need to insert the error after the chosen container
                    error.insertAfter($(element).closest('.form-group').children('div').children().last());
                } else if (element.attr("name") == "dd" || element.attr("name") == "mm" || element.attr("name") == "yyyy") {
                    error.insertAfter($(element).closest('.form-group').children('div'));
                } else if (element.attr("type") == "text") {
                    error.insertAfter($(element).closest('.input-group').children('div'));
                } else {
                    error.insertAfter(element);
                    // for other inputs, just perform default behavior
                }
            },
            ignore: "",
            rules: {
                Id_conceptoDocumento: { min: 1 },
                Cantidad: { required: true, min: 0.000001 },
                PrecioUnitario: { required: true, min: 0.000001 },
                Id_tipoClasificacionCobro: { min: 0.01 },
                Id_productoServicio: {required:true}
            },
            messages: {
                Id_conceptoDocumento: { min: "Por favor, seleccione un tipo de descripción." },
                Cantidad: { required: "Por favor, ingrese una cantidad", min: "Por favor, ingrese una cantidad" },
                PrecioUnitario: { required: "Por favor, ingrese un precio unitario.", min: "Por favor, ingrese un precio unitario." },
                Id_tipoClasificacionCobro: { min: "Por favor, seleccione una descripción." },
                Id_productoServicio: { required: "Por favor, seleccione una clave o servicio." }
            },
            invalidHandler: function (event, validator) { //display error alert on form submit
                successHandler1.hide();
                errorHandler1.show();
            },
            highlight: function (element) {
                $(element).closest('.help-block').removeClass('valid');
                // display OK icon
                $(element).closest('.controlError').removeClass('has-success').addClass('has-error').find('.symbol').removeClass('ok').addClass('required');
                // add the Bootstrap error class to the control group
            },
            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest('.controlError').removeClass('has-error');
                // set error class to the control group
            },
            success: function (label, element) {
                label.addClass('help-block valid');
                label.removeClass('color');
                // mark the current input as valid and display OK icon
                $(element).closest('.controlError').removeClass('has-error').addClass('has-success').find('.symbol').removeClass('required').addClass('ok');
            },
            submitHandler: function (form) {
                successHandler1.show();
                errorHandler1.hide();
                form.submit();
            }
        });
    };

    return {
        init: function () {
            eventos();
            validacion();
            divInventario();
        }
    };
}();