﻿var CuentasProveedor = function () {
    "use strict";
    // Funcion para validar registrar
    var runValidator = function () {
        var form1 = $('#form-cp');
        var errorHandler1 = $('.errorHandler', form1);
        var successHandler1 = $('.successHandler', form1);

        $('#form-cp').validate({
            errorElement: "span", // contain the error msg in a span tag
            errorClass: 'help-block color',
            errorLabelContainer: "#validation_summary",  //$("#validation_summary"),
            errorPlacement: function (error, element) { // render error placement for each input type
                if (element.attr("type") == "radio" || element.attr("type") == "checkbox") { // for chosen elements, need to insert the error after the chosen container
                    error.insertAfter($(element).closest('.form-group').children('div').children().last());
                } else if (element.attr("name") == "dd" || element.attr("name") == "mm" || element.attr("name") == "yyyy") {
                    error.insertAfter($(element).closest('.form-group').children('div'));
                } else if (element.attr("type") == "text" ) {
                    error.insertAfter($(element).closest('.input-group').children('div'));
                } else {
                    error.insertAfter(element);
                    // for other inputs, just perform default behavior
                }
            },
            ignore: "",
            rules: {
                IDBanco: { CMBINT: true },
                Titular: { required: true, nombre: true, maxlength: 300 },
                NumTarjeta: { tarjetaCredito : true },
                NumCuenta: { cuenta: true },
                Clabe: { clabe: true }
            },
            messages: {
                IDBanco: { CMBINT: "Seleccione un banco." },
                Titular: { required: "Ingrese el nombre del titular de la cuenta.", nombre: "Ingrese un nombre del titular válido.", maxlength: "El campo nombre del titular admite máximo 300 caracteres." },
                NumTarjeta: { tarjetaCredito: "Ingrese un número de tarjeta válido." },
                NumCuenta: { cuenta: "Ingrese un número de cuenta válido."},
                Clabe: { clabe: "Ingrese una clabe interbancaria válida." }
            },
            invalidHandler: function (event, validator) { //display error alert on form submit
                successHandler1.hide();
                errorHandler1.show();
                //$("#validation_summary").text(validator.showErrors());
            },
            highlight: function (element) {
                $(element).closest('.help-block').removeClass('valid');
                // display OK icon
                $(element).closest('.input-group').removeClass('has-success').addClass('has-error').find('.symbol').removeClass('ok').addClass('required');
                // add the Bootstrap error class to the control group
            },
            unhighlight: function (element) { // revert the change done by hightlight
                $(element).closest('.input-group').removeClass('has-error');
                // set error class to the control group
            },
            success: function (label, element) {
                label.addClass('help-block valid');
                label.removeClass('color');
                // mark the current input as valid and display OK icon
                $(element).closest('.input-group').removeClass('has-error').addClass('has-success').find('.symbol').removeClass('required').addClass('ok');
            },
            submitHandler: function (form) {
                successHandler1.show();
                errorHandler1.hide();
                form.submit();
            }
        });
    };

    var runElements = function ()
    {
        $("input.mask_credit").mask('?9999-9999-9999-9999', {autoUnmask: true , showMaskOnFocus: false, showMaskOnHover: false});
        
    };

    return {
        //main function to initiate template pages
        init: function () {
            runValidator();
            runElements();
        }
    };
}();