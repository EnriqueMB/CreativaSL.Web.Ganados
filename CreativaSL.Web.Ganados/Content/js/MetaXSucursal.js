var MetaXSucursal = function () {
    "use strict";

    var Configuracion = function () {
        $(".ganado").maskMoney(
            {
                allowZero: true,
                precision: 0
            }
        );

        $(".kg").maskMoney(
            {
                allowZero: true,
                precision: 0,
                suffix: ' Kg.'
            }
        );

        $(".kg").maskMoney('mask');
        $(".ganado").maskMoney('mask');
    };

    var Validaciones = function () {
        var form1 = $('#frmMetaXSucursal');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);
        form1.validate({ // initialize the plugin
            debug: true,
            errorElement: "dd", // contain the error msg in a span tag
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
                }
            },
            ignore: "",
            rules: {
                CantidadKilo: {
                    required: true,
                    numeroConComas: true
                },
                CantidadGanado: {
                    required: true,
                    numeroConComas: true
                }
            },
            messages: {
                CantidadKilo: {
                    required: "-El campo: Cantidad de kilos a la semana, es requerido",
                    numeroConComas: "-El campo: Cantidad de kilos a la semana, debe ser igual o mayor que 0 (solo números enteros)."
                },
                CantidadGanado: {
                    required: "-El campo: Cantidad de ganado a la semana, es requerido",
                    numeroConComas: "-El campo: Cantidad de kilos a la semana, debe ser igual o mayor que 0 (solo números enteros)."
                }
            },
            invalidHandler: function (event, validator) {
                successHandler1.hide();
                errorHandler1.show();
            },
            highlight: function (element) {
                $(element).closest('.help-block').removeClass('valid');
                $(element).closest('.controlError').removeClass('has-success').addClass('has-error').find('.symbol').removeClass('ok').addClass('required');
            },
            unhighlight: function (element) {
                $(element).closest('.controlError').removeClass('has-error');
            },
            success: function (label, element) {
                label.addClass('help-block valid');
                label.removeClass('color');
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
        init: function (option) {
            Configuracion();
            Validaciones();
        }
    };
}();