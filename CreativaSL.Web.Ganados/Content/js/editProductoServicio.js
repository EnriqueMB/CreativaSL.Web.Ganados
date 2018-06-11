var EditProductoServicio = function () {
    "use strict";
    var Inventario = $("#Inventario").val();

    
    function divInventario() {
        if (Inventario.localeCompare("True") == 0)
        {
            $('#esconder').show(1000);
        }
        else
        {
            $('#esconder').hide(0);
        }
        
        
    }
    
    var validacion = function () {
        var form1 = $('#frm_productoServicio');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        form1.validate({
            debug: true,
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
                Cantidad: { required: true, min: 0.000001 },
                PrecioUnitario: { required: true, min: 0.000001 },
                Id_tipoClasificacionCobro: { min: 0.01 },
                Id_productoServicio: { required: true }
            },
            messages: {
                Cantidad: { required: "Ingrese una cantidad", min: "Ingrese una cantidad" },
                PrecioUnitario: { required: "Ingrese un precio unitario", min: "Ingrese un precio unitario" },
                Id_tipoClasificacionCobro: { min: "Seleccione una descripción" },
                Id_productoServicio: { required: "Seleccione una clave o servicio" }
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
            divInventario();
            validacion();
        }
    };
}();